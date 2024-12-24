using UnityEngine;

public class AttackState : IState
{
    private float _attackTimer = 1.5f;
    private float _timer;

    public void OnEnter(Enemy enemy)
    {
        if (enemy.Target != null)
        {
            // doi huong enemy toi huong player
            enemy.ChangeDiraction(enemy.Target.transform.position.x > enemy.transform.position.x);
            enemy.StopMoving();
            enemy.Attack();
        }

        _timer = 0;
    }

    public void OnExecute(Enemy enemy)
    {
        _timer += Time.deltaTime;
        if (_timer >= _attackTimer)
        {
            enemy.ChangeState(new PatrolState());
        }
    }

    public void OnExit(Enemy enemy)
    {
    }
}