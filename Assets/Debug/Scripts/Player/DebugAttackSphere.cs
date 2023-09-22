#if UNITY_EDITOR
using UnityEngine;

public class DebugAttackSphere : MonoBehaviour
{
    [SerializeField] private SphereCollider _sphereCollider;
    private Vector3 _scale;

    void Update()
    {
        var scale = _sphereCollider.radius * 2;
        _scale = new Vector3(scale, scale, scale);
        gameObject.transform.localScale = _scale;
    }
}
#endif