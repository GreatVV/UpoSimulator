using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class PlayerControl : MonoBehaviour {
    
    // Controll
    public float accelerationForce = 10f;
    public float dragDividend = 100f;
    private Rigidbody rigidBody;
    private Dictionary<KeyCode, Vector3> keyMappings;

    private Vector3 directionForce;
    public float JumpForce=250f;
    // Camera
    private GameObject camera;
    public CameraConfig cameraConfig;
    private LensDistortion lensDistortion;
    
    public void Start()
    {

        this.camera = Camera.main.transform.gameObject;
        this.rigidBody = GetComponent<Rigidbody>();

        // Get Lens distortion
        GameObject pp = GameObject.Find("Post Process Volume");
        
        if (pp) {
            LensDistortion tmp;
            pp.GetComponent<Volume>().profile.TryGet<LensDistortion>(out tmp);
            this.lensDistortion = tmp;
        }

    }

    public void Update()
    {


            
            float drag, force;
            if (Input.GetKey(KeyCode.LeftShift)) {
                drag = this.dragDividend * 8;
                force = this.accelerationForce * 2;
            if (cameraConfig.normalZ > -8)
            {
                cameraConfig.normalZ -= Time.deltaTime;
            }
            
            } else {
                cameraConfig.normalZ = -6;
                drag = this.dragDividend;
                force = this.accelerationForce;
            }
            
            // Define directions
            this.keyMappings = new Dictionary<KeyCode, Vector3>()
            {
                {KeyCode.W, camera.transform.TransformDirection(new Vector3(0, 0, 2))},
                {KeyCode.A, camera.transform.TransformDirection(new Vector3(-1, 0, 0))},
                {KeyCode.S, camera.transform.TransformDirection(new Vector3(0, 0, -1))},
                {KeyCode.D, camera.transform.TransformDirection(new Vector3(1, 0, 0))},
                
            };
            
            // Control
            foreach (KeyValuePair<KeyCode, Vector3> keyMapping in this.keyMappings)
            {
                if (Input.GetKey(keyMapping.Key) && this.IsGrounded())
                {
                    Vector3 objectForce = keyMapping.Value * force;
                    this.rigidBody.drag = objectForce.sqrMagnitude / drag;
                    this.rigidBody.AddForce(objectForce);
                directionForce = objectForce;
            }

            }
            
            // Max speed
            if(this.rigidBody.velocity.magnitude > 20) {
                this.rigidBody.velocity = this.rigidBody.velocity.normalized * 20;
            }

            // Lens distortion
            this.lensDistortion.intensity.value = -this.rigidBody.velocity.magnitude / 35;


        if (Input.GetKeyDown(KeyCode.Space) && Physics.Raycast(this.transform.position, -Vector3.up, 0.65f))
        {
            this.rigidBody.AddForce(Vector3.up * JumpForce);
           // this.rigidBody.AddForce(Vector3.up+Vector3.forward, ForceMode.Impulse);
        }
    }
    
    public bool IsGrounded() {
        return Physics.Raycast(this.transform.position, -Vector3.up, 0.65f);
    }
    
    public void Jump()
    {
        rigidBody.AddForce(Vector3.up, ForceMode.Impulse);
     //   rigidBody.velocity = Vector3. * 5;
    }
}