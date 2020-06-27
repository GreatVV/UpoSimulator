using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public Transform camTrans;
    public Transform pivot;
    public Transform Character;
    public Transform mTransform;

    public CharacterStatus characterStatus;
    public CameraConfig cameraConfig;
    public bool leftPivot;
    public float delta;

    public float mouseX;
    public float mouseY;
    public float smoothX;
    public float smoothY;
    public float smoothXVelocity;
    public float smoothYVelocity;
    public float lookAngle;
    public float titlAngel;

    private void Update()
    {
        FixedTick();
    }

    private void FixedTick()
    {
        delta = Time.deltaTime;

        HandlePosition();
        HandleRotation();

        Vector3 targetPosition = Vector3.Lerp(mTransform.position, Character.position, 1);
        mTransform.position = targetPosition;
    }

    void HandlePosition()
    {
        float targetX = cameraConfig.normalX;
        float targetY = cameraConfig.normalY;
        float targetZ = cameraConfig.normalZ;

        Vector3 newPivotPosition = pivot.localPosition;
        newPivotPosition.x = targetX;
        newPivotPosition.y = targetY;
        
        Vector3 newCameraPosition = camTrans.localPosition;
        newCameraPosition.z = targetZ;

        float t = delta * cameraConfig.pivotSpeed;
        pivot.localPosition = Vector3.Lerp(pivot.localPosition, newPivotPosition, t);
        camTrans.localPosition = Vector3.Lerp(camTrans.localPosition, newCameraPosition, t);

    }
    void HandleRotation()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        if (cameraConfig.turnSmooth > 0)
        {
            smoothX = Mathf.SmoothDamp(smoothX, mouseX, ref smoothXVelocity, cameraConfig.turnSmooth);
            smoothY = Mathf.SmoothDamp(smoothY, mouseY, ref smoothYVelocity, cameraConfig.turnSmooth);
        }
        else
        {
            smoothX = mouseX;
            smoothY = mouseY;
        }

        lookAngle += smoothX * cameraConfig.yRotSpeed;
        Quaternion targetRot = Quaternion.Euler(0, lookAngle, 0);
        mTransform.rotation = targetRot;

        titlAngel -= smoothY * cameraConfig.yRotSpeed;
        titlAngel = Mathf.Clamp(titlAngel, cameraConfig.minAngle, cameraConfig.maxAngle);
        pivot.localRotation = Quaternion.Euler(titlAngel, 0, 0);
    }

}
