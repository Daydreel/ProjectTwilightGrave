using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "DayFSM/Data/BossStats")]
public class BossStats : EntityStats
{
    //Furie Bar
    public float MaxAnger = 100.0f;//TODO
    public float CurrentAnger;//TODO


    public float CACReach = 2.0f;
    public float MIDReach = 6.0f;
    public float LONGReach = 12.0f;
}
