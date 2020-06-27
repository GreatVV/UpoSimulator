using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBall : MonoBehaviour
{
    public Transform cameraTransform;
    public CharacterStatus characterStatus;

    public Vector3 rotationDirection;
    public Vector3 moveDirection;

    public float vertical;
    public float horizontal;
    public float moveAmount;

    public float speed;
    private Rigidbody rb;

    

        
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
      //  moveAmount = Mathf.Clamp01(Mathf.Abs(vertical) + Mathf.Abs(horizontal));
        


     
        Vector3 movement = new Vector3(horizontal * speed, rb.velocity.y, vertical * speed);
        // rb.AddForce(movement);
        rb.velocity = movement;


        Vector3 moveDir = cameraTransform.forward * vertical;
        moveDir += cameraTransform.right * horizontal;
        moveDir.Normalize();
        moveDirection = moveDir;
        rotationDirection = cameraTransform.forward;

        RotationNormal();
    }

    public void RotationNormal()
    {
        rotationDirection = moveDirection;

        Vector3 targetDir = rotationDirection;
        targetDir.y = 0;

        if (targetDir == Vector3.zero)
        {
            targetDir = transform.forward;
        }

        Quaternion lookDir = Quaternion.LookRotation(targetDir);
        Quaternion targetRot = Quaternion.Slerp(transform.rotation, lookDir, 0.00001f);
        transform.rotation = targetRot;
    }
}
