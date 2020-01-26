using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Forces you to have a character controller
[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviourPun
{
    [SerializeField] private float movementSpeed = 0f;

    private CharacterController controller = null;
    private Transform mainCameraTransform = null;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        mainCameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            TakeInput();
        }
    }

    public void TakeInput()
    {
        Vector3 movement = new Vector3
        {
            x = Input.GetAxisRaw("Horizontal"),
            y = 0f,
            z = Input.GetAxisRaw("Vertical")
        }.normalized;

        Vector3 forward = mainCameraTransform.forward;
        Vector3 right = mainCameraTransform.right;

        // Make sure that the camera does not care about the tilt of the player. Example: If we look down, the player won´t try to go into the ground
        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        // Move the way you are looking
        Vector3 calculatedMovement = (forward * movement.z + right * movement.x).normalized;

        if(calculatedMovement != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(calculatedMovement);
        }
        
        controller.SimpleMove(calculatedMovement * movementSpeed);
    }
}
