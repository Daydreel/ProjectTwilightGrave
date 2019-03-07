using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private const float Y_ANGLE_MIN = 0.0f;
    private const float Y_ANGLE_MAX = 50.0f;

    Camera playerCamera;
    GameObject player;

    Transform camTransform;
    Transform playerTransform;

    public float CameraLength = 10;
    public float CameraSpeed = 1;
    public float Damping = 1;

    float distance = 10.0f;
    float currentX = 0.0f;
    float currentY = 0.0f;

    Rigidbody camBody;

    // Use this for initialization
    void Start () {

        camBody = GetComponent<Rigidbody>();

        playerCamera = Camera.main;
        camTransform = playerCamera.transform;

        currentX = camTransform.position.x;
        currentY = camTransform.position.y;

        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;

        if(camBody != null)
        {
            camBody.freezeRotation = true;
        }
    }
	
	// Update is called once per frame
	void Update () {

	}

    private void LateUpdate()
    {
        //Get Camera Input
        currentX += Input.GetAxis("CameraHorizontal");
        currentY += Input.GetAxis("CameraVertical");

        currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);

        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);

        transform.position = playerTransform.position - rotation * (Vector3.forward * distance); // Make the camera flicker
        transform.rotation = rotation;

        transform.LookAt(playerTransform.position, Vector3.up);
    }
}
