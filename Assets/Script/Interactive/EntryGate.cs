using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryGate : MonoBehaviour
{
    Animator animator;
    public float openDuration = 5f;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    
    public void OpenTheGate()
    {
        OpenTheGate(openDuration);
    }

    public void OpenTheGate(float duration)
    {
        animator.ResetTrigger("Close");
        animator.SetTrigger("Open");
        if(duration != -1f)
            Invoke("CloseTheGate", duration);
    }

    public void CloseTheGate()
    {
        animator.ResetTrigger("Open");
        animator.SetTrigger("Close");
    }
}
