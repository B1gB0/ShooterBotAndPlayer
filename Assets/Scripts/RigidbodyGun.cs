using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyGun : MonoBehaviour
{
    [SerializeField] private Rigidbody _projectile;
    [SerializeField] private UnityEngine.Transform _startPosition;
    [SerializeField] private float _speed = 10f;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var projectile = Instantiate(_projectile, _startPosition.position, _projectile.rotation);
            Vector3 forward = _startPosition.forward;
            projectile.velocity = forward * _speed;
        }
    }
}
