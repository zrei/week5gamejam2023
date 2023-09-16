using UnityEngine;
using System.Collections;

public class CursorTracker : Singleton<CursorTracker> 
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && DrawPointsManager.Instance.CanDrawPoints) // leftclick
            GlobalEvents.CursorEvents.DrawPointEvent?.Invoke(Input.mousePosition);
        else if (Input.GetMouseButtonDown(1))
            GlobalEvents.CursorEvents.ClearPointsEvent?.Invoke();
    }

}