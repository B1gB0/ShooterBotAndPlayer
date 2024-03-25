using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSplasher : MonoBehaviour
{
    [SerializeField] private UnityEngine.Transform _splashPrefab;

    private UnityEngine.Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    public bool CheckWater(Vector3 input)
    {
        return input.y < _transform.position.y;
    }

    public Vector3 RaycastToVirtualPlane(Vector3 startPoint, Vector3 direction)
    {
        Plane plane = new Plane(Vector3.up, _transform.position);
        Ray ray = new Ray(startPoint, direction);

        if (plane.Raycast(ray, out float enter))
        {
            return (startPoint + direction.normalized * enter);
        }

        return Vector3.zero;
    }

    public void TryCreateWaterSplash(Vector3 startPoint, Vector3 endPoint)
    {
        if (CheckWater(endPoint))
        {
            var point =  RaycastToVirtualPlane(startPoint, endPoint - startPoint);
            Destroy(Instantiate(_splashPrefab, point, _splashPrefab.rotation), 5);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawSphere(transform.position, 0.1f);
    }
}
