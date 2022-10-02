using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    private Vector3 closedDoorRotation;
    private Vector3 openDoorRotation;

    private bool isInUse = false;
    private bool isOpen = false;

    private void Start()
    {
        closedDoorRotation = transform.localRotation.eulerAngles;
    }

    public void Use(Player player)
    {
        ToggleDoor(player);
    }

    private void ToggleDoor(Player player)
    {
        if (isInUse)
            return;

        isInUse = true;

        if (isOpen)
            StartCoroutine(nameof(CloseDoor));
        else if (!isOpen)
            StartCoroutine(nameof(OpenDoor));
    }

    private IEnumerator OpenDoor()
    {
        Debug.Log("open door");
        yield return null;
        isOpen = true;
        isInUse=false;
    }

    private IEnumerator CloseDoor()
    {
        Debug.Log("close door");
        yield return null;
        isOpen = false;
        isInUse = false;
    }
}
