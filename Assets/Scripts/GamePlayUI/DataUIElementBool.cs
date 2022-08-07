using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataUIElementBool : MonoBehaviour
{
    [Header("Core")]
    [SerializeField] ScriptableBool value;

    [Header("UI")]
    [SerializeField] Toggle toggle;
    private void Awake()
    {
        Init();
    }
    public void Init()
    {
        toggle.isOn = value.value;
    }

    public void OnValueChanged()
    {
        value.value = toggle.isOn;
    }
}
