using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;


public class button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Color normalImageColor = Color.white;
    public Color highlightedImageColor = Color.green;

    public Color normalTextColor = Color.white;
    public Color highlightedTextColor = Color.black;

    private Image buttonImage;
    private TextMeshProUGUI buttontext;
    // Start is called before the first frame update
    void Start()
    {

        buttonImage = GetComponent<Image>();
        buttontext = GetComponentInChildren<TextMeshProUGUI>();

        buttonImage.color = normalImageColor;
        buttontext.color = normalTextColor;

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        
        buttonImage.color = highlightedImageColor;
        buttontext.color = highlightedTextColor;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
       
        buttonImage.color = normalImageColor;
        buttontext.color = normalTextColor;
    }


}
