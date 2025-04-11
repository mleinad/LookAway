using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.SceneManagement;

namespace Scenes.Scripts.Enemy_States
{
    public class ChaseState: EnemyBaseState
    {
        public override void EnterState(EnemyBehaviour context)
        {
            context.GetNavAgent().SetDestination(context.GetPlayerTransform().position);
            context.animator.SetTrigger("Motion");
            context.GetNavAgent().speed = 10f;
        }

        public override void UpdateState(EnemyBehaviour context)
        {
         //   Debug.Log("Chase");

         context.animator.SetFloat("x", 0);
         context.animator.SetFloat("y", -1);
         
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