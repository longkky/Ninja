using UnityEngine;

public class IdleState : IState
{
    private float _randomTimer;
    private float _timer;
    public void OnEnter(Enemy enemy)
    {
        enemy.StopMoving();
        _timer = 0;
        _randomTimer = Random.Range(2f, 4f);
    }

    public void OnExecute(Enemy enemy)
    {
        _timer += Time.deltaTime;
        if (_timer > _randomTimer)
        {
            enemy.ChangeState(new PatrolState());
        }
    }

    public void OnExit(Enemy enemy)
    {
        
    }
}
