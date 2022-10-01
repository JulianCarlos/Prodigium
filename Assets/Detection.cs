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
    [Range(5f, 25f)][SerializeField] private float maxRadius;
    [Range(1f, 25f)][SerializeField] private float minRadius;
    [Range(5f, 50f)][SerializeField] private float chaseRadius;

    [Range(0, 360f)][SerializeField] private float wanderAngle;
    [Range(0, 360f)][SerializeField] private float scoutAngle;
    [Range(0, 360f)][SerializeField] private float chaseAngle;

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
            DetectedEntities = Physics.OverlapSphere(gameObject.transform.position, minRadius, detectionLayer).ToList();
            var farDetected = Physics.OverlapSphere(gameObject.transform.position, maxRadius, detectionLayer);
            var chaseDetected = Physics.OverlapSphere(transform.position, chaseRadius, detectionLayer);

            if(monsterAi.StateType == StateType.Wandering)
            {
                foreach (var entity in farDetected)
                {
                    if (DetectedEntities.Contains(entity)) continue;
                    var directionToTarget = (entity.transform.position - transform.position).normalized;
                    var distanceToObject = Vector3.Distance(transform.position, entity.transform.position);
                    if (Vector3.Angle(transform.forward, directionToTarget) < wanderAngle / 2)
                    {
                        //NEEDS RAYCAST CORRECTION
                        if (!Physics.Raycast(transform.position, directionToTarget, distanceToObject, obstructionLayer))
                        {
                            DetectedEntities.Add(entity);
                        }
                    }
                }
            }

            if (monsterAi.StateType == StateType.Chasing || monsterAi.StateType == StateType.Fleeing || monsterAi.StateType == StateType.Scouting)
            {
                foreach (var entity in chaseDetected)
                {
                    if (DetectedEntities.Contains(entity)) continue;
                    var directionToTarget = (entity.transform.position - transform.position).normalized;
                    var distanceToObject = Vector3.Distance(transform.position, entity.transform.position);
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
        Gizmos.DrawWireSphere(gameObject.transform.position, maxRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(gameObject.transform.position, chaseRadius);
    }
}
