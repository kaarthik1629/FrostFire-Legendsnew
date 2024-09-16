using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sounds[] musicsounds;
    public SFX[] Sfxsounds;
    public AudioSource musicsource, sfxsource;
    public static AudioManager instance;



    private void Awake()
    {
        instance = this;
    }

    public void Playmusic(string name)
    {
        Sounds sounds = Array.Find(musicsounds, sounds => sounds.musicname == name);

        if (sounds == null)
        {
            print("sounds not found");


        }
        else
        {
            musicsource.clip = sounds.musicClip;
            musicsource.Play();
        }
    }
    public void PlaySfx(string name)
    {
        SFX sfx = Array.Find(Sfxsounds, sfx => sfx.sfxname == name);

        if (sfx == null)
        {
            print("Sfx sounds not found");
        }
        else
        {
            sfxsource.clip = sfx.sfxClip;
            sfxsource.Play();
        }

    }

}