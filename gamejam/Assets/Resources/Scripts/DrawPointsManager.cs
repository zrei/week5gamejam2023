using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrawPointsManager : Singleton<DrawPointsManager>
{
    private List<Vector3> m_PointsInShape = new List<Vector3>(); 

    protected override void HandleAwake()
    {
        base.HandleAwake();
        GlobalEvents.CursorEvents.DrawPointEvent += DrawPoints;
    }

    protected override void HandleDestroy()
    {
        base.HandleDestroy();
        GlobalEvents.CursorEvents.DrawPointEvent -= DrawPoints;
    }

    private void DrawPoints(Vector3 mousePosition)
    {
        // distance check here, but I'll do it later
        m_PointsInShape.Add(mousePosition);
        if (m_PointsInShape.Count == GlobalSettings.g_PointsInShape)
            m_PointsInShape.Clear();
    }
}