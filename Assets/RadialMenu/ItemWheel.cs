using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class ItemWheel : MonoBehaviour
{
    //Color
    [Header("Color Settings")]
    [SerializeField] private Color normalColor;
    [SerializeField] private Color highlightColor;

    //Wheel Settings
    [Header("Wheel Settings")]
    [SerializeField] private float segmentGap;
    [SerializeField] private float iconDistanceOffset;

    //Lists
    [Header("Segments")]
    [SerializeField] private ItemWheelSegment[] segments;
    [SerializeField] private ItemWheelSegment currentSegment;
    [SerializeField] private ItemWheelSegment previousSegment;

    //Prefab
    [Header("Prefabs")]
    [SerializeField] private ItemWheelSegment ringCakePiecePrefab;

    [Space]
    [SerializeField, Range(2, 15)] private uint numberOfSegments = 5;

    private void Update()
    {
        var stepLength = 360f / segments.Length;
        var mouseAngle = NormalizeAngle(Vector3.SignedAngle(Vector3.up, Input.mousePosition - new Vector3(Screen.width / 2, Screen.height / 2), Vector3.forward) + stepLength / 2f);
        var activeElement = (int)(mouseAngle / stepLength);

        currentSegment = segments[activeElement];

        if (previousSegment != currentSegment)
        {
            if(previousSegment != null)
                previousSegment.CakePiece.color = normalColor;

            currentSegment.CakePiece.color = highlightColor;
        }

        if (PlayerInputs.Instance.PlayerInputAction.Player.LeftClick.WasPressedThisFrame())
        {
            PlayerCanvasController.Instance.CloseItemWheel();

            PlayerInventory.Instance.InstantiateItem(currentSegment.CurrentSelectedItem);
        }

        previousSegment = currentSegment;
    }

    private void GenerateSegments()
    {
        segments = new ItemWheelSegment[numberOfSegments];

        float stepLength = 360f / segments.Length;
        var iconDist = Vector3.Distance(ringCakePiecePrefab.Icon.transform.position, ringCakePiecePrefab.CakePiece.transform.position) + iconDistanceOffset;

        for (int i = 0; i < segments.Length; i++)
        {
            segments[i] = Instantiate(ringCakePiecePrefab, transform);

            //Set root element
            segments[i].transform.localPosition = Vector3.zero;
            segments[i].transform.localRotation = Quaternion.identity;

            //Set cake piece
            segments[i].CakePiece.fillAmount = 1f / segments.Length - segmentGap / 360;
            segments[i].CakePiece.transform.localPosition = Vector3.zero;
            segments[i].CakePiece.transform.localRotation = Quaternion.Euler(0, 0, stepLength / 2f + segmentGap / 2f + i * stepLength - segmentGap);
            segments[i].CakePiece.color = new Color(1, 1, 1, 0.5f);

            //Set Icon
            segments[i].Icon.transform.localPosition = segments[i].CakePiece.transform.localPosition + Quaternion.AngleAxis(i * stepLength, Vector3.forward) * Vector3.up * iconDist;
        }
    }

    private float NormalizeAngle(float a) => (a + 360f) % 360f;

    [Button("Generate Wheel")]
    public void GenerateWheel()
    {
        if(transform.childCount > 0)
        {
            for (int i = this.transform.childCount; i > 0; --i)
                DestroyImmediate(this.transform.GetChild(0).gameObject);
        }

        GenerateSegments();
    }
}
