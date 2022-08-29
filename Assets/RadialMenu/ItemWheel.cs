using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWheel : MonoBehaviour
{
    //Color
    [SerializeField] private Color normalColor;
    [SerializeField] private Color highlightColor;

    //Wheel Settings
    [SerializeField] private float segmentGap;

    //Lists
    //[SerializeField] private RingSegment[] segmentData;
    [SerializeField] private RingCakePiece[] segments;

    //Prefab
    [SerializeField] private RingCakePiece ringCakePiecePrefab;

    //private void Start()
    //{
    //    GenerateSegments();
    //}

    private void GenerateSegments()
    {
        var stepLength = 360 / segments.Length;
        var iconDist = Vector3.Distance(ringCakePiecePrefab.Icon.transform.position, ringCakePiecePrefab.CakePiece.transform.position);

        //segments = new RingCakePiece[segmentData.Length];

        for (int i = 0; i < segments.Length; i++)
        {
            segments[i] = Instantiate(ringCakePiecePrefab, transform);

            //Set root element
            segments[i].transform.localPosition = Vector3.zero;
            segments[i].transform.localRotation = Quaternion.identity;

            //Set cake piece
            segments[i].CakePiece.fillAmount = 1f / segments.Length - segmentGap / 360;
            segments[i].CakePiece.transform.localPosition = Vector3.zero;
            segments[i].CakePiece.transform.localRotation = Quaternion.Euler(0, 0, stepLength / 2f + segmentGap / 2 + i * stepLength - segmentGap);
            segments[i].CakePiece.color = new Color(1, 1, 1, 0.5f);

            //Set Icon
            segments[i].Icon.transform.localPosition = segments[i].CakePiece.transform.localPosition + Quaternion.AngleAxis(i * stepLength, Vector3.forward) * Vector3.up * iconDist;
            //segments[i].Icon.sprite = segments[i].Icon;
        }
    }

    private void Update()
    {
        var stepLength = 360f / segments.Length;
        var mouseAngle = NormalizeAngle(Vector3.SignedAngle(Vector3.up, Input.mousePosition - new Vector3(Screen.width / 2, Screen.height / 2), Vector3.forward) + stepLength / 2f);
        var activeElement = (int)(mouseAngle / stepLength);

        for (int i = 0; i < segments.Length; i++)
        {
            if (TargetIsValid(activeElement, i))
                segments[i].CakePiece.color = highlightColor;
            else
                segments[i].CakePiece.color = normalColor;
        }
    }

    private bool TargetIsValid(int activeElement, int i)
    {
        return i == activeElement && Vector2.Distance(Input.mousePosition, transform.position) <= segments[i].CakePiece.sprite.rect.width / 4 && Vector2.Distance(Input.mousePosition, transform.position) >= 250;
    }

    private float NormalizeAngle(float a) => (a + 360f) % 360f;

    [ContextMenu("Generate Wheel")]
    public void GenerateWheel()
    {
        for (int i = this.transform.childCount; i > 0; --i)
            DestroyImmediate(this.transform.GetChild(0).gameObject);

        GenerateSegments();
    }
}
