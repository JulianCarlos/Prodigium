using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInteraction : MonoBehaviour
{
    public RaycastHit HitInfo => hitInfo;

    [SerializeField] private LayerMask itemLayer = 1 << 9;
    [SerializeField] private LayerMask monsterLayer = 1 << 9;

    private bool hasTarget;

    private RaycastHit hitInfo;
    private Camera playerCamera;

    private void Awake()
    {
        playerCamera = GetComponentInChildren<Camera>();
        hitInfo = new RaycastHit();
    }

    private void Update()
    {
        hasTarget = Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hitInfo, 100);

        CheckForRaycastType();
    }

    private void CheckForRaycastType()
    {
        if (!hasTarget)
            return;

        if (hitInfo.transform.gameObject.layer == monsterLayer)
        {
            Debug.Log("You found a Item");
        }

        if(hitInfo.transform.gameObject.layer == ~monsterLayer)
        {
            Debug.Log("You found a Monster");
        }
    }
}
