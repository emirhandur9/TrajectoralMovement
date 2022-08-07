using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DataUIElementFloat : MonoBehaviour
{
    [Header("Core")]
    [SerializeField] ScriptableFloat scriptableFloat;

    [Header("UI")]
    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI minText;
    [SerializeField] TextMeshProUGUI maxText;
    [SerializeField] TextMeshProUGUI sliderText;

    private void Awake()
    {
        Init();
    }
    public void Init()
    {
        slider.value = scriptableFloat.value;
        sliderText.text = slider.value.ToString();
        minText.text = slider.minValue.ToString();
        maxText.text = slider.maxValue.ToString();
    }
    public void OnValueChanged()
    {
        float value = (float)Math.Round(slider.value, 2);
        scriptableFloat.value = value;
        sliderText.text = value.ToString();
    }

    public void ResetFloat(float value)
    {
        scriptableFloat.value = value;
        slider.value = scriptableFloat.value;
        sliderText.text = slider.value.ToString();
    }
}
