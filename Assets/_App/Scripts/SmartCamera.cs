using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartCamera : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)] 
    private float _smoothTime;

    private Transform target;
    private Vector3 velocity;

    public void MoveTo(SpaceObject planet)
    {
        target = planet.transform;
    }

    private void Awake()
    {
        Ship.OnShipArriveToPlanet += MoveTo;
        Ship.OnShipDisabled += Reset;
    }

    private void FixedUpdate()
    {
        if (target == null) return;
        if (transform.position.y == target.position.y)
        {
            target = null;
            return;
        }

        transform.position = Vector3.SmoothDamp(transform.position, TargetPosition, ref velocity, _smoothTime);
    }

    private void Reset()
    {
        target = null;
        transform.position = new Vector3(0, 0, -10);
    }

    private void OnDestroy()
    {
        Ship.OnShipArriveToPlanet -= MoveTo;
        Ship.OnShipDisabled -= Reset;
    }

    private Vector3 TargetPosition { get => new Vector3(transform.position.x, target.position.y, -10); }
}