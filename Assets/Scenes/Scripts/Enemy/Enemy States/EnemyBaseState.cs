
public abstract class EnemyBaseState
{
    public abstract void EnterState(EnemyBehaviour context);

    public abstract void UpdateState(EnemyBehaviour context);

    public abstract void ExitState(EnemyBehaviour context);
}
