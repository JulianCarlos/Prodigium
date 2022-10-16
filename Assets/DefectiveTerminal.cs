using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefectiveTerminal : MonoBehaviour, IInteractable
{
    [SerializeField, TextArea] private string interactableDescription;

    public string ReturnInteractableText()
    {
        return interactableDescription;
    }

    public void Use(Player player)
    {

    }
}
