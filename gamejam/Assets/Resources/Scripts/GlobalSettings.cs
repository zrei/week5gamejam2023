using UnityEngine;

public class GlobalSettings : Singleton<GlobalSettings>
{
    [Header("Debug")]
    [SerializeField] private bool m_DebugTextOn = false;
    public static bool g_DebugTextOn => Instance.m_DebugTextOn;

    [Header("Mouse Points")]
    [SerializeField] private int m_PointsInShape = 4;
    public static int g_PointsInShape => Instance.m_PointsInShape;

    [SerializeField] private float m_DelayAfterCompleteShape = 2f;
    public static float g_DelayAfterCompleteShape => Instance.m_DelayAfterCompleteShape;

    [SerializeField] private Color m_PointBlueColor;
    public static Color g_PointBlueColor => Instance.m_PointBlueColor;

    [Header("Collider values")]
    [SerializeField] private float m_PlayerCursorColliderRadius = 2f;
    public static float g_PlayerCursorColliderRadius => Instance.m_PlayerCursorColliderRadius;

    [SerializeField] private float m_ShapePointColliderRadius = 1f;
    public static float g_ShapePointColliderRadius => Instance.m_ShapePointColliderRadius;

    [Header("Health values")]
    [SerializeField] private int m_FoodHealthValue = 5;
    public static int g_FoodHealthValue => Instance.m_FoodHealthValue;

    [SerializeField] private int m_PlayerHealthValue = 10;
    public static int g_PlayerHealthValue => Instance.m_PlayerHealthValue;
}