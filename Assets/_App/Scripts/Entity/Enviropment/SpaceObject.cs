using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceObject : MonoBehaviour
{
    [SerializeField] protected float _captureRadius;
    [SerializeField] protected float _attraction;

    public float Attraction     { get => _attraction; }
    public float CaptureRadius  { get => _captureRadius; }

    public static System.Action OnPlanetDisable;

    protected bool IsExplored = false;

    private void Awake()
    {
        _captureRadius = Random.Range(3f, 5f);
        GetComponent<CircleCollider2D>().radius = _captureRadius;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, _captureRadius);
    }
}