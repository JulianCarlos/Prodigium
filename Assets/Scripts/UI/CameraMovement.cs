using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float randomPositionOffset;

    private Vector3 startingPos;
    [SerializeField] private Vector3 targetPos;

    private void Awake()
    {
        startingPos = transform.position;
    }

    void Start()
    {
        Vector3 target = startingPos + Random.insideUnitSphere * randomPositionOffset;

        targetPos = new Vector3(transform.position.x, target.y, target.x);

        MoveCamera();
    }

    private void MoveCamera()
    {
        DOTween.To(() => transform.position, x => transform.position = x, targetPos, 10f).SetEase(Ease.Linear).OnComplete(() => CalculateNewTarget());
    }

    private void CalculateNewTarget()
    {
        transform.DOKill();

        Vector3 target = startingPos + Random.insideUnitSphere * randomPositionOffset;

        targetPos = new Vector3(transform.position.x, target.y, target.x);

        MoveCamera();
    }
}
