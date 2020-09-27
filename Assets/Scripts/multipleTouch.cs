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
                touches.Add(new touchLocation(t.fingerId, CreateCircle(t), true));
            }
            else if (t.phase == TouchPhase.Moved)
            {
                touchLocation thisTouch = touches.Find(touchLocation => touchLocation.touchID == t.fingerId);
                thisTouch.planet.transform.position = GetTouchPos(t.position);
            }
            else if (t.phase == TouchPhase.Ended)
            {
                touchLocation thisTouch = touches.Find(touchLocation => touchLocation.touchID == t.fingerId);
                thisTouch.manualMove = false;
                Destroy(thisTouch.planet);
                touches.RemoveAt(touches.IndexOf(thisTouch));
            }
            i++;
        }
    }

    GameObject CreateCircle(Touch t)
    {
        GameObject c = Instantiate(circle) as GameObject;
        c.name = "Touch" + t.fingerId;
        c.transform.position = GetTouchPos(t.position);
        return c;
    }

    Vector2 GetTouchPos(Vector2 touchPos)
    {
        return GetComponent<Camera>().ScreenToWorldPoint(new Vector3(touchPos.x, touchPos.y, transform.position.z));
    }
}
