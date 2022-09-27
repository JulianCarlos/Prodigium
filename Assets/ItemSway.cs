using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSway : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Camera playerCamera;

    [SerializeField] private Transform cameraPivot;

    [SerializeField] private float smoothRotateSpeed;

    void LateUpdate()
    {
        transform.position = cameraPivot.position;
        RotateTowardsCamera();
    }

    private void RotateTowardsCamera()
    {
        Quaternion currentRot = transform.rotation;
        Quaternion desiredRot = cameraPivot.rotation;

        transform.rotation = Quaternion.Slerp(currentRot, desiredRot, Time.deltaTime * smoothRotateSpeed);
    }
}
