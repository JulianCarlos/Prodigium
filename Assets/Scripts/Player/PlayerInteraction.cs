using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    public RaycastHit HitInfo => hitInfo;

    [SerializeField] private Player player;
    [SerializeField] private bool hasTarget;

    private Camera playerCamera;
    private RaycastHit hitInfo;

    private void Awake()
    {
        hitInfo = new RaycastHit();
        playerCamera = GetComponentInChildren<Camera>();
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
        if (context.started)
        {
            if (!hasTarget)
                return;

            var Interact = HitInfo.collider.transform.GetComponent<IInteractable>();
            if (Interact != null)
            {
                Interact.Use(player);
            }

            var DamageAble = HitInfo.collider.transform.GetComponent<IDamageable>();
            if (DamageAble != null)
            {
                Debug.Log("Damaged This Motherfuckeer");
            }
        }
    }   
}
