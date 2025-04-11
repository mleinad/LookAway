using UnityEngine;
using UnityEngine.AI;

namespace Scenes.Scripts.Enemy_States
{
    public class PatrolState: EnemyBaseState
    {
        private static readonly int Motion1 = Animator.StringToHash("Motion");
        
        private static readonly int BlendX = Animator.StringToHash("x");
        private static readonly int BlendY = Animator.StringToHash("y");
        
        
        
        private Vector3 target;
        private NavMeshAgent agent;

        
        
        private Vector2 targetBlend; // Desired blend tree target
        private Vector2 currentBlend; // Current blend position
        
        private float speed;
        private float blendLerpSpeed = 0.5f;
        
        
        public override void EnterState(EnemyBehaviour context)
        {
            agent = context.GetNavAgent();
            target = context.GetRandomWayPoint();
            agent.SetDestination(target);
            context.animator.SetTrigger(Motion1);
            
            targetBlend = new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f));
            currentBlend = targetBlend;
            
            context.animator.SetFloat(BlendX, currentBlend.x);
            context.animator.SetFloat(BlendY, currentBlend.y);
        }

        public override void UpdateState(EnemyBehaviour context)
        {
            currentBlend = Vector2.Lerp(currentBlend, targetBlend, blendLerpSpeed * Time.deltaTime);
            context.animator.SetFloat(BlendX, currentBlend.x);
            context.animator.SetFloat(BlendY, currentBlend.y);
            
            
            if (context.LookForPlayer()||context.DetectPlayerNearby(context.detectRadius))
            {
                context.SwitchState(context.chaseState);
            }
            
            
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                EnemyBaseState nextState = context.GetRandomState(
                    0.6f,
                    0.3f,
                    0.1f);
                context.SwitchState(nextState);
            }
            
            
            if (context.IsGazingAtEnemy())
            {
                context.SwitchState(context.freezeState);
            }
        }

        public override void ExitState(EnemyBehaviour context)
        {
            agent.ResetPath();
        }
        
        
        
    }
}