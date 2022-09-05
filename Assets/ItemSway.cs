using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSway : MonoBehaviour
{
    [SerializeField] private Transform cameraHolderTransform;

    [SerializeField] private float smoothRotateSpeed;

    void Update()
    {
        RotateTowardsCamera();
    }

    private void RotateTowardsCamera()
    {
        Quaternion currentRot = transform.rotation;
        Quaternion desiredRot = cameraHolderTransform.rotation;

        transform.rotation = Quaternion.Slerp(currentRot, desiredRot, Time.deltaTime * smoothRotateSpeed);
    }
}
