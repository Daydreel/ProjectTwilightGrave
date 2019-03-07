using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Fall : Action
{
    public UnityEvent FallEvent;

    public override void Act(DayFSM fsm)
    {
        FallEvent.Invoke();
    }
}
