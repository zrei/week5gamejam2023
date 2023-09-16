using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrawPointsManager : Singleton<DrawPointsManager>
{
    //[SerializeField] private Canvas m_Canvas;
    [SerializeField] private GameObject m_PointObject;

    private List<Vector3> m_PointsInShape = new List<Vector3>(); 
    private List<GameObject> m_PointObjects;

    protected override void HandleAwake()
    {
        base.HandleAwake();
        GlobalEvents.CursorEvents.DrawPointEvent += DrawPoints;
        
        if (GlobalSettings.IsReady)
            InitialisePoints();
        else
            GlobalSettings.OnReady += InitialisePoints;
    }

    protected override void HandleDestroy()
    {
        base.HandleDestroy();
        GlobalEvents.CursorEvents.DrawPointEvent -= DrawPoints;
    }

    private void InitialisePoints()
    {
        GlobalSettings.OnReady -= InitialisePoints;
        m_PointObjects = new List<GameObject>(GlobalSettings.g_PointsInShape);
        for (int i = 0; i < GlobalSettings.g_PointsInShape; i++)
        {
            GameObject newPoint = Instantiate(m_PointObject, this.transform);
            newPoint.SetActive(false);
            m_PointObjects.Add(newPoint);
        }
    }

    // returns disabled game object
    private GameObject GetPoint()
    {
        foreach (GameObject point in m_PointObjects)
            if (point.activeSelf == false)
                return point;
        return null;
    }

    private void DrawPoints(Vector3 mousePosition)
    {
        if (GlobalSettings.g_DebugTextOn)
            Debug.Log("Mouse coordinates: " + mousePosition);
        // distance check here, but I'll do it later
        GameObject point = GetPoint();
        Vector3 pos = Camera.main.ScreenToWorldPoint(mousePosition);//new Vector3(mousePosition.x, mousePosition.y, 0f);
        point.transform.position = new Vector3(pos.x, pos.y, 0f);
        point.SetActive(true);
        Debug.Log("???");
        m_PointsInShape.Add(mousePosition);
        if (m_PointsInShape.Count == GlobalSettings.g_PointsInShape)
            m_PointsInShape.Clear();
    }
}