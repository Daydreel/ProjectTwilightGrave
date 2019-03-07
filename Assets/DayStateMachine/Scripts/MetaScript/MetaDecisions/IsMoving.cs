using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "DayFSM/Decisions/IsRunning")]
public class IsMoving : Decision {
    public override bool Decide(DayFSM fsm)
    {
        if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            return true;
        }
        return false;
    }


}
