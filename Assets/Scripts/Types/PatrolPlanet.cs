using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPlanet : MonoBehaviour
{
    public Vector2 minMaxX;
    public Vector2 minMaxY;
    public float minSpeed;
    public float maxSpeed;
    public float secsToMaxDifficulty; //How long we are going to increase the difficulty

    float _speed;
    Vector2 _targetPos;

    private void Start()
    {
        _targetPos = GetRandomPos();
    }

    private void Update()
    {
        if ((Vector2)transform.position != _targetPos)
        {
            _speed = Mathf.Lerp(minSpeed, maxSpeed, GetDifficultyPct());
            transform.position = Vector2.MoveTowards(transform.position, _targetPos, _speed * Time.deltaTime);
        }
        else
        {
            _targetPos = GetRandomPos();
        }
    }

    Vector2 GetRandomPos()
    {
        float randomX = Random.Range(minMaxX.x, minMaxX.y);
        float randomY = Random.Range(minMaxY.x, minMaxY.y);
        return new Vector2(randomX, randomY);
    }

    float GetDifficultyPct()
    {
        return Mathf.Clamp01(Time.timeSinceLevelLoad / secsToMaxDifficulty);
    }
}