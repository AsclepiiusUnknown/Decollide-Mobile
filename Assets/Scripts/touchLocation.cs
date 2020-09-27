using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touchLocation : MonoBehaviour
{
    public int touchID;
    public GameObject planet;
    public bool manualMove = false;

    public touchLocation(int newTouchID, GameObject newPlanet, bool newManualMove)
    {
        touchID = newTouchID;
        planet = newPlanet;
        manualMove = newManualMove;
    }
}
