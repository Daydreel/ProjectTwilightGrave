using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "DayFSM/Decisions/IsJumping")]
public class IsJumping : Decision {
    public override bool Decide(DayFSM fsm)
    {

        //Debug.Log("IsGrounded" + fsm.entityB.CheckIsGround());
        if (Input.GetAxis("Jump") != 0)
        {
            return true;
        }
        return false;
    }
}
