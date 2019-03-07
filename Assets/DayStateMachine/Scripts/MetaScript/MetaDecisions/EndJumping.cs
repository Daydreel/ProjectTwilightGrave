using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "DayFSM/Decisions/EndJumping")]
public class EndJumping : Decision {
    static bool airborne = false;

    public override bool Decide(DayFSM fsm)
    {
        //Debug.Log(airborne);
        return airborne;
    }

    public void SetAirborneTrue()
    {
        airborne = true;
    }

    public void SetAirborneFalse()
    {
        airborne = false;
    }
}
