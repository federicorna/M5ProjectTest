
using UnityEngine;

public class CharacterDetection : MonoBehaviour
{
    [SerializeField] private Transform _enemy;
    [SerializeField] private Transform _target;
    [SerializeField] private float _viewAngle = 45f;
    [SerializeField] private float _sightDistance = 15f;
    [SerializeField] private LayerMask _whatIsObstacle;

    public float ViewAngle => _viewAngle;
    public float SightDistance => _sightDistance;


    
    void Update()
    {
        if (_target != null && CanSeePlayer())
        {
            Debug.Log($"{gameObject.name} can see the target: {_target.gameObject.name}");
        }
    }

    public bool CanSeePlayer()
    {
        Vector3 toTarget = _target.position - transform.position;
        float sqrtDistance = toTarget.sqrMagnitude;

        if (sqrtDistance > SightDistance * _sightDistance)
        {
            return false;
        }

        float distance = Mathf.Sqrt(sqrtDistance);
        toTarget /= distance;

        if (Vector3.Dot(transform.forward, toTarget) < Mathf.Cos(_viewAngle * Mathf.Deg2Rad))
        {
            return false;
        }

        return true;
    }

}
