using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Scenes.Scripts.Enemy_States
{
    public class TeleportState: EnemyBaseState
    {
        private NavMeshAgent agent;
        private Transform agentTransform;
        private float timer;
        public override void EnterState(EnemyBehaviour context)
        {
            timer = Random.Range(0.2f, 1f);
            agent = context.GetNavAgent();
            agentTransform = context.transform;
        }
        
        public override void UpdateState(EnemyBehaviour context)
        {
            timer -= Time.deltaTime;


            if (context.LookForPlayer()||context.DetectPlayerNearby())
            {
                context.SwitchState(context.chaseState);
            }
            
            if (timer <= 0)
            {
                TeleportToRandomNavMeshPoint();
                
                EnemyBaseState nextState = context.GetRandomState(
                    0.5f,
                    0.2f,
                    0.3f);
                context.SwitchState(nextState);
            }
        }

        public override void ExitState(EnemyBehaviour context)
        {

        }
        
        public void TeleportToRandomNavMeshPoint()
        {
            NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();
            int randomIndex = Random.Range(0, navMeshData.vertices.Length);
            Vector3 randomPoint = navMeshData.vertices[randomIndex];
        
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                agent.Warp(hit.position);
            }
        }
    }
}