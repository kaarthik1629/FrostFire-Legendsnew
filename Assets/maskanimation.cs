using UnityEngine;

public class MaskController : MonoBehaviour
{
    public Animator maskAnimator;

    public void StartMaskAnimation()
    {
        maskAnimator.SetTrigger("Highlighted"); 
    }
    public void StopMaskAnimation()
    {
        maskAnimator.SetTrigger("Highlighted"); 
    }
}