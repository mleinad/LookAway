using UnityEngine;
using UnityEngine.SceneManagement;

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
            
            if (context.DetectPlayerNearby(context.killRadius))
            {   
                Debug.Log("Killed player!");
            }
        }

        public override void ExitState(EnemyBehaviour context)
        {
            
        }


        private void HurtPlayer(float time)
        {
            time += Time.deltaTime;
        }
        private void ReloadScene()
        {
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
        }
        
        
    }
}