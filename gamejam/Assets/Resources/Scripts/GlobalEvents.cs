using Events;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace Events
{
    public delegate void VoidEvent();
    public delegate void FloatEvent(float _);
    public delegate void IntEvent(int _);
}

public class GlobalEvents
{
    public static VoidEvent GameOverEvent;
    public static VoidEvent LeaveEndScreenEvent;
    public class PlayerEvents
    {
        public static VoidEvent PlayerDeathEvent;
        public static IntEvent PlayerHealthChangeEvent;
    }
    public class CursorEvents
    {
        public delegate void CursorClickEvent(Vector3 mouseCoordinates);
        public delegate void ShapeEvent(List<Vector3> pointVectors, List<Vector3> sideVectors);
        public static CursorClickEvent DrawPointEvent;
        public static VoidEvent ClearPointsEvent;
        public static VoidEvent CompleteDrawingAllPointsEvent;
        public static ShapeEvent ValidShapeEvent;
        public static VoidEvent InvalidShapeEvent;
    }

    public class PlayerControlEvents
    {
        public static VoidEvent NotWithinPointRangeEvent;
        public static VoidEvent WithinPointRangeEvent;
    }

    public class FoodEvents
    {
        public static IntEvent FoodHealthEvent;
    }

    public class EnemyEvents
    {
        public static VoidEvent OnEnemyDeath;
    }
}