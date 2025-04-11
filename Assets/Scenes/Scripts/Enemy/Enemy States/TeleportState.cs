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
            
            if (timer <= 0)
            {
                TeleportToRandomNavMeshPoint(context);
                
                EnemyBaseState nextState = context.GetRandomState(
                    0.6f,
                    0.01f,
                    0.39f);
                context.SwitchState(nextState);
            }
        }

        public override void ExitState(EnemyBehaviour context)
        {

        }
        
        public void TeleportToRandomNavMeshPoint(EnemyBehaviour context)
        {

            agent.Warp(context.GetRandomWayPoint());
            
            /*
            NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();
            int randomIndex = Random.Range(0, navMeshData.vertices.Length);
            Vector3 randomPoint = navMeshData.vertices[randomIndex];
        
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                agent.Warp(hit.position);
            }*/
        }
    }
}