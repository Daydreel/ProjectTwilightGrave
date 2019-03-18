using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DayFSM/Boss/Actions/Chase")]
public class Chase : Action
{
    public override void Act(DayFSM FSM)
    {
        BossBehaviour BossB = FSM.bossB;

        if (BossB != null)
        {
            BossB.navMeshAgent.destination = BossB.target.transform.position;
            BossB.navMeshAgent.isStopped = false;
        }
    }
}
