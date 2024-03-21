using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Data/StateMachine/PlayerState/Attack", fileName = "PlayerState_Attack")]
public class PlayerState_Attack : PlayerState
{
    public override void Enter()
    {
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