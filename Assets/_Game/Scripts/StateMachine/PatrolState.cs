using UnityEngine;

public class PatrolState : IState
{
    private float _randomTimer;
    private float _timer;

    public void OnEnter(Enemy enemy)
    {
        _timer = 0;
        _randomTimer = Random.Range(3f, 6f);
    }

    public void OnExecute(Enemy enemy)
    {
        _timer += Time.deltaTime;
        if (enemy.Target != null)
        {
            // doi huong enemy toi huong player
            enemy.ChangeDiraction(enemy.Target.transform.position.x > enemy.transform.position.x);
            if (enemy.IsTargetInRange())
            {
                enemy.ChangeState(new AttackState());
            }
            else
            {
                enemy.Moving();
            }
        }
        else
        {
            if (_timer < _randomTimer)
            {
                enemy.Moving();
            }
            else
            {
                enemy.ChangeState(new IdleState());
            }
        }
    }

    public void OnExit(Enemy enemy)
    {
    }
}