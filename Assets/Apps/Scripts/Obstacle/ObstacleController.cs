using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    [SerializeField] private List<GameObject> _rightObstacles;
    [SerializeField] private List<GameObject> _leftObstacles;

    public void Initialize()
    {
        _rightObstacles.Clear();
        _leftObstacles.Clear();
    }

    public void SetRightObstacle(GameObject rightObstacle)
    {
        _rightObstacles.Add(rightObstacle);
    }

    public void SetLeftObstacle(GameObject leftObstacle)
    {
        _leftObstacles.Add(leftObstacle);
    }

    public GameObject GetRightObstacke()
    {
        return _rightObstacles.Count > 0 ? _rightObstacles[0] : null;
    }

    public GameObject GetLeftObstacke()
    {
        return _leftObstacles.Count > 0 ? _leftObstacles[0] : null;
    }

    public void RemoveObstacle(GameObject obstacle)
    {
        if (_rightObstacles.Contains(obstacle))
        {
            _rightObstacles.Remove(obstacle);
        }
        else
        {
            _leftObstacles.Remove(obstacle);
        }

        Destroy(obstacle);
    }
}
