using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrawnShapeManager : Singleton<DrawnShapeManager>
{
    private List<Vector3> m_PointsInShape = new List<Vector3>(); 

    protected override void HandleAwake()
    {
        base.HandleAwake();
        GlobalEvents.CursorEvents.DrawPointEvent += HandlePointDrawn;
        GlobalEvents.CursorEvents.ClearPointsEvent += HandleClearPoints;
        GlobalEvents.CursorEvents.CompleteDrawingAllPointsEvent += HandleAllPointsDrawn;
    }

    protected override void HandleDestroy()
    {
        base.HandleDestroy();
        GlobalEvents.CursorEvents.DrawPointEvent -= HandlePointDrawn;
        GlobalEvents.CursorEvents.CompleteDrawingAllPointsEvent -= HandleAllPointsDrawn;
        GlobalEvents.CursorEvents.ClearPointsEvent -= HandleClearPoints;
    }

    private void HandlePointDrawn(Vector3 mousePosition)
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(mousePosition);
        m_PointsInShape.Add(new Vector3(pos.x, pos.y, 0f));
    }

    private void HandleClearPoints()
    {
        m_PointsInShape.Clear();
    }
    
    private void HandleShapeCompleted()
    {
        List<Vector3> polygonVectors = new List<Vector3>();
        
        for (int i = 0; i < GlobalSettings.g_PointsInShape; i++)
            polygonVectors.Add(m_PointsInShape[(i + 1) % GlobalSettings.g_PointsInShape] - m_PointsInShape[i]);
        
        if (CheckValidShape(polygonVectors))
            GlobalEvents.CursorEvents.ValidShapeEvent?.Invoke(m_PointsInShape, polygonVectors);
        else
            GlobalEvents.CursorEvents.InvalidShapeEvent?.Invoke();
    }

    private bool CheckValidShape(List<Vector3> polygonVectors)
    {
        for (int i = 0; i < polygonVectors.Count; i++)
            for (int j = i + 1; j < polygonVectors.Count; j++)
                if (CheckIntersection(m_PointsInShape[i], m_PointsInShape[(i + 1) % GlobalSettings.g_PointsInShape], polygonVectors[i], m_PointsInShape[j], m_PointsInShape[(j + 1) % GlobalSettings.g_PointsInShape], polygonVectors[j]))
                {
                    Debug.Log(" " + m_PointsInShape[i] + " " + polygonVectors[i] + " " + m_PointsInShape[j] + " " + polygonVectors[j]);
                    return false;
                }
        return true;
    }
    
    private bool CheckIntersection(Vector3 line1Start, Vector3 line1End,
        Vector3 line1, Vector3 line2Start, Vector3 line2End, Vector3 line2){

        Vector3 lineVec3 = line2Start - line1Start;
        Vector3 crossVec1and2 = Vector3.Cross(line1, line2);
        Vector3 crossVec3and2 = Vector3.Cross(lineVec3, line2);

        float planarFactor = Vector3.Dot(lineVec3, crossVec1and2);

        //is coplanar, and not parallel
        if( Mathf.Abs(planarFactor) < 0.0001f 
                && crossVec1and2.sqrMagnitude > 0.0001f)
        {
            float s = Vector3.Dot(crossVec3and2, crossVec1and2) 
                    / crossVec1and2.sqrMagnitude;
            Vector3 intersection = line1Start + (line1 * s);
            if (intersection == line1Start || intersection == line2Start || intersection == line1End || intersection == line2End)
                return false;
            else if (!CheckWithinLineSegment(line1Start, line1End, line2Start, line2End, intersection))
                return false;
            else
                return true;
        }
        else
            return false;

        bool CheckWithinLineSegment(Vector3 start1, Vector3 end1, Vector3 start2, Vector3 end2, Vector3 pointOfInterest)
        {
            double lineSegment1Length = (end1 - start1).sqrMagnitude;
            bool withinLineSegment1 = (pointOfInterest - end1).sqrMagnitude <= lineSegment1Length && (pointOfInterest - start1).sqrMagnitude <= lineSegment1Length;

            double lineSegment2Length = (end2 - start2).sqrMagnitude;
            bool withinLineSegment2 = (pointOfInterest - end2).sqrMagnitude <= lineSegment2Length && (pointOfInterest - start2).sqrMagnitude <= lineSegment2Length;

            return withinLineSegment1 && withinLineSegment2;
        }
    
    }

    private void HandleAllPointsDrawn()
    {
        HandleShapeCompleted();
        m_PointsInShape.Clear();
    }
}