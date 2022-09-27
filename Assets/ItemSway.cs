using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSway : MonoBehaviour
{
    [SerializeField] private Player player;

    [SerializeField] private Transform cameraHolderTransform;

    [SerializeField] private float smoothRotateSpeed;

    private float cameraToBodyDistance;

    private void Awake()
    {
        cameraToBodyDistance = transform.position.y - player.gameObject.transform.position.y;
    }

    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + cameraToBodyDistance, player.transform.position.z);

        RotateTowardsCamera();
    }

    private void RotateTowardsCamera()
    {
        Quaternion currentRot = transform.rotation;
        Quaternion desiredRot = cameraHolderTransform.rotation;

        transform.rotation = Quaternion.Slerp(currentRot, desiredRot, Time.deltaTime * smoothRotateSpeed);
    }
}
