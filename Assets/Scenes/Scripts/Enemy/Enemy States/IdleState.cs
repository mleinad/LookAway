using UnityEngine;
using UnityEngine.AI;


namespace Scenes.Scripts.Enemy_States
{
    public class IdleState : EnemyBaseState //handle animations as well 
    {
        float idleTimer;
        public override void EnterState(EnemyBehaviour context)
        {
            idleTimer = Random.Range(2f, 5f);
            Debug.Log($"entered idle state with a {idleTimer} seconds timer");
        }

        public override void UpdateState(EnemyBehaviour context)
        {
            idleTimer -= Time.deltaTime;    //waiting for random amount of time
            
            if (context.LookForPlayer()||context.DetectPlayerNearby())
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
        }
        
        public override void ExitState(EnemyBehaviour context)
        {
            
        }
    }
}