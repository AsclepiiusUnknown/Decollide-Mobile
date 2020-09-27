using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DragAndDrop : MonoBehaviour
{
    bool _manualMove;
    Collider2D _col;
    private GameManager _gameManager;

    public GameObject selectionEffect;
    public GameObject deathEffect;

    public AudioClip touchSFX;
    public AudioClip dieSFX;

    public AudioSource audioSource;

    private void Awake()
    {
        _col = GetComponent<Collider2D>();
        _gameManager = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();

        if (audioSource == null)
            audioSource = FindObjectOfType<AudioSource>();
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            int i = 0;
            while (i < Input.touchCount)
            {
                Touch t = Input.GetTouch(i);
                Vector2 touchPos = Camera.main.ScreenToWorldPoint(t.position);

                if (t.phase == TouchPhase.Began)
                {
                    Collider2D touchedCol = Physics2D.OverlapPoint(touchPos);
                    if (_col == touchedCol)
                    {
                        GameObject spawnGO = Instantiate(selectionEffect, transform.position, Quaternion.identity);

                        if (audioSource != null)
                        {
                            audioSource.clip = touchSFX;
                            audioSource.Play();
                        }

                        _manualMove = true;
                    }
                }
                else if (t.phase == TouchPhase.Moved && _manualMove)
                {
                    transform.position = new Vector2(touchPos.x, touchPos.y);
                }
                else if (t.phase == TouchPhase.Ended)
                {
                    _manualMove = false;
                }

                i++;
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
                    GameObject spawnGO = Instantiate(selectionEffect, transform.position, Quaternion.identity);

                    if (audioSource != null)
                    {
                        audioSource.clip = touchSFX;
                        audioSource.Play();
                    }

                    _manualMove = true;
                }
            }
            else if (Input.GetMouseButton(0) && _manualMove)
            {
                transform.position = new Vector2(touchPos.x, touchPos.y);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                _manualMove = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.tag == "Planet") || (other.tag == "Comet" && other.GetComponent<Comet>().parentObject != this.gameObject))
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);

            if (audioSource != null)
            {
                audioSource.clip = dieSFX;
                audioSource.Play();
            }

            Destroy(other.gameObject);
            Destroy(gameObject);

            _gameManager.GameOver();
        }
    }
}