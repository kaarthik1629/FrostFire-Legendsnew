using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CinemachineShake : MonoBehaviour
{
    public static CinemachineShake Instance { get; private set; }
    private CinemachineFreeLook cinemachineFreeLook;
    private float shakeTimer;
    private float StartingIntensity;
    private float ShakeTimerTotal;
    private void Awake()
    { 
        Instance = this;
        cinemachineFreeLook = GetComponent<CinemachineFreeLook>();
        ResetShake();

    }
   public void ShakeCamera(float intensity , float time)

    
   {
        for (int i = 0; i < 3; i++)
        {

            CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
                           cinemachineFreeLook.GetRig(i).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        if (cinemachineBasicMultiChannelPerlin != null)
        {
            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
            StartingIntensity = intensity;
            ShakeTimerTotal = time;
            shakeTimer = time;
            Debug.Log(intensity);
        }
        else
        {
            Debug.LogError("CinemachineBasicMultiChannelPerlin component not found on middle rig.");
        }


        }




           
   }

    private void Update()
    {
        if(shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if(shakeTimer <= 0f ) 
            {

                for (int i = 0; i < 3; i++)
                {

                    CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
                            cinemachineFreeLook.GetRig(i).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                    if (cinemachineBasicMultiChannelPerlin != null)
                    {

                        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(StartingIntensity, 0f, (1 - (shakeTimer / ShakeTimerTotal)));

                    }

                }
                if (shakeTimer <= 0f)
                {
                    ResetShake();
                }





            }
        }
    }
    private void ResetShake()
    {
        for (int i = 0; i < 3; i++)
        {
            CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
                cinemachineFreeLook.GetRig(i).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            if (cinemachineBasicMultiChannelPerlin != null)
            {
                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
            }
        }
    }
}

