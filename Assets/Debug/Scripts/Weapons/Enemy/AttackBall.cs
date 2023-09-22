using UnityEngine;

public class AttackBall : MonoBehaviour
{
    private GameObject _target;
    private float _speed;

    private const string PlayerTag = "Player";

    void Update()
    {
        if (_target != null)
        {
            transform.position =
                Vector3.MoveTowards(transform.position, _target.transform.position, _speed * Time.deltaTime);
        }
    }

    public void SetTarget(GameObject target)
    {
        _target = target;
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }
}
