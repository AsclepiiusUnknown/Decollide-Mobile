using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touchLocation : MonoBehaviour
{
    public int touchID;
    public GameObject circle;

    public touchLocation(int newTouchID, GameObject newCircle)
    {
        touchID = newTouchID;
        circle = newCircle;
    }
}
