using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DayFSM/FSM")]
public class DayFSM : ScriptableObject
{
    public State currentState;

    //For editor purpose only
    public List<State> states;

    //Get character behaviour and animator to entity
    [HideInInspector]
    public EntityBehaviour entityB;
    [HideInInspector]
    public BossBehaviour bossB;
    [HideInInspector]
    public Animator animator;

    public void Start()
    {
        //Enters the state !
        currentState.onEnterState(this);
    }

    //Call in the update of the GameObject fsm
    public void Update()
    {
        if (currentState == null)
        {
            Debug.LogError("No State");
        }
        else
        {
            currentState.UpdateState(this);
        }
    }

    //Call in the update of the GameObject fsm
    public void FixedUpdate()
    {
        //If the state isn't physic related then Update is used instead of FixedUpdate
        if (!currentState.isPhysic)
            return;

        if (currentState == null)
        {
            Debug.LogError("No State");
        }
        else
        {
            currentState.PhysicUpdateState(this);
        }
    }

    public void TransitionToState(State nextState)
    {
        //If the next state is different from the state we have been, then change state
        //Exiting the last state
        currentState.onExitState(this);

        //Entering the new state
        nextState.onEnterState(this);

        //Change State
        if (nextState != currentState)
            currentState = nextState;
    }
}
