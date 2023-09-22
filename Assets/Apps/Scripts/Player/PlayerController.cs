using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _normalInside;
    [SerializeField] private float _critical;
    [SerializeField] private float _normalOutside;
    [SerializeField] private ObstacleController _obstacleController;

    private const string ObstacleTag = "Obstacle";

    public void AttackRight()
    {
        var rightObstacke = _obstacleController.GetRightObstacke();
        if (rightObstacke != null)
        {
            Attack(rightObstacke);
        }
    }

    public void AttackLeft()
    {
        var leftObstacke = _obstacleController.GetLeftObstacke();
        if (leftObstacke != null)
        {
            Attack(leftObstacke);
        }
    }

    private void Attack(GameObject obstacle)
    {
        var distance = (obstacle.transform.position - transform.position).magnitude;
        if (distance < _normalInside)
        {
            Debug.Log("NormalInside");
            _obstacleController.RemoveObstacle(obstacle);
        }
        else if (distance < _critical)
        {
            Debug.Log("Critical");
            _obstacleController.RemoveObstacle(obstacle);
        }
        else if (distance < _normalOutside)
        {
            Debug.Log("NormalOutside");
            _obstacleController.RemoveObstacle(obstacle);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ObstacleTag))
        {
            Debug.Log("Damage");
            _obstacleController.RemoveObstacle(other.gameObject);
        }
    }
}
