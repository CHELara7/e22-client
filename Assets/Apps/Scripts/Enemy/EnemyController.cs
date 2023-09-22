using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _enemy;
    [SerializeField] private GameObject _attackBall;
    [SerializeField] private GameObject _attackPos;
    [SerializeField] private float _attackSpan;
    [SerializeField] private float _ballSpeed;
    [SerializeField] private bool _isRight;
    [SerializeField] private ObstacleController _obstacleController;

    private float _time;

    void Update()
    {
        _time += Time.deltaTime;
        if (_time > _attackSpan)
        {
            var ball = Instantiate(_attackBall, _attackPos.transform.position, Quaternion.identity);
            ball.GetComponent<AttackBall>().SetTarget(_player);
            ball.GetComponent<AttackBall>().SetSpeed(_ballSpeed);

            if (_isRight)
            {
                _obstacleController.SetRightObstacle(ball);
            }
            else
            {
                _obstacleController.SetLeftObstacle(ball);
            }

            _time -= _attackSpan;
        }
    }
}
