using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "DayFSM/Data/entityS")]

public class EntityStats : ScriptableObject {
    //Player Statistics
    //Player hit points
    public int MaxHitPoints = 1000; //TODO
    public int CurrentHitPoints;//TODO
    //Player Retry points
    public int MaxRetryPoints = 5;//TODO
    public int CurrentRetryPoints;//TODO
    //Player Attack related stats
    public int Strength = 10; //TODO // Global player stats that modify damage
    public float AttackSpeed = 1; //TODO // Global player's attack speed modifier

    //Furie Bar
    public float MaxFurie = 100.0f;//TODO
    public float CurrentFurie;//TODO

    //Magic Points
    public float MaxMagic = 300.0f;//TODO
    public float CurrentMagic;//TODO

    //Player movement related stats
    //Movement
    public float MoveSpeed = 5.0f;//TODO
    //Jump
    public float JumpHeight = 10.0f;//TODO
    //public float JumpSpeed = 10.0f;

    //Rotation speed
    public float rotationSpeed = 2.0f;

    //Dodge stats
    public float dodgeDistance = 5.0f;
}
