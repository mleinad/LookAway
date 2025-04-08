using UnityEngine;

namespace Scenes.Scripts.Enemy_States
{
    public class FreezeState: EnemyBaseState
    {
        private float timer;
        public override void EnterState(EnemyBehaviour context)
        {
            context.GetNavAgent().ResetPath();
        }
        
        public override void UpdateState(EnemyBehaviour context)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                context.SwitchState(context.teleportState);
            }
            Debug.Log("frozen!");
        }

        public override void ExitState(EnemyBehaviour context)
        {
            
        }
            
        public void SeTimer(float time)=> timer = time;
        
    }
}