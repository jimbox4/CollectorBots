using System;
using UnityEngine;

[Serializable]
public class BotMover
{
    private const float SlowSpeedCoefficent = 0.5f;
    private const float BaseSpeedCoefficent = 1;

    [SerializeField] private float _moveSpeed;
    [SerializeField, Range(0, 180)] private float _maxAngleToMove;
    [SerializeField] private float _rotationSpeed;
    [field: SerializeField, Min(0)] public float MaxDistanceToTarget { get; private set; }

    private Rigidbody _rigidbody;
    private Transform _transform;
    private float _angleToTarget;
    private float _speedCoefficent;

    public float InterpolatedSpeed => Mathf.Abs(_rigidbody.velocity.z) / _moveSpeed;

    public void Initialize(Rigidbody rigidbody, Transform transform)
    {
        _rigidbody = rigidbody;
        _transform = transform;
    }

    public void ResetVelocitys()
    {
        ResetVelocity();
        _rigidbody.angularVelocity = Vector3.zero;
    }

    public void MoveTo(Vector3 target, bool isSlowed)
    {
        if (Vector2.Distance(new Vector2(_transform.position.x, _transform.position.z), new Vector2(target.x, target.z)) < MaxDistanceToTarget)
        {
            return;
        }

        if (isSlowed)
        {
            _speedCoefficent = SlowSpeedCoefficent;
        }
        else
        {
            _speedCoefficent = BaseSpeedCoefficent;
        }

        if (_angleToTarget < _maxAngleToMove)
        {
            _rigidbody.velocity = _transform.forward * _moveSpeed * _speedCoefficent + new Vector3(0, _rigidbody.velocity.y, 0);
        }
        else
        {
            ResetVelocity();
        }
    }

    public void LookAt(Vector3 target)
    {
        _angleToTarget = Vector2.Angle(new Vector2(target.x - _transform.position.x, target.z - _transform.position.z), new Vector2(_transform.forward.x, _transform.forward.z));
        _transform.rotation = Quaternion.Slerp(_transform.rotation, Quaternion.LookRotation(target - _transform.position), _rotationSpeed * Time.deltaTime);
        _transform.rotation = Quaternion.Euler(0, _transform.eulerAngles.y, 0);
    }

    private void ResetVelocity()
    {
        _rigidbody.velocity = Vector3.zero;
    }
}
