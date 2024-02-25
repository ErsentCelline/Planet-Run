using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : SpaceObject
{
    [SerializeField] private ParticleSystem _particle;
    private GameObject Ship;

    private void OnEnable()
    {
        _attraction = Random.Range(5f, 7f);
        _particle.startSize = _captureRadius * 2;
    }

    private void Update()
    {
        if (Ship != null)
        {
            float distance = Vector2.Distance(transform.position, Ship.transform.position);

            if (distance < 1f)
            {
                Ship.SetActive(false);
                return;
            }

            if (Vector3.Distance(Ship.transform.position, transform.position) < 1.75)
            {
                Message.ShowMessage();
            }
        }

        if (transform.position.y < Camera.main.transform.position.y - 25)
        {
            OnPlanetDisable?.Invoke();
            gameObject.SetActive(false);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsExplored) return;
        IsExplored = true;

        if (collision.gameObject.CompareTag("Ship"))
        {
            Ship = collision.gameObject;
            global::Ship.OnEnterTheOrbit(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ship"))
        {
            Ship = null;
            global::Ship.ExitTheOrbit();

        }
    }
}