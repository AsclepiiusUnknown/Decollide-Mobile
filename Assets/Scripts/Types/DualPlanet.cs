using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DualPlanet : MonoBehaviour
{
    public Transform otherPlanet;
    public float speed;

    private LineRenderer _lineRenderer;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (otherPlanet != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, otherPlanet.position, speed * Time.deltaTime);

            if (_lineRenderer != null)
            {
                _lineRenderer.SetPosition(0, transform.position);
                _lineRenderer.SetPosition(1, otherPlanet.position);
            }
        }
    }
}