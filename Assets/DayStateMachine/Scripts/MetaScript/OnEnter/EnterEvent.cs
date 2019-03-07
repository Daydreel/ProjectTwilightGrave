using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "DayFSM/OnEnter/EnterEvent")]
public class EnterEvent : OnEnter
{
    public UnityEvent EEvent;

    public override void Act(DayFSM fsm)
    {
        EEvent.Invoke();
    }
}
