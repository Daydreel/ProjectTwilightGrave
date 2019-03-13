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
        Queue<PlayerInput> buffer = FSM.entityB.inputBuffer.buffer;
        
        foreach (var input in buffer)
        {
            if (Input.GetButtonDown(playerInput.ToString()))
            {
                buffer.Clear();
                return true;
            }
        }
        return false;
    }
}
