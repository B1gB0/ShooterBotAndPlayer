using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    [SerializeField] private float _damage = 10f;
    [SerializeField] private float _distance = 500f;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _impactForce = 10f;

    [SerializeField] private UnityEngine.Transform _decal;
    [SerializeField] private float _decalOffset;
    [SerializeField] private ShootEffect _shootEffect;
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _reloadSound;

    [SerializeField] private CameraShake _cameraShake;
    [SerializeField] private WaterSplasher _splasher;

    [SerializeField] private Projectile _prefab;
    [SerializeField] private float _velocity;

    [Header ("Shell")]
    [SerializeField] private Rigidbody _shellPrefab;
    [SerializeField] private UnityEngine.Transform _shellPoint;
    [SerializeField] private float _shellSpeed = 2f;
    [SerializeField] private float _shellAngular = 15f;

    private Vector3 _startPoint;
    private Vector3 _direction;
    private Collider _collider;

    public void Initialize(CharacterController characterController)
    {
        _collider = characterController;
    }

    public void Shoot(Vector3 startPoint, Vector3 direction)
    {
        _startPoint = startPoint;
        _direction = direction;

        RaycastShoot(startPoint, direction * _velocity);
        _shootEffect.Perform();
        _animator.SetTrigger("Shoot");
        //ProjectileShoot(startPoint, direction * _velocity);
    }

    private void ProjectileShoot(Vector3 startPoint, Vector3 velocity)
    {
        var projectile = Instantiate(_prefab);
        projectile.Initialize(_damage, _collider);

        projectile.Shoot(startPoint, velocity);
    }

    private void RaycastShoot(Vector3 startPoint, Vector3 direction)
    {
        _cameraShake.MakeRecoil();

        if (Physics.SphereCast(startPoint, 0.1f, direction, out RaycastHit hitInfo, _distance, _layerMask, QueryTriggerInteraction.Ignore))
        {
            _splasher.TryCreateWaterSplash(_startPoint, hitInfo.point);

            var decal = Instantiate(_decal, hitInfo.transform);
            decal.position = hitInfo.point + hitInfo.normal * _decalOffset;
            decal.LookAt(hitInfo.point);
            decal.Rotate(Vector3.up, 180, Space.Self);

            var health = hitInfo.collider.GetComponentInParent<AbstractHealth>();

            if (health != null)
            {
                health.TakeDamage(_damage);
            }

            var victimBody = hitInfo.rigidbody;

            if(victimBody != null)
            {
                victimBody.AddForceAtPosition(direction * _impactForce, hitInfo.point);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        if (Physics.SphereCast(_startPoint, 0.1f, _direction, out RaycastHit hitInfo, _distance, _layerMask, QueryTriggerInteraction.Ignore))
        {
            Gizmos.DrawLine(_startPoint, hitInfo.point);
        }
    }

    public void ExtractShell()
    {
        var shell = Instantiate(_shellPrefab, _shellPoint.position, _shellPoint.rotation);
        shell.velocity = _shellPoint.forward * _shellSpeed;
        shell.angularVelocity = Vector3.up * _shellAngular;
    }

    private void ReloadSound()
    {
        _reloadSound.Play();
    }
}
