using System;
using UnityEngine;

[Serializable]
public class PlayerData 
{
    [HideInInspector]
    public TrajectoralMovementBase trajectoryMethod;

    [Header("Movement")]
    public ScriptableFloat movementSpeed;
    [Header("Camera")]
    public ScriptableFloat cameraHorizontalSensitivity;
    public ScriptableFloat cameraVerticalSensivitity;
    public ScriptableFloat cameraMinPitch;
    public ScriptableFloat cameraMaxPitch;
}
