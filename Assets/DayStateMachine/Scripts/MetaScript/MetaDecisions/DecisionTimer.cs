using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "DayFSM/Decisions/Timer")]
public class DecisionTimer : Decision
{
    public float Duration = 2.0f;

    TimerManager timerManager;

    public override bool Decide(DayFSM fsm)
    {
        if (timerManager == null)
        {
            Debug.Log(timerManager);
            Initialise(fsm);
        }

        if (timerManager.time >= Duration)
        {
            //Debug.Log("timerManager True");
            return true;
        }

        return false;
    }

    public override void Initialise(DayFSM fsm)
    {
        timerManager = TimerManager.Instance;
        timerManager.StartTimer();
        //Debug.Log("Initialize Timer");
    }

    public override void Terminate(DayFSM fsm)
    {
        timerManager.StopTimer();
    }
}
