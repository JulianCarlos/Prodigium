using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

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

    private void Start()
    {
        PlayerInputs.InputAction.Player.PickUp.started += CheckForRaycastType;
    }

    private void Update()
    {
        hasTarget = Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hitInfo, 100);
    }

    private void CheckForRaycastType(InputAction.CallbackContext context)
    {
        Debug.Log("InteractPressed");

        if (context.started)
        {
            if (!hasTarget)
                return;

            var Interact = HitInfo.collider.transform.GetComponent<IInteractable>();
            if (Interact != null)
            {
                Interact.EnterTerminal();
            }
        }
    }   
}
