using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Gaia;
using UnityEngine.Rendering;

public class SliderControl : MonoBehaviour
{
    public Slider volumeSlider;
    
    private GaiaAudioManager gaiaAudioManager;

    private void Start()
    {
        gaiaAudioManager = GaiaAudioManager.Instance;


        if (gaiaAudioManager != null)
        {
            volumeSlider.value = gaiaAudioManager.m_masterVolume*100; 
        }

        volumeSlider.onValueChanged.AddListener(SetMusicVolume);
       

    }
    public void IncreaseVolume()
    {
        if (volumeSlider.value < volumeSlider.maxValue)
        {
            volumeSlider.value += 1;
            AudioManager.instance.PlaySfx("Ui Increase");
        }
    }
    public void DecreaseVolume()
    {
        if (volumeSlider.value > volumeSlider.minValue)
        {
            volumeSlider.value -= 1;
            AudioManager.instance.PlaySfx("Ui Decrease");
        }
    }
    void SetMusicVolume(float value)
    {
        if (gaiaAudioManager != null)
        {
            gaiaAudioManager.m_masterVolume = value/100; // Set master volume
        }


       
       // musicSource.volume = value;
    }

   


}
