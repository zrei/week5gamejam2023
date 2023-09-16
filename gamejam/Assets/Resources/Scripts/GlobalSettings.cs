using UnityEngine;

public class GlobalSettings : Singleton<GlobalSettings>
{
    [Header("Debug")]
    [SerializeField] private bool m_DebugTextOn = false;
    public static bool g_DebugTextOn => Instance.m_DebugTextOn;
}