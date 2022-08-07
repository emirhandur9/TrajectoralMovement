using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Action<int> onButtonClicked;

    [SerializeField] DataBase data;

    [SerializeField] TrajectoralMovementBase animationCurve; //index 0
    [SerializeField] TrajectoralMovementBase bezierCurve; //index 1
    [SerializeField] TrajectoralMovementBase trigonometry; //index 2
    private void OnEnable()
    {
        onButtonClicked += OnButtonClicked;
    }

    private void OnDisable()
    {
        onButtonClicked -= OnButtonClicked;
    }

    private void Start()
    {
        data.playerData.trajectoryMethod = null;
    }

    private void OnButtonClicked(int buttonIndex)
    {
        switch (buttonIndex)
        {
            case 0:
                data.playerData.trajectoryMethod = animationCurve;
                break;
            case 1:
                data.playerData.trajectoryMethod = bezierCurve;
                break;
            case 2:
                data.playerData.trajectoryMethod = trigonometry;
                break;
        }


        SceneManager.LoadScene("GamePlay");
    }

}
