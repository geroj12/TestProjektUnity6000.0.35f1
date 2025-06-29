using UnityEngine;

[CreateAssetMenu(menuName = "EnemyStates/Patrol")]
public class PatrolStateAsset : EnemyState
{
    public float patrolSpeed = 3f;
    public float pointReachedThreshold = 1f;

    private int patrolIndex;
    private Transform currentPatrolPoint;

    public override void Enter(StateMachineEnemy enemy)
    {
        patrolIndex = 0;
        currentPatrolPoint = enemy.GetNextPatrolPoint();
        enemy.animator.SetBool("IsWalking", true);
    }

    public override void Tick(StateMachineEnemy enemy)
    {
        if (enemy.vision.CanSeeTarget())
        {
            enemy.TransitionTo(enemy.chaseState);
            return;
        }

        if (currentPatrolPoint == null) return;

        float dist = Vector3.Distance(enemy.transform.position, currentPatrolPoint.position);
        if (dist < pointReachedThreshold)
        {
            currentPatrolPoint = enemy.GetNextPatrolPoint();
        }

        enemy.MoveTo(currentPatrolPoint.position, patrolSpeed);
    }

    public override void Exit(StateMachineEnemy enemy)
    {
        enemy.animator.SetBool("IsWalking", false);

    }
}