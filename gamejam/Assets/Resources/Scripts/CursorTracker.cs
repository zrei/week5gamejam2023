using UnityEngine;
using System.Collections;

public class CursorTracker : Singleton<CursorTracker> 
{
    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0)) // leftclick
            GlobalEvents.CursorEvents.DrawPointEvent?.Invoke(Input.mousePosition);
    }

}