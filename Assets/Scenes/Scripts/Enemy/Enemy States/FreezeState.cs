using System.Collections;
using UnityEngine;
using UnityEngine.AI;


public class FreezeState: EnemyBaseState
{
    private static readonly int Idle = Animator.StringToHash("idle");
    private Coroutine graceCoroutine;
    
    private float gazeTimer = 0f;
    private float requiredGazeTime = 5f;

    private float graceTimer = 0f;
    private float graceDuration = 5f;
    
    private Vector3 lastAgentVelocity;
    private NavMeshPath lastAgentPath;
    
    public override void EnterState(EnemyBehaviour context)
    {
        gazeTimer = 0f;
        context.animator.SetInteger(Idle, 2);
        
        lastAgentVelocity = context.GetNavAgent().velocity;
        lastAgentPath = context.GetNavAgent().path;
        context.GetNavAgent().velocity = Vector3.zero;
        context.GetNavAgent().ResetPath();
        
    }
        
    public override void UpdateState(EnemyBehaviour context)
    {
        gazeTimer += Time.deltaTime;

        // If the gaze timer reaches the required time, teleport
        if (gazeTimer >= requiredGazeTime)
        {
            context.SwitchState(context.teleportState);
            Debug.Log("tp!");
        }

        // If the player stops gazing, start the grace period coroutine
        if (!context.IsGazingAtEnemy())
        {
            if (graceCoroutine == null)
            {
               // graceCoroutine = context.StartCoroutine(GracePeriod(context));
            }
            
            context.SwitchState(context.chaseState);
        }
    }

    public override void ExitState(EnemyBehaviour context)
    {
        context.GetNavAgent().velocity = lastAgentVelocity;
        context.GetNavAgent().SetPath(lastAgentPath);
    }
    
    private IEnumerator GracePeriod(EnemyBehaviour context)
    {
        Debug.Log("Started grace period...");
        float elapsed = 0f;

        while (elapsed < graceDuration)
        {
            if (context.IsGazingAtEnemy()) // If the player looks back before grace period ends
            {
                Debug.Log("Player looked back during grace period");
                yield break; // Cancel grace period and stop coroutine
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Grace period expired — reset the gaze timer
        Debug.Log("Grace period ended — gaze timer reset");
        gazeTimer = 0f;
        graceCoroutine = null;
    }
}
