using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DayFSM/Actions/Run")]
public class Run : Action
{
    //The direction vector of the run
    Vector3 moveDirection;

    public override void Act(DayFSM fsm)
    {
        PlayerMovement(fsm);
    }

    void PlayerMovement(DayFSM fsm)
    {
        Camera cam;

        cam = fsm.entityB.cam;

        //Check if there is a camera
        if (cam == null)
        {
            Debug.LogError("There is no camera attached to => Action/Run");
            return;
        }

        //Player Input
        moveDirection.x = Input.GetAxis("Horizontal");
        moveDirection.z = Input.GetAxis("Vertical");

        //Detach local transform to world transform | Make the movement relative to camera and not to the player's mesh
        moveDirection = cam.transform.TransformDirection(moveDirection);
        moveDirection.y = 0;
        
        //Get all the components of the player
        Transform transform = fsm.entityB.transform;
        Rigidbody body = fsm.entityB.body;
        //Get the speed from entityS ScriptableObject
        float speed = fsm.entityB.entityS.MoveSpeed;
        float rotationSpeed = fsm.entityB.entityS.rotationSpeed;
        //Make the player turn
        if (moveDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection, Vector3.up), Time.deltaTime * rotationSpeed);
        }

        //Make the player move
        body.MovePosition(body.position + moveDirection * speed * Time.deltaTime);

    }
}
