using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : SpaceObject
{
    [SerializeField] private ParticleSystem _orbitalEffect;
    [SerializeField] private ParticleSystem _orbitVisualize;

    private void Start()
    {
        int index = Random.Range(1, 18);
        GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>($"Planets/planet{index}");
    }

    private void OnEnable()
    {
        _attraction = Random.Range(.25f, .5f);

        var shape = _orbitalEffect.shape;
        shape.radius = _captureRadius;

        shape = _orbitVisualize.shape;
        shape.radius = _captureRadius;
    }

    private void Update()
    {
        if (transform.position.y < Camera.main.transform.position.y - 25)
        {
            OnPlanetDisable?.Invoke();
            gameObject.SetActive(false);
        }

        if (Ship.Planet != this) return;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsExplored) return;
        IsExplored = true;
        
        if (collision.gameObject.CompareTag("Ship"))
        {
            _orbitVisualize.gameObject.SetActive(true);
            Ship.OnEnterTheOrbit(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ship"))
        {
            Ship.ExitTheOrbit();
            _orbitVisualize.gameObject.SetActive(false);
        }
    }
}