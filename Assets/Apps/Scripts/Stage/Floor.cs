using UnityEngine;

public class Floor : MonoBehaviour
{
    private float _speed;

    void FixedUpdate()
    {
        transform.Translate(0, 0, -_speed * Time.deltaTime);
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }
}
