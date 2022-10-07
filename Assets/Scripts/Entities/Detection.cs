using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Detection : MonoBehaviour
{
    public List<Collider> DetectedEntities { get; private set; } = new();

    [SerializeField] private float detectionInterval;
    [SerializeField] private LayerMask detectionLayer;
    [SerializeField] private LayerMask obstructionLayer;

    [Header("Radius Settings")]
    [Range(1f, 25f)][SerializeField] private float minRadius;
    [Range(5f, 25f)][SerializeField] private float wanderRadius;
    [Range(5f, 50f)][SerializeField] private float chaseRadius;
    [Range(5f, 50f)][SerializeField] private float scoutRadius;

    [Header("Angle Settings")]
    [Range(0, 360f)][SerializeField] private float wanderAngle;
    [Range(0, 360f)][SerializeField] private float scoutAngle;
    [Range(0, 360f)][SerializeField] private float chaseAngle;
    [SerializeField] private float angle;
    private MonsterAI monsterAi;

    private WaitForSeconds detectionIntervalWait;

    private void Awake()
    {
        monsterAi = GetComponent<MonsterAI>();
        detectionIntervalWait = new WaitForSeconds(detectionInterval);
    }

    private void Start()
    {
        StartCoroutine(nameof(CheckForEntities));
    }

    private IEnumerator CheckForEntities()
    {
        while (true)
        {
            var radius = (monsterAi.StateMachine.CurrentState == monsterAi.ChaseState) ? chaseRadius :
                (monsterAi.StateMachine.CurrentState == monsterAi.ScoutState) ? scoutRadius : wanderRadius;

            DetectedEntities = Physics.OverlapSphere(gameObject.transform.position, minRadius, detectionLayer).ToList();
            var AngleDetected = Physics.OverlapSphere(gameObject.transform.position, radius, detectionLayer);

            Debug.Log("radius = " + radius);

            foreach (var entity in AngleDetected)
            {
                if (DetectedEntities.Contains(entity)) continue;
                var directionToTarget = (entity.transform.position - transform.position).normalized;
                var distanceToObject = Vector3.Distance(transform.position, entity.transform.position);

                angle = 
                    (monsterAi.StateMachine.CurrentState == monsterAi.ChaseState) ? chaseAngle : 
                    (monsterAi.StateMachine.CurrentState == monsterAi.ScoutState) ? scoutAngle : wanderAngle;

                Debug.Log("angle = " + angle);
                if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
                {
                    //NEEDS RAYCAST CORRECTION
                    if (!Physics.Raycast(transform.position, directionToTarget, distanceToObject, obstructionLayer))
                    {
                        DetectedEntities.Add(entity);
                    }
                }
            }

            yield return detectionIntervalWait;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(gameObject.transform.position, minRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(gameObject.transform.position, wanderRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(gameObject.transform.position, chaseRadius);
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(gameObject.transform.position, scoutRadius);
    }
}
