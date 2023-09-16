using UnityEngine;

public class GlobalSettings : Singleton<GlobalSettings>
{
    [Header("Debug")]
    [SerializeField] private bool m_DebugTextOn = false;
    public static bool g_DebugTextOn => Instance.m_DebugTextOn;

    [Header("Mouse Points")]
    [SerializeField] private int m_PointsInShape = 4;
    public static int g_PointsInShape => Instance.m_PointsInShape;
}