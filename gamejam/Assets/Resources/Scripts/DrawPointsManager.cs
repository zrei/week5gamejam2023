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

    private bool m_BlockDrawing = false;

    private bool m_CompletedDrawingBeingShown = false;

    protected override void HandleAwake()
    {
        base.HandleAwake();
        GlobalEvents.CursorEvents.DrawPointEvent += DrawPoints;
        GlobalEvents.CursorEvents.ClearPointsEvent += ClearPoints;
        GlobalEvents.PlayerControlEvents.WithinPointRangeEvent += HandleCollidePoint;
        GlobalEvents.PlayerControlEvents.NotWithinPointRangeEvent += HandleNoPoint;
        GlobalEvents.CursorEvents.InvalidShapeEvent += HandleInvalidShape;
        GlobalEvents.CursorEvents.ValidShapeEvent += HandleValidShape;

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
        GlobalEvents.CursorEvents.ClearPointsEvent -= ClearPoints;
        GlobalEvents.PlayerControlEvents.WithinPointRangeEvent -= HandleCollidePoint;
        GlobalEvents.PlayerControlEvents.NotWithinPointRangeEvent -= HandleNoPoint;
        GlobalEvents.CursorEvents.InvalidShapeEvent -= HandleInvalidShape;
        GlobalEvents.CursorEvents.ValidShapeEvent -= HandleValidShape;
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
        m_LineRenderer.startColor = Color.black;
        m_LineRenderer.endColor = Color.black;
        m_LineRenderer.startWidth = 0.2f;
        m_LineRenderer.endWidth = 0.2f;
    }

    private void HandleCollidePoint()
    {
        if (!m_BlockDrawing)
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
        m_PointsInShape.Add(point.transform.position); 

        // reset once you reach four points
        if (m_PointsInShape.Count == GlobalSettings.g_PointsInShape)
        {
            m_LineRenderer.positionCount += 1;
            m_LineRenderer.SetPosition(m_PointsInShape.Count, m_PointsInShape[0]);
            m_PointsInShape.Clear();
            DisableAllPoints();
            GlobalEvents.CursorEvents.CompleteDrawingAllPointsEvent?.Invoke();
            StartCoroutine(CompleteAllPointsDelay(GlobalSettings.g_DelayAfterCompleteShape));
        }
    }

    private void ClearPoints()
    {
        if (m_CompletedDrawingBeingShown)
            return;

        m_PointsInShape.Clear();
        DisableAllPoints();
        m_LineRenderer.positionCount = 0;
        m_BlockDrawing = false;
        m_CanDrawPoints = true;
        m_LineRenderer.startColor = Color.black;
        m_LineRenderer.endColor = Color.black;
    }

    private IEnumerator CompleteAllPointsDelay(float delayTime)
    {
        m_CompletedDrawingBeingShown = true;
        m_BlockDrawing = true;
        m_CanDrawPoints = false;
        yield return new WaitForSeconds(delayTime);
        m_LineRenderer.positionCount = 0;
        m_CompletedDrawingBeingShown = false;
        m_BlockDrawing = false;
        m_CanDrawPoints = true;
        m_LineRenderer.startColor = Color.black;
        m_LineRenderer.endColor = Color.black;
        yield return null;
    }

    private void HandleInvalidShape()
    {
        m_LineRenderer.startColor = Color.red;
        m_LineRenderer.endColor = Color.red;
    }

    private void HandleValidShape(List<Vector3> _, List<Vector3> _2)
    {
        m_LineRenderer.startColor = GlobalSettings.g_PointBlueColor;
        m_LineRenderer.endColor = GlobalSettings.g_PointBlueColor;
    }
}