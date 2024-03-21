using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Idle", fileName = "PlayerState_Idle")]
public class PlayerState_Idle : PlayerState
{
    public override void Enter()
    {
        AudioSource audioSource = playerObject.GetComponent<AudioSource>();
        audioSource.Stop();
        animator.Play("Idle");
    }

    public override void LogicUpdate()
    {
        if(input.jumpPressed&&input.jumpOK)
        {
            stateMachine.SwitchState(typeof(PlayerState_Jump));
        }
        else if (input.isMoving)
        {
            stateMachine.SwitchState(typeof(PlayerState_Run));
        }
    }
}