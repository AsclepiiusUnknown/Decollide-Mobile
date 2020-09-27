using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingPlanet : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float startTimeBtwShots;

    private float _timeBtwShots;

    private void Update()
    {
        if (_timeBtwShots <= 0)
        {
            GameObject cometGO = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            cometGO.GetComponent<Comet>().parentObject = this.gameObject;

            _timeBtwShots = startTimeBtwShots;
        }
        else
        {
            _timeBtwShots -= Time.deltaTime;
        }
    }
}