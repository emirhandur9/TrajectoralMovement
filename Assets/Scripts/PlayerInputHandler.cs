using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    //this class is just for button type inputs, Vector2 inputs are handling by player it self because this just a line of readvalue code.

    FPSController fpsController;

    public bool middleMousePressed;

    [SerializeField] Player player;

    private void Awake()
    {
        fpsController = new FPSController();
    }

    private void OnEnable()
    {

        fpsController.Enable();

        fpsController.Player.MiddleMouse.started += OnMiddleMouse;
        fpsController.Player.MiddleMouse.canceled += OnMiddleMouse;

        fpsController.Player.LeftClick.performed += OnLeftClick;
    }

    private void OnDisable()
    {
        fpsController.Player.MiddleMouse.started -= OnMiddleMouse;
        fpsController.Player.MiddleMouse.canceled -= OnMiddleMouse;

        fpsController.Player.LeftClick.performed -= OnLeftClick;
    }
    private void OnMiddleMouse(InputAction.CallbackContext context)
    {
        middleMousePressed = context.ReadValueAsButton();
        player.SetCursorLocked(middleMousePressed);
    }

    private void OnLeftClick(InputAction.CallbackContext context)
    {
        player.RaycastForPosSetting();
    }

}
