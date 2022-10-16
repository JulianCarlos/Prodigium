using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Vehicle : MonoBehaviour, IInteractable
{
    public CinemachineVirtualCamera VehicleCamera { get; private set; }

    public bool CanBeEntered => canBeEntered;

    //UI
    [SerializeField, TextArea] protected string interactableDescription;

    [SerializeField] protected bool canBeEntered;

    //Stats
    [Header("Stats")]
    [SerializeField] protected float speed;
    [SerializeField] protected float drag;

    [SerializeField] protected Transform exitPosition;

    private Transform playerInVehicle;

    private PlayerInputAction inputAction;

    private void Awake()
    {
        VehicleCamera = GetComponentInChildren<CinemachineVirtualCamera>();

        inputAction = new();

        inputAction.Vehicle.E_Use.started += ExitVehicle;
    }

    public virtual string ReturnInteractableText()
    {
        return interactableDescription;
    }

    public virtual void Use(Player player)
    {
        if(canBeEntered)
            EnterVehicle(player);
    }

    protected virtual void EnterVehicle(Player player)
    {
        inputAction.Vehicle.Enable();

        VehicleCamera.Priority = PlayerInteraction.PlayerVirtualCamera.Priority + 1;

        playerInVehicle = player.transform.root;
        playerInVehicle.gameObject.SetActive(false);
    }

    protected virtual void ExitVehicle(InputAction.CallbackContext context)
    {
        inputAction.Vehicle.Disable();

        VehicleCamera.Priority = PlayerInteraction.PlayerVirtualCamera.Priority - 1;

        playerInVehicle.gameObject.SetActive(true);
        playerInVehicle.transform.position = transform.right * 2;
        playerInVehicle = null;
    }
}
