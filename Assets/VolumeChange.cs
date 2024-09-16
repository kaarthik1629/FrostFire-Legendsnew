using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VolumeChange : MonoBehaviour
{
    public Slider VolumeSlider;
    public TextMeshProUGUI volumetext;

    // Start is called before the first frame update
    void Start()
    {
        UpdateVolumeText((int)VolumeSlider.value);

        VolumeSlider.onValueChanged.AddListener(delegate { UpdateVolumeText((int)VolumeSlider.value); });


    }

    // Update is called once per frame
    void UpdateVolumeText(int value)
    {
        volumetext.text = value.ToString() + "%";
        
    }
}
