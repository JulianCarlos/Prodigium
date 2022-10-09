using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Vehicle : MonoBehaviour, IInteractable
{
    //UI
    [SerializeField, TextArea] protected string interactableDescription;

    //Stats
    [Header("Stats")]
    [SerializeField] protected float speed;
    [SerializeField] protected float drag;

    [SerializeField] protected Transform exitPosition;

    private Transform playerInVehicle;


    public virtual string ReturnInteractableText()
    {
        return interactableDescription;
    }

    public virtual void Use(Player player)
    {
        EnterVehicle(player);
    }
    
    protected virtual void EnterVehicle(Player player)
    {
        playerInVehicle = player.transform.root;
        playerInVehicle.gameObject.SetActive(false);
    }

    protected virtual void ExitVehicle(InputAction.CallbackContext context)
    {
        playerInVehicle.gameObject.SetActive(true);
        playerInVehicle = null;
    }
}
