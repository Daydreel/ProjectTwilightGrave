using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum DistanceType
{
    CAC,
    MID,
    LONG
}

public enum BossAngerMoveSet
{
    Null,
    Run,
    Combo1,
    Combo2,
    Combo3
}

[RequireComponent(typeof(BossBehaviour))]
public class BossBehaviour : MonoBehaviour
{
    //Get reference from the player
    public GameObject target;

    [HideInInspector]
    public NavMeshAgent navMeshAgent;

    private Animator animator;

    public BossStats bossStats;

    //The state machine reference
    public DayFSM fsm;

    //Input buffer recorded for some milliseconds
    public DayInputBuffer inputBuffer;

    [HideInInspector]
    public Camera cam;

    [HideInInspector]
    public Rigidbody body;

    public LayerMask groundLayer;

    protected bool isGrounded = true;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        cam = Camera.main;
        body = GetComponent<Rigidbody>();
        inputBuffer = new DayInputBuffer();
        navMeshAgent = GetComponent<NavMeshAgent>();

        if (fsm != null)
        {
            //Give player animator to state fsm
            fsm.animator = animator;
            fsm.bossB = this;
            fsm.Start();

            Debug.Log(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        inputBuffer.Listen();
        fsm.Update();
        //Debug.Log("IsGrounded: " + CheckIsGround());
    }

    void FixedUpdate()
    {
        fsm.FixedUpdate();
    }

    public bool CheckIsGround()
    {
        isGrounded = Physics.CheckSphere(transform.position + Vector3.up * 0.3f, 0.5f, groundLayer.value); //Need to check Layer Mask
        return isGrounded;
    }

    public float TargetDistance()
    {
        float value = 0;
        value = Vector3.Distance(transform.position, target.transform.position);

        return value;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, bossStats.CACReach);
        Gizmos.DrawWireSphere(transform.position, bossStats.MIDReach);
        Gizmos.DrawWireSphere(transform.position, bossStats.LONGReach);

        Gizmos.DrawRay(transform.position,transform.forward * bossStats.LONGReach);
    }
}
