using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyDeath : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        GlobalEvents.CursorEvents.ValidShapeEvent += HandleShapeCompleted;
    }

    private void OnDestroy()
    {
        GlobalEvents.CursorEvents.ValidShapeEvent -= HandleShapeCompleted;
    }

    // line 1 will be the polygon line!
    private bool CheckIntersection(Vector3 line1Start, Vector3 line1End,
        Vector3 lineVec1, Vector3 linePoint2, Vector3 lineVec2){

        Vector3 lineVec3 = linePoint2 - line1Start;
        Vector3 crossVec1and2 = Vector3.Cross(lineVec1, lineVec2);
        Vector3 crossVec3and2 = Vector3.Cross(lineVec3, lineVec2);

        float planarFactor = Vector3.Dot(lineVec3, crossVec1and2);

        //is coplanar, and not parallel
        if( Mathf.Abs(planarFactor) < 0.0001f 
                && crossVec1and2.sqrMagnitude > 0.0001f)
        {
            float s = Vector3.Dot(crossVec3and2, crossVec1and2) 
                    / crossVec1and2.sqrMagnitude;
            Vector3 intersection = line1Start + (lineVec1 * s);
            //Debug.Log("Intersection point is: " + intersection);
            if (CheckWithinLineSegment(line1Start, line1End, intersection) && CheckIsInFront(intersection, linePoint2))
                return true;
            else 
                return false;
        }
        else
        {
            Debug.Log("Parallel");
            return false;
        }

        bool CheckWithinLineSegment(Vector3 start1, Vector3 end1, Vector3 pointOfInterest)
        {
            double lineSegment1Length = (end1 - start1).sqrMagnitude;
            bool withinLineSegment1 = (pointOfInterest - end1).sqrMagnitude <= lineSegment1Length && (pointOfInterest - start1).sqrMagnitude <= lineSegment1Length;
            //Debug.Log("Within line segment: " +  withinLineSegment1);
            return withinLineSegment1;
        }

        bool CheckIsInFront(Vector3 inFrontPoint, Vector3 behindPoint)
        {
            Vector3 temp = inFrontPoint - behindPoint;
            return temp.x >= 0;
        }
    }

    private void HandleShapeCompleted(List<Vector3> points, List<Vector3> sides)
    {
        Debug.Log("The shape is containing: " + CheckWithinShape(points, sides));
        if (CheckWithinShape(points, sides)) 
        {
            anim = GetComponent<Animator>();
            anim.SetTrigger("isDead");
            Destroy(gameObject, 1f);
        }
    }

    private bool CheckWithinShape(List<Vector3> points, List<Vector3> sides)
    {
        int numIntersections = 0;
        for (int i = 0; i < GlobalSettings.g_PointsInShape; i++)
        {
            if (CheckIntersection(points[i], points[(i + 1) % GlobalSettings.g_PointsInShape], sides[i], transform.position, Vector3.right))
                numIntersections++;
        }
        //Debug.Log("Number of intersections: " + numIntersections);
        return (numIntersections % 2) == 1;
    }
}