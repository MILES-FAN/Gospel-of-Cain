using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Jump", fileName = "PlayerState_Jump")]
public class PlayerState_Jump : PlayerState
{
    public override void Enter()
    {
        AudioSource audioSource = playerObject.GetComponent<AudioSource>();
        audioSource.clip = playerManager.jumpSFX;
        audioSource.Stop();
        audioSource.loop = false;
        audioSource.mute = false;
        audioSource.Play();
        animator.Play("Jump");
    }

    public override void LogicUpdate()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            stateMachine.SwitchState(typeof(PlayerState_Run));
        }
    }
}