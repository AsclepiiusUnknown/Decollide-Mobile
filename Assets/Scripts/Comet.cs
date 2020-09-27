using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Comet : MonoBehaviour
{
    public float speed;
    public GameObject destroyEffect;

    private GameObject[] _planets;
    private Vector2 _target;
    private GameManager _gameManager;
    [HideInInspector] public GameObject parentObject;
    private Collider2D _col;

    private void Awake()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        _planets = GameObject.FindGameObjectsWithTag("Planet");
        _col = GetComponent<Collider2D>();
    }

    private void Start()
    {
        int rand = 0;
        Random.Range(0, _planets.Length);
        _target = _planets[rand].transform.position;

        while (_planets[rand] == this.gameObject)
        {
            Random.Range(0, _planets.Length);
            _target = _planets[rand].transform.position;
        }
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, _target, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, _target) < .1f)
        {
            Destroy(gameObject);
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

            if (touch.phase == TouchPhase.Began)
            {
                Collider2D touchedCol = Physics2D.OverlapPoint(touchPos);
                if (_col == touchedCol)
                {
                    GameObject spawnGO = Instantiate(destroyEffect, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                }
            }
        }
        else
        {
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Input.GetMouseButtonDown(0))
            {
                Collider2D touchedCol = Physics2D.OverlapPoint(touchPos);
                if (_col == touchedCol)
                {
                    GameObject spawnGO = Instantiate(destroyEffect, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Planet" && other.gameObject != parentObject)
        {
            _gameManager.GameOver();
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}