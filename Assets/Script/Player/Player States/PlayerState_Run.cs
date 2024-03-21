using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Run", fileName = "PlayerState_Run")]
public class PlayerState_Run : PlayerState
{
    AudioSource audioSource;
    public override void Enter()
    {
        audioSource = playerObject.GetComponent<AudioSource>();
        audioSource.Stop();
        audioSource.clip = playerManager.walkSFX;
        audioSource.loop = true;
        audioSource.mute = false;
        audioSource.time = Random.Range(0f, audioSource.clip.length);
        audioSource.Play();
        animator.Play("Walk");
    }

    public override void LogicUpdate()
    {
        if(input.isGrounded)
        {
            audioSource.mute = false;
        }
        else
        {
            audioSource.mute = true;
        }

        if (input.jumpPressed && input.jumpOK)
        {
            audioSource.time = 0f;
            stateMachine.SwitchState(typeof(PlayerState_Jump));
        }
        else if (!input.isMoving)
        {
            audioSource.Stop();
            audioSource.time = 0f;
            stateMachine.SwitchState(typeof(PlayerState_Idle));
        }
        
        
    }
}