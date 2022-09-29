using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    private Quaternion closedDoorRotation;

    private bool isInUse = false;
    private bool isOpen = false;

    private void Start()
    {
        closedDoorRotation = transform.localRotation;
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
            StartCoroutine(nameof(OpenDoor));
        else if (!isOpen)
            StartCoroutine(nameof(CloseDoor));
    }

    private IEnumerator OpenDoor()
    {
        yield return null;
        isOpen = true;
    }

    private IEnumerator CloseDoor()
    {
        yield return null;
        isOpen = false;
    }
}
