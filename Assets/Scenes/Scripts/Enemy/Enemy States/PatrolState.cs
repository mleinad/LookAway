using UnityEngine;
using UnityEngine.AI;

namespace Scenes.Scripts.Enemy_States
{
    public class PatrolState: EnemyBaseState
    {
        private Vector3 target;
        private NavMeshAgent agent;

        private float speed;
        
        public override void EnterState(EnemyBehaviour context)
        {
            agent = context.GetNavAgent();
            target = context.GetRandomWayPoint();
            agent.SetDestination(target);
        }

        public override void UpdateState(EnemyBehaviour context)
        {
            
            if (context.LookForPlayer()||context.DetectPlayerNearby())
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
        }

        public override void ExitState(EnemyBehaviour context)
        {
            agent.ResetPath();
        }
        
        
        
    }
}