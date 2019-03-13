using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DayFSM/Actions/Dodge")]
public class Dodge : Action
{
    public AnimationCurve animCurve;

    //Privates
    float t;
    Rigidbody body;
    Keyframe lastKey;
    float dodgeDistance;

    public override void Act(DayFSM fsm)
    {
        PlayerDodge(fsm);
    }

    public override void Initialise(DayFSM fsm)
    {
        t = 0;
        body = fsm.entityB.body;
        lastKey = animCurve.keys[animCurve.length - 1];

        //Get player dodge distance
        dodgeDistance = fsm.entityB.entityS.dodgeDistance;
    }

    void PlayerDodge(DayFSM fsm)
    {
        t += Time.fixedDeltaTime;
        if (t < lastKey.time)
        {
            body.MovePosition(body.position + body.transform.forward * dodgeDistance * Time.deltaTime * animCurve.Evaluate(t));
        }

        
    }
}
