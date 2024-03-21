using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : ScriptableObject, IState
{
    protected Animator animator;

    protected PlayerGameInput input;

    protected PlayerManager playerManager;

    protected GameObject playerObject;

    protected PlayerStateMachine stateMachine;

    public void Initialize(Animator animator, PlayerGameInput input, PlayerManager playerManager,GameObject playerObject, PlayerStateMachine stateMachine)
    {
        this.animator = animator;
        this.input = input;
        this.playerManager = playerManager;
        this.playerObject = playerObject;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter()
    {
        
    }

    public virtual void Exit()
    {
        
    }

    public virtual void LogicUpdate()
    {
        
    }

    public virtual void PhysicUpdate()
    {
        
    }
}