using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DayFSM/Boss/Decisions/IsDistanceInRange")]
public class IsDistanceInRange : Decision
{
    //What kind of distance are we looking for ?
    public DistanceType distanceType;

    public override bool Decide(DayFSM FSM)
    {
        // Get boss stats
        BossStats bossStats = FSM.bossB.bossStats;
        //Get distance between the boss and the player
        float targetDistance = FSM.bossB.TargetDistance();

        //Depending on the selected type we return true for 
        switch (distanceType)
        {
            case DistanceType.CAC:
                if (targetDistance >= 0 && targetDistance <= bossStats.CACReach)
                {
                    return true;
                }
                break;
            case DistanceType.MID:
                if (targetDistance > bossStats.CACReach && targetDistance <= bossStats.MIDReach)
                {
                    return true;
                }
                break;
            case DistanceType.LONG:
                if (targetDistance > bossStats.MIDReach && targetDistance <= bossStats.LONGReach)
                {
                    return true;
                }
                break;
            default:
                break;
        }

        return false;
    }
}
