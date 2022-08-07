using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    [SerializeField] DataBase data;

    [SerializeField] Transform startPos;
    [SerializeField] Transform endPos;

    [SerializeField] PlayerInputHandler playerInputHandler;
    [SerializeField] InputActionReference moveInput;
    [SerializeField] InputActionReference lookInput;
    [SerializeField] Camera cam;
    [SerializeField] Transform cinemachineCam;
    [SerializeField] Transform ball;

    private float currentCameraPitch = 0;
    private bool isFirstClicking = true;
    private CharacterController characterController;
    #region MonoBehaviour callbacks
    private void Awake()
    {
        moveInput.action.Enable();
        lookInput.action.Enable();

        characterController = GetComponent<CharacterController>();
    }
    private void Update()
    {
        HandleMovement();
    }
    protected void LateUpdate()
    {
        UpdateCamera();
    }
    #endregion

    #region raycast

    public void RaycastForPosSetting()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 pos = hit.point;
            pos.y = 0.15f;
            if (isFirstClicking)
            {
                startPos.position = pos;
            }
            else
            {
                endPos.position = pos;
                StartTrajectoryMovement(startPos.position, endPos.position, data.trajectoralMovementData.maxHeight.value, data.trajectoralMovementData.duration.value);
            }
            isFirstClicking = !isFirstClicking;
        }
    }
    #endregion

    #region strafe movement

    private void HandleMovement()
    {
        Vector2 movementInputVector = moveInput.action.ReadValue<Vector2>();

        if (movementInputVector != Vector2.zero)
        {
            Vector3 input = new Vector3(movementInputVector.x, 0, movementInputVector.y);
            Vector3 movement = cam.transform.forward * input.z + cam.transform.right * input.x;
            movement = movement * data.playerData.movementSpeed.value * Time.deltaTime;
            movement.y = 0;
            characterController.Move(movement);
        }
    }

    #endregion
    #region Camera rotation
    protected void UpdateCamera()
    {
        if (!playerInputHandler.middleMousePressed) return;
        Vector2 lookInputVector = lookInput.action.ReadValue<Vector2>();

        //calculate camera inputs
        float cameraYawDelta = lookInputVector.x * data.playerData.cameraHorizontalSensitivity.value * Time.deltaTime;
        float cameraPitchDelta = lookInputVector.y * data.playerData.cameraVerticalSensivitity.value * Time.deltaTime * -1; //invert

        //rotate character
        transform.localRotation = transform.localRotation * Quaternion.Euler(0f, cameraYawDelta, 0);

        //tilt the camera
        currentCameraPitch = Mathf.Clamp(currentCameraPitch + cameraPitchDelta, data.playerData.cameraMinPitch.value, data.playerData.cameraMaxPitch.value);

        cinemachineCam.localRotation = Quaternion.Euler(currentCameraPitch, 0, 0);
    }
    #endregion

    #region Trajectory Movement
    public void StartTrajectoryMovement(Vector3 startPos, Vector3 endPos, float maxHeight, float duration)
    {
        if (data == null)
        {
            Debug.LogError("Please add the data in the inspector for this gameObject: " + gameObject.name);
            return;
        }

        if (data.playerData.trajectoryMethod == null)
        {
            Debug.LogError("Please start the game in MainMenu scene and choose trajectory method!");
            return;
        }
        StartCoroutine(TrajectoryMovementCoroutine(startPos, endPos, maxHeight, duration));
    }
    private IEnumerator TrajectoryMovementCoroutine(Vector3 startPos, Vector3 endPos, float maxHeight, float duration)
    {
        float time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            // the time that compressed between 0-1 for lerp and animation curve.
            float compressedTime = time / duration;

            //uygun trajectory methodundan degeri cekiyoruz.
            ball.position = data.playerData.trajectoryMethod.Movement(startPos, endPos, compressedTime, maxHeight);
            yield return new WaitForEndOfFrame();
        }
        //Hareket tamamlandigi zaman bounce check atarak, eger isBounce true ise bi sonraki gitmesini gereken konumu hesaplayarak
        //Recursive bir sekilde fonksiyonu tekrar cagiriyor.
        if (data.trajectoralMovementData.isBounce.value)
        {
            maxHeight *= data.trajectoralMovementData.bounceHeightMultiplier.value;

            // islemi yapabilmek icin maxHeight'in belli bi seviyenin uzerinde olmasini bekliyoruz yoksa kucucuk float degerleri icinde kayboluruz ve gercekci bi goruntu elde edemeyiz
            // tabii en gercekci durum icin; topun agirligi, yatay atis icin verilen kuvvet, surtunme katsayisi, yercekimi gucu vs gibi degerleri kullanmak gerekir.
            if (maxHeight > data.trajectoralMovementData.minBounceHeight.value)
            {
                //Bir sonraki konumun hesaplanmasi, bi onceki harekette gidilen x ve z mesafesinin yarisi(yari azalip ayni direction'u korusun diye) ve height'i da bounceMultiplier'a carparak
                float x = endPos.x - startPos.x;
                float z = endPos.z - startPos.z;
                startPos = endPos;
                endPos = new Vector3(endPos.x + (x * data.trajectoralMovementData.bounceForwardMultiplier.value), endPos.y, endPos.z + (z * data.trajectoralMovementData.bounceForwardMultiplier.value));
                //Recursive calling
                StartTrajectoryMovement(startPos, endPos, maxHeight, duration);
            }

        }
    }

    #endregion

    #region other
    public void SetCursorLocked(bool locked)
    {
        Cursor.visible = !locked;
        Cursor.lockState = locked ? CursorLockMode.Locked : CursorLockMode.None;
    }
    #endregion
}
