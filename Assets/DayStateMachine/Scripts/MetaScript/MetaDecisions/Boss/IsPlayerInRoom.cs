using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DayFSM/Boss/Decisions/IsPlayerInRoom")]
public class IsPlayerInRoom : Decision
{
    public override bool Decide(DayFSM FSM)
    {

        if (FSM.bossB.target != null)
        {
            return true;
        }

        return false;
    }
}
