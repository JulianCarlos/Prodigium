using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWheel : MonoBehaviour
{
    [SerializeField] private Color normalColor;
    [SerializeField] private Color highlightColor;

    [SerializeField] private Ring data;
    [SerializeField] private RingCakePiece ringCakePiecePrefab;
    [SerializeField] private float segmentGap;

    [SerializeField] private RingCakePiece[] segments;

    private void Start()
    {
        var stepLength = 360 / data.Segments.Length;
        var iconDist = Vector3.Distance(ringCakePiecePrefab.Icon.transform.position, ringCakePiecePrefab.CakePiece.transform.position);

        segments = new RingCakePiece[data.Segments.Length];

        for (int i = 0; i < data.Segments.Length; i++)
        {
            segments[i] = Instantiate(ringCakePiecePrefab, transform);
            //Set root element
            segments[i].transform.localPosition = Vector3.zero;
            segments[i].transform.localRotation = Quaternion.identity;

            //Set cake piece
            segments[i].CakePiece.fillAmount = 1f / data.Segments.Length - segmentGap / 360;
            segments[i].CakePiece.transform.localPosition = Vector3.zero;
            segments[i].CakePiece.transform.localRotation = Quaternion.Euler(0, 0, stepLength / 2f + segmentGap / 2 + i * stepLength - segmentGap);
            segments[i].CakePiece.color = new Color(1, 1, 1, 0.5f);    

            //Set Icon
            segments[i].Icon.transform.localPosition = segments[i].CakePiece.transform.localPosition + Quaternion.AngleAxis(i * stepLength, Vector3.forward) * Vector3.up * iconDist;
            segments[i].Icon.sprite = data.Segments[i].Icon;
        }
    }

    private void Update()
    {
        var stepLength = 360f / data.Segments.Length;
        var mouseAngle = NormalizeAngle(Vector3.SignedAngle(Vector3.up, Input.mousePosition - new Vector3(Screen.width / 2, Screen.height / 2), Vector3.forward) + stepLength / 2f);
        var activeElement = (int)(mouseAngle / stepLength);

        for (int i = 0; i < data.Segments.Length; i++)
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
}
