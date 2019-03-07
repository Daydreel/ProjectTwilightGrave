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
        }

        if (timerManager.time >= Duration)
        {
            Debug.Log("timerManager True");
            return true;
        }

        return false;
        
    }

    public override void Initialize(DayFSM fsm)
    {
        timerManager = TimerManager.Instance;
        timerManager.StartTimer();
    }

    public override void Terminate(DayFSM fsm)
    {
        timerManager.StopTimer();
    }
}
