using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "DayFSM/Decisions/IsButtonPressed")]
public class IsButtonPressed : Decision
{
    public PlayerInput playerInput;

    public override bool Decide(DayFSM FSM)
    {
        //Debug.Log("IsGrounded" + fsm.entityB.CheckIsGround());
        if (Input.GetAxis(playerInput.ToString()) != 0)
        {
            return true;
        }
        return false;
    }
}
