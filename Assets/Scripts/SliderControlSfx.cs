using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderControlSfx : MonoBehaviour
{
    public Slider SFXSlider;
    public AudioSource SfxSource;
    
    void Start()
    {
        if (SfxSource != null)
        {
            SFXSlider.value = SfxSource.volume * 100; 
        }
        SFXSlider.onValueChanged.AddListener(SetSfxVolume);

    }
    public void IncreaseVolume()
    {
        if (SFXSlider.value < SFXSlider.maxValue)
        {
            SFXSlider.value += 1;
            AudioManager.instance.PlaySfx("Ui Increase");
        }
    }
    public void DecreaseVolume()
    {
        if (SFXSlider.value > SFXSlider.minValue)
        {
            SFXSlider.value -= 1;
            AudioManager.instance.PlaySfx("Ui Decrease");
        }
    }
    void SetSfxVolume(float value)
    {
        if (SfxSource != null)
        {
            SfxSource.volume = value / 100;
        }

    }


}
