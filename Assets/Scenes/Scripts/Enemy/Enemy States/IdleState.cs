using UnityEngine;
using UnityEngine.AI;


namespace Scenes.Scripts.Enemy_States
{
    public class IdleState : EnemyBaseState //handle animations as well 
    {
        private static readonly int Iddle = Animator.StringToHash("idle");
        float idleTimer;
        public override void EnterState(EnemyBehaviour context)
        {
            idleTimer = Random.Range(2f, 5f);
            Debug.Log($"entered idle state with a {idleTimer} seconds timer");
            

            if (idleTimer >= 3)
            {
                context.animator.SetInteger(Iddle, 3);
                
            }
            else
            {
                context.animator.SetInteger(Iddle, 1);

            }
        }

        public override void UpdateState(EnemyBehaviour context)
        {
            idleTimer -= Time.deltaTime;    //waiting for random amount of time
            
            if (context.LookForPlayer()||context.DetectPlayerNearby(context.detectRadius))
            {
                context.SwitchState(context.chaseState);
            }
            
            if(idleTimer <= 0) //when timer ends
            {
               EnemyBaseState nextState = context.GetRandomState(
                   0.6f, 
                   0.3f,
                   0.1f);
               context.SwitchState(nextState);
            }
            
            
            if (context.IsGazingAtEnemy())
            {
                //wait for 3/4 seconds before switching 
                context.SwitchState(context.freezeState);
            }
            
        }
        
        public override void ExitState(EnemyBehaviour context)
        {
            context.animator.SetInteger(Iddle, -1);
        }
    }
}