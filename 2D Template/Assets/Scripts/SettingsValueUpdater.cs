using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsValueUpdater : MonoBehaviour
{
    public Slider Slider;
    public float StartValue;
    public string ValueName;
    // Start is called before the first frame update
    void Start()
    {
        Slider.GetComponent<Slider>().value = StartValue;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<TMP_Text>().SetText(ValueName + ": " + Slider.value + "%");
    }
    public void SetValueText(float sliderValue)
    {
        gameObject.GetComponent<TMP_Text>().SetText(ValueName + ": " + sliderValue + "%");
    }
}
