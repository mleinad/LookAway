using UnityEngine;

namespace Scenes.Scripts.Enemy_States
{
    public class FreezeState: EnemyBaseState
    {
        private float timer;
        public override void EnterState(EnemyBehaviour context)
        {
            context.GetNavAgent().isStopped = true;
        }
        
        public override void UpdateState(EnemyBehaviour context)
        {
            if (!context.IsGazingAtEnemy())
            {
                context.SwitchState(context.chaseState);
            }
        }

        public override void ExitState(EnemyBehaviour context)
        {
            context.GetNavAgent().isStopped = false;
        }
            
        public void SeTimer(float time)=> timer = time;
        
    }
}