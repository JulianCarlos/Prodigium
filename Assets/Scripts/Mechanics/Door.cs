using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private float openAngle;
    [SerializeField] private float smooth;

    [SerializeField, TextArea] private string interactableDescription;

    [SerializeField] private AudioClip openingSound;
    [SerializeField] private AudioClip closingSound;

    private AudioSource doorAudioSource;

    private float targetYRotation;
    private float defaultYRotation = 0f;

    private bool isInUse = false;
    private bool isOpen = false;

    private void Awake()
    {
        doorAudioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        defaultYRotation = transform.eulerAngles.y;
    }

    public void Use(Player player)
    {
        ToggleDoor(player);
    }

    private void ToggleDoor(Player player)
    {
        if (isInUse)
            return;

        isOpen = !isOpen;
        isInUse = true;

        if (isOpen)
            StartCoroutine(nameof(OpenDoor), player);
        else if (!isOpen)
            StartCoroutine(nameof(CloseDoor));
    }

    private IEnumerator OpenDoor(Player player)
    {
        doorAudioSource.PlayOneShot(openingSound);
        Vector3 dir = (player.transform.position - transform.position);
        targetYRotation = -Mathf.Sign(Vector3.Dot(transform.right, dir)) * openAngle;

        while (transform.rotation != Quaternion.Euler(0, defaultYRotation + targetYRotation, 0))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, defaultYRotation + targetYRotation, 0f), smooth * Time.deltaTime);
            yield return null;
        }

        isInUse = false;
    }

    private IEnumerator CloseDoor()
    {
        doorAudioSource.PlayOneShot(closingSound);
        targetYRotation = 0f;

        while (transform.rotation != Quaternion.Euler(0, defaultYRotation + targetYRotation, 0))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, defaultYRotation + targetYRotation, 0f), smooth * Time.deltaTime);
            yield return null;
        }

        isInUse = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.right * 3);
    }

    public string ReturnInteractableText()
    {
        return interactableDescription;
    }
}
