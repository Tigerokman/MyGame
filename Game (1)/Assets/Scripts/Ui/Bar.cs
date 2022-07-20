using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class Bar : MonoBehaviour
{
    [SerializeField] protected Slider Slider;
    [SerializeField] protected TMP_Text Text;

    protected virtual void OnValueChanged(int value, int maxValue)
    {
        Slider.value = (float) value / maxValue;
        Text.text = value + "/" + maxValue;
    }
}
