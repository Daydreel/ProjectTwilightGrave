using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


[CreateAssetMenu(menuName = "DayFSM/State")]
public class State : ScriptableObject {

    [Tooltip("If the state must use fixedUpdate instead of Update check the isPhysic")]
    public bool isPhysic = false;

    public OnEnter[] onEnters;
    public OnExit[] onExits;

    public Action[] actions;
    public Transition[] transitions;

    //The UpdateState is called every frame
    public void UpdateState(DayFSM FSM)
    {
        //Go through all registered actions
        foreach (Action action in actions)
        {
            #region Warnings
            //Some warnings
            if (action == null)
            {
                Debug.LogWarning("Carefull, you have setup an empty action !! check this state again : " + this);
            }
            #endregion

            action.Act(FSM);
        }

        //Check if 
        CheckTransitions(FSM);
    }

    //When entering the state, go through every actions initialise,OnExit and decision initialise
    public void onEnterState(DayFSM FSM)
    {



        //Go through all registered actions
        foreach (OnEnter onEnter in onEnters)
        {
            #region Warnings
            if (onEnter == null)
            {
                Debug.LogWarning("Carefull, you have setup an empty on enter !! check this state again : " + this);
            }
            #endregion
            
            onEnter.Act(FSM);
        }

        //Initialise Action State 
        foreach (Action action in actions)
        {
            #region Warnings
            if (action == null)
            {
                Debug.LogWarning("Carefull, you have setup an empty action !! check this state again : " + this);
            }
            #endregion
            
            action.Initialise(FSM);
        }

        //Initialise Decision State 
        foreach (Transition transition in transitions)
        {
            transition.decision.Initialize(FSM);
            Debug.Log("On Enter");
        }
    }

    //When exiting the state, go through every actions terminate, OnExit and decision terminate
    public void onExitState(DayFSM FSM)
    {
        //Go through all registered actions
        foreach (OnExit onExit in onExits)
        {
            #region Warnings
            //Some warnings
            if (onExit == null)
            {
                Debug.LogWarning("Carefull, you have setup an empty onExit !! check this state again : " + this);
            }
            #endregion

            onExit.Act(FSM);
        }

        //Terminate action of state
        foreach (Action action in actions)
        {
            action.Terminate(FSM);
        }

        //Terminate Decision of State 
        foreach (Transition transition in transitions)
        {
            transition.decision.Terminate(FSM);
        }
    }

    private void CheckTransitions(DayFSM FSM)
    {
        foreach (Transition transition in transitions)
        {
            bool decisionSucceeded = transition.decision.Decide(FSM);

            if (decisionSucceeded)
                FSM.TransitionToState(transition.nextState);
        }
    }
}
