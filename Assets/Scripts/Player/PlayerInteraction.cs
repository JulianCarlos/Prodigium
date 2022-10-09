using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    public RaycastHit HitInfo => currentHit;

    [SerializeField] private Player player;
    [SerializeField] private bool hasTarget;

    [SerializeField] private float interactionRaycastLength = 3;

    [SerializeField] private TextMeshProUGUI interactionLookDescription;

    [SerializeField] private Camera playerCamera;

    private RaycastHit currentHit;

    private void Awake()
    {
        currentHit = new RaycastHit();
    }

    private void Start()
    {
        PlayerInputs.InputAction.Player.PickUp.started += InteractionUse;
    }

    private void Update()
    {
        hasTarget = Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out currentHit, interactionRaycastLength);
    }

    private void FixedUpdate()
    {
        SetInteractionText();
    }

    private void SetInteractionText()
    {
        if (hasTarget && currentHit.collider.transform.TryGetComponent(out IInteractable interactable))
        {
            interactionLookDescription.text = interactable.ReturnInteractableText();
        }
        else
        {
            interactionLookDescription.text = string.Empty;
        }
    }

    private void InteractionUse(InputAction.CallbackContext context)
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
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * interactionRaycastLength);
    }
}
