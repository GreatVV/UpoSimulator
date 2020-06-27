using System;
using UnityEngine.EventSystems;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    
    public float turnSpeed = 1f;
    public Vector3 offsetView = new Vector3(0f, 3f, -5f);
    
    private Vector3 offsetComputed;

    public Vector3 x = Vector3.up;
    public Vector3 y = Vector3.left;

    private GameObject camera;
    
    
    private void Start()
    {
        this.camera = Camera.main.transform.gameObject;
        offsetComputed = this.transform.position + offsetView;
    }

    private void Update()
    {
        
        offsetComputed = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed, this.camera.transform.TransformDirection (this.x)) * Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * turnSpeed, this.camera.transform.TransformDirection (this.y)) * offsetComputed;
        this.camera.transform.position = this.transform.position + offsetComputed;
        this.camera.transform.LookAt(this.transform.position);

    }
}