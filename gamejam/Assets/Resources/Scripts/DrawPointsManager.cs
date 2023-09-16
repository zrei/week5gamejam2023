using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrawPointsManager : Singleton<DrawPointsManager>
{
    //[SerializeField] private Canvas m_Canvas;
    [SerializeField] private GameObject m_PointObject;
    private List<Vector3> m_PointsInShape = new List<Vector3>(); 

    private List<GameObject> m_PointObjects;
    private LineRenderer m_LineRenderer;

    private bool m_CanDrawPoints = true;
    public bool CanDrawPoints => m_CanDrawPoints;

    protected override void HandleAwake()
    {
        base.HandleAwake();
        GlobalEvents.CursorEvents.DrawPointEvent += DrawPoints;
        GlobalEvents.PlayerControlEvents.WithinPointRangeEvent += HandleCollidePoint;
        GlobalEvents.PlayerControlEvents.NotWithinPointRangeEvent += HandleNoPoint;
        m_LineRenderer = GetComponent<LineRenderer>();
        if (GlobalSettings.IsReady)
            HandleDependencies();
        else
            GlobalSettings.OnReady += HandleDependencies;
    }

    protected override void HandleDestroy()
    {
        base.HandleDestroy();
        GlobalEvents.CursorEvents.DrawPointEvent -= DrawPoints;
        GlobalEvents.PlayerControlEvents.WithinPointRangeEvent -= HandleCollidePoint;
        GlobalEvents.PlayerControlEvents.NotWithinPointRangeEvent -= HandleNoPoint;
    }

    private void HandleDependencies()
    {
        GlobalSettings.OnReady -= HandleDependencies;
        m_PointObjects = new List<GameObject>(GlobalSettings.g_PointsInShape);
        for (int i = 0; i < GlobalSettings.g_PointsInShape; i++)
        {
            GameObject newPoint = Instantiate(m_PointObject, this.transform);
            newPoint.SetActive(false);
            m_PointObjects.Add(newPoint);
        }
        m_LineRenderer.positionCount = 0;
    }

    private void HandleCollidePoint()
    {
        m_CanDrawPoints = true;
    }

    private void HandleNoPoint()
    {
        if (m_PointsInShape.Count > 0)
            m_CanDrawPoints = false;
    }

    // returns disabled game object
    private GameObject GetPoint()
    {
        foreach (GameObject point in m_PointObjects)
            if (point.activeSelf == false)
                return point;
        return null;
    }

    private void DisableAllPoints()
    {
        foreach (GameObject point in m_PointObjects)
            point.SetActive(false);
    }

    private void DrawPoints(Vector3 mousePosition)
    {
        if (GlobalSettings.g_DebugTextOn)
            Debug.Log("Mouse coordinates: " + mousePosition);
        
        // draw point
        GameObject point = GetPoint();
        Vector3 pos = Camera.main.ScreenToWorldPoint(mousePosition);//new Vector3(mousePosition.x, mousePosition.y, 0f);
        point.transform.position = new Vector3(pos.x, pos.y, 0f);
        point.SetActive(true);

        // draw line
        m_LineRenderer.positionCount += 1;
        m_LineRenderer.SetPosition(m_PointsInShape.Count, point.transform.position);
        m_PointsInShape.Add(mousePosition); 

        // reset once you reach four points
        if (m_PointsInShape.Count == GlobalSettings.g_PointsInShape)
        {
            m_LineRenderer.positionCount += 1;
            m_LineRenderer.SetPosition(m_PointsInShape.Count, m_PointsInShape[0]);
            StartCoroutine(Delay(2f));
            m_LineRenderer.positionCount = 0;
            m_PointsInShape.Clear();
            DisableAllPoints();
            m_CanDrawPoints = true;
            GlobalEvents.CursorEvents.ClearPointsEvent?.Invoke();
        }
    }

    private IEnumerator Delay(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
    }
}