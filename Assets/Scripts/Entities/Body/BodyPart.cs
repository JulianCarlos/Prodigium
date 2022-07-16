using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BodyPart : MonoBehaviour
{
    public float Health { get => health; set => health = value; }

    [SerializeField] private float health;
}
