using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "DayFSM/OnEnter/StartTriggerAnim")]
public class StartTriggerAnim : OnEnter
{
    [Tooltip("Animation Enum can be found in the player fsm. Must be set up in the animator first then added in the PlayerMoveSet enum")]
    public PlayerMoveSet playerMoveSet;

    public BossAngerMoveSet bossAngerMoveSet;

    public override void Act(DayFSM fsm)
    {
        //Debug.Log(this);

        if (playerMoveSet != PlayerMoveSet.Null)
        {
            //Need to add an animation Trigger to the move set
            fsm.animator.SetTrigger(playerMoveSet.ToString());
        }

        if (bossAngerMoveSet != BossAngerMoveSet.Null)
        {
            //Need to add an animation Trigger to the move set
            fsm.animator.SetTrigger(bossAngerMoveSet.ToString());
        }
    }
}
