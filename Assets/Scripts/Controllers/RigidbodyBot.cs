using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RigidbodyBot : MonoBehaviour
{
    [SerializeField] private float _speed = 3f;
    [SerializeField] private Transform _target;

    private Rigidbody _rigidbody;
    private Vector3 _offset;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _offset = transform.position - _target.transform.position;
        Vector3 speed = new Vector3(_target.transform.position.x - _offset.x * _speed, _rigidbody.velocity.y, _target.transform.position.z - _offset.z);

        _rigidbody.velocity = speed;
        _rigidbody.velocity += Vector3.down;
    }
}
