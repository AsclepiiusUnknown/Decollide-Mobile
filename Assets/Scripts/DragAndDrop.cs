using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DragAndDrop : MonoBehaviour
{
    bool _pcManualMove;
    Collider2D _col;
    private GameManager _gameManager;

    public GameObject selectionEffect;
    public GameObject deathEffect;

    public AudioClip touchSFX;
    public AudioClip dieSFX;

    public AudioSource audioSource;

    // private List<touchLocation> touches = new List<touchLocation>();

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
            // while (i < Input.touchCount)
            if (Input.touchCount > 0)
            {
                Touch t = Input.GetTouch(i);
                Vector2 touchPos = Camera.main.ScreenToWorldPoint(t.position);

                if (t.phase == TouchPhase.Began)
                {
                    Collider2D touchedCol = Physics2D.OverlapPoint(touchPos);
                    if (_col == touchedCol)
                    {
                        GameObject spawnGO = Instantiate(selectionEffect, transform.position, Quaternion.identity);
                        // touches.Add(new touchLocation(t.fingerId, touchedCol.gameObject, true));

                        if (audioSource != null)
                        {
                            audioSource.clip = touchSFX;
                            audioSource.Play();
                        }

                        _pcManualMove = true;
                    }
                }
                else if (t.phase == TouchPhase.Moved && _pcManualMove)
                {
                    transform.position = t.position;
                    // touchLocation thisTouch = touches.Find(touchLocation => touchLocation.touchID == t.fingerId);

                    // if (thisTouch.manualMove)
                    //     thisTouch.planet.transform.position = new Vector2(t.position.x, t.position.y);
                }
                else if (t.phase == TouchPhase.Ended)
                {
                    // touchLocation thisTouch = touches.Find(touchLocation => touchLocation.touchID == t.fingerId);
                    // thisTouch.manualMove = false;
                    // Destroy(thisTouch.planet);
                    // touches.RemoveAt(touches.IndexOf(thisTouch));
                    _pcManualMove = false;
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

                    _pcManualMove = true;
                }
            }
            else if (Input.GetMouseButton(0) && _pcManualMove)
            {
                transform.position = new Vector2(touchPos.x, touchPos.y);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                _pcManualMove = false;
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