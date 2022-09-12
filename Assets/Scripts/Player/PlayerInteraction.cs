using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInteraction : MonoBehaviour
{
    public RaycastHit HitInfo => hitInfo;

    private bool hasTarget;

    private PlayerInputs playerInputs;
    private RaycastHit hitInfo;
    [SerializeField] private Camera playerCamera;

    private void Awake()
    {
        playerInputs = GetComponentInChildren<PlayerInputs>();
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

        var Interact = HitInfo.collider.transform.GetComponent<IInteractable>();
        if (Interact != null)
        {
            if (playerInputs.PlayerInputAction.Player.PickUp.WasPressedThisFrame())
            {
                Interact.EnterTerminal();
            }
        }
    }   
}
