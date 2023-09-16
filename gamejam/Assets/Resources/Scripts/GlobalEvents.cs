using Events;
using UnityEngine;

namespace Events
{
    public delegate void VoidEvent();
    public delegate void FloatEvent(float _);
}

public class GlobalEvents
{
    public class PlayerEvents
    {
        public static VoidEvent PlayerDeathEvent;
        public static FloatEvent PlayerHealthChangeEvent;
    }
    public class CursorEvents
    {
        public delegate void CursorClickEvent(Vector3 mouseCoordinates);
        
        public static CursorClickEvent DrawPointEvent;
    }
}