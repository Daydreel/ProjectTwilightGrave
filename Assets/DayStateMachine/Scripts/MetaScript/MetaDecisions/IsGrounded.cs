using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DayFSM/Decisions/IsGrounded")]
public class IsGrounded : Decision
{
    public float Radius;
    public Vector3 SpherePosition;
    [Tooltip("Get int of floor or environment layer mask to check with")]
    public int FloorLayerMask;
    Transform transform;


    public override bool Decide(DayFSM fsm)
    {
        //Debug.Log(fsm.playerfsm.CheckIsGround());
        return fsm.entityB.CheckIsGround();
    }
}
