using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "DayFSM/Actions/Jump")]
public class Jump : Action
{
    public AnimationCurve animCurve;
    public UnityEvent JumpEvent;

    //Privates
    float t;
    Rigidbody body;
    Keyframe lastKey;
    float jumpHeight;

    public override void Act(DayFSM fsm) { 
        PlayerJump(fsm);
    }

    public override void Initialise(DayFSM fsm)
    {
        
        t = 0;
        body = fsm.entityB.body;
        lastKey = animCurve.keys[animCurve.length - 1];

        //Get player Jump Height
        jumpHeight = fsm.entityB.entityS.JumpHeight;
    }

    void PlayerJump(DayFSM fsm)
    {
        t += Time.fixedDeltaTime;
        if (t < lastKey.time)
        {
            body.MovePosition(body.position + Vector3.up * jumpHeight * Time.deltaTime * animCurve.Evaluate(t));
        }
        //If jumping is finished
        else
        {
            JumpEvent.Invoke();
        }
    }


}
