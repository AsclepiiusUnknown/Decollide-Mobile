using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class multipleTouch : MonoBehaviour
{
    public GameObject circle;
    public List<touchLocation> touches = new List<touchLocation>();

    private void Update()
    {
        int i = 0;
        while (i < Input.touchCount)
        {
            Touch t = Input.GetTouch(i);

            if (t.phase == TouchPhase.Began)
            {
                print("began");
            }
            else if (t.phase == TouchPhase.Moved)
            {
                print("moved");
            }
            else if (t.phase == TouchPhase.Ended)
            {
                print("ended");
            }
            i++;
        }
    }
}
