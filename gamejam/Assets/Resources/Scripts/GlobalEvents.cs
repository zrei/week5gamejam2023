using Events;
using UnityEngine;

namespace Events
{
    public delegate void VoidEvent();
    public delegate void FloatEvent(float _);
}

public class GlobalEvents
{
    public class CursorEvents
    {
        public delegate void CursorClickEvent(Vector3 mouseCoordinates);
        
        public static CursorClickEvent DrawPointEvent;
    }
}