using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data")]
public class DataBase : ScriptableObject
{
    public PlayerData playerData;

    public TrajectoralMovementData trajectoralMovementData;
}
