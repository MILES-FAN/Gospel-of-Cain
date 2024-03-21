using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [SerializeField] PlayerState[] states;

    public Animator animator;

    PlayerGameInput playerInput;

    [SerializeField] PlayerManager playerManager;

    void Awake()
    {
        //animator = GetComponentInChildren<Animator>();

        playerInput = GetComponent<PlayerGameInput>();



        stateTable = new Dictionary<System.Type, IState>(states.Length);

        foreach (PlayerState state in states)
        {
            state.Initialize(animator, playerInput, playerManager,this.gameObject, this);
            stateTable.Add(state.GetType(), state);
        }
    }

    void Start()
    {
        SwitchOn(stateTable[states[0].GetType()]); //取首个状态为默认状态
    }
}