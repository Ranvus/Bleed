using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloodBarController : MonoBehaviour
{
    [SerializeField] private Slider slider;

    public void SetMaxBlood(float blood)
    {
        slider.maxValue = blood;
        slider.value = blood;
    }

    public void SetBlood(float blood)
    {
        slider.value = blood;
    }
}
