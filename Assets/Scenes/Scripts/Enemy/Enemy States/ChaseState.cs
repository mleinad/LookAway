using UnityEngine;

namespace Scenes.Scripts.Enemy_States
{
    public class ChaseState: EnemyBaseState
    {
        public override void EnterState(EnemyBehaviour context)
        {
            context.GetNavAgent().SetDestination(context.GetPlayerTransform().position);
        }

        public override void UpdateState(EnemyBehaviour context)
        {
         //   Debug.Log("Chase");
            context.GetNavAgent().SetDestination(context.GetPlayerTransform().position);

            if (context.IsGazingAtEnemy())
            {
                context.SwitchState(context.freezeState);
            }


            if (context.DetectPlayerNearby())
            {
                Debug.Log("Player died!");
            }
        }

        public override void ExitState(EnemyBehaviour context)
        {
            
        }
        
        
    }
}