using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DayFSM/Actions/Idle")]
public class Idle : Action
{
    public override void Act(DayFSM fsm)
    {
        ActIdle(fsm);
    }

    private void ActIdle(DayFSM fsm)
    {

    }
}
