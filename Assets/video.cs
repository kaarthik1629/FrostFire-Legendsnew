using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class video : MonoBehaviour
{
    public VideoPlayer backscreen;


    private void Start()
    {
        backscreen = GetComponent<VideoPlayer>();   
        backscreen.Play();
    }
}
