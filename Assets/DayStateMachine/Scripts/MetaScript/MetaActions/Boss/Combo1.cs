using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DayFSM/Boss/Actions/Combo1")]
public class Combo1 : Action
{
    public override void Act(DayFSM FSM)
    {
        Debug.Log("Combo1");
    }
}
