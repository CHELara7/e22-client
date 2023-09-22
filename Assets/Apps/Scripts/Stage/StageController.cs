using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    [SerializeField] private List<GameObject> _floorList;
    [SerializeField] private GameObject _player;
    [SerializeField] private float _changeFloorDiff;
    [SerializeField] private float _floorSpeed;
    [SerializeField] private Vector3 _initPos;
    
    private int _index;
    private const int StageCoef = 10;
    
    void Awake()
    {
        var pos = _initPos;
        // 初期配置
        for (var i = 0; i < _floorList.Count; i++)
        {
            _floorList[i].transform.position = pos;
            var nextPos = pos;
            nextPos.z += _floorList[i].transform.localScale.z * StageCoef;
            pos = nextPos;
        }

        foreach (var floor in _floorList)
        {
            floor.GetComponent<Floor>().SetSpeed(_floorSpeed);
        }
    }

    public void SetFloor()
    {
        var preIndex = _index - 1 < 0 ? _floorList.Count - 1 : _index - 1;
        var pos = _floorList[preIndex].transform.position;
        pos.z = _floorList[preIndex].transform.position.z +
                                                  _floorList[_index].transform.localScale.z * StageCoef;
        _floorList[_index].transform.position = pos;
        _index++;
        _index = _index == _floorList.Count ? 0 : _index;
    }

    void FixedUpdate()
    {
        var nextIndex = (_index + 1) % _floorList.Count;
        if (_player.transform.position.z > _floorList[nextIndex].transform.position.z)
        {
            SetFloor();
        }
    }
}
