using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ResetDataButton : MonoBehaviour
{

    [SerializeField] DefaultValueResetter[] allResetValues;
    public void ResetData()
    {
        foreach (var item in allResetValues)
        {
            item.elementFloat.ResetFloat(item.defaultValue);
        }
    }
}

 [Serializable]
public class DefaultValueResetter
{
    public DataUIElementFloat elementFloat;
    public float defaultValue;
}