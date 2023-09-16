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
    public class CursorEvents
    {
        public delegate void CursorClickEvent(Vector3 mouseCoordinates);
        public delegate void ShapeEvent(List<Vector3> pointVectors, List<Vector3> sideVectors);
        public static CursorClickEvent DrawPointEvent;
        public static VoidEvent ClearPointsEvent;
        public static ShapeEvent ValidShapeEvent;
    }
}