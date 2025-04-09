using System.Collections.Generic;
using Scenes.Scripts.Enemy_States;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{ 
    
    public EnemyBaseState currentState;
    
    public ChaseState chaseState = new ChaseState();
    public IdleState IdleState = new IdleState();
    public TeleportState teleportState = new TeleportState();
    public PatrolState patrolState = new PatrolState();
    public FreezeState freezeState = new FreezeState();
    
    private NavMeshAgent _agent;
    private Transform _target;
    public float radius = 5f;
    
    [Header("Detection Settings")]
    [SerializeField] private float detectionDistance = 10f;
    [SerializeField] private float fieldOfViewAngle = 60f; // Total FOV in degrees
    [SerializeField] private Color debugRayColor = Color.red;
    

    [SerializeField]
    private float speed = 10;

    public string state = string.Empty;
    
    [SerializeField]
    List<Transform> waypoints;
        
    void Start()
    {
        _target = PlayerController.Instance.transform;
        _agent = GetComponent<NavMeshAgent>();    
        
        currentState = patrolState;
        currentState.EnterState(this);
    }


    void Update()
    {
        currentState.UpdateState(this);
        state = currentState.ToString();
    }

    public void SwitchState(EnemyBaseState state){
        
        currentState.ExitState(this);
        currentState = state;
        state.EnterState(this);
    }
    public bool LookForPlayer()
    {
        if (_target == null)
            return false;

        // Calculate direction from enemy to player
        Vector3 directionToPlayer = (_target.position - transform.position).normalized;
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

        // Draw debug rays for visualization
        float halfFOV = fieldOfViewAngle * 0.5f;
        Vector3 leftBoundary = Quaternion.Euler(0, -halfFOV, 0) * transform.forward;
        Vector3 rightBoundary = Quaternion.Euler(0, halfFOV, 0) * transform.forward;

        Debug.DrawRay(transform.position, leftBoundary * detectionDistance, Color.green);
        Debug.DrawRay(transform.position, rightBoundary * detectionDistance, Color.green);
        Debug.DrawRay(transform.position, transform.forward * detectionDistance, Color.red); // Forward vision ray

        // Check if the player is within the field of view
        if (angleToPlayer <= halfFOV)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, directionToPlayer, out hit, detectionDistance))
            {
                // Player is detected if the raycast hits them
                if (hit.transform == _target)
                {
                    return true;
                }
            }
        }

        return false;
    }
    public Transform GetPlayerTransform() => PlayerController.Instance.transform;
    public EnemyBaseState GetRandomState(float patrolWeight, float teleportWeight, float idleWeight)
   {
       float totalWeight = patrolWeight + teleportWeight + idleWeight;
       float randomValue = Random.value * totalWeight; // Random.value gives [0, 1)

       if (randomValue < patrolWeight)
       {
           return patrolState;
       }
       else if (randomValue < patrolWeight + teleportWeight)
       {
           return teleportState;
       }
       else
       {
           return IdleState;
       }
   }

    public bool DetectPlayerNearby()
    {
        
        if (_target != null)
        {
            float distance = Vector3.Distance(transform.position, _target.position);
            if (distance <= radius)
            {
                return true;
            }
        }
        return false;

    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
    
    public bool IsGazingAtEnemy()
    {
        var gazeTarget = GazeBehaviour.CurrentGazeTarget?.transform.parent;
        return gazeTarget == transform;
    }


    #region Navigation

   public Vector3 GetRandomWayPoint()
   {
       if (waypoints.Count <= 0) return Vector3.zero;
       int point = Random.Range(0, waypoints.Count);
       return waypoints[point].position;
   }
   
   public bool WaypointsExist() => waypoints.Count > 0;

   public NavMeshAgent GetNavAgent() => _agent;
   
   #endregion
}
