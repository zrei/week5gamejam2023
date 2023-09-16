using Events;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

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
        public delegate void ShapeEvent(List<Vector3> pointVectors, List<Vector3> sideVectors);
        public static CursorClickEvent DrawPointEvent;
        public static VoidEvent ClearPointsEvent;
        public static ShapeEvent ValidShapeEvent;
    }

    public class PlayerControlEvents
    {
        public static VoidEvent NotWithinPointRangeEvent;
        public static VoidEvent WithinPointRangeEvent;
    }
}