using UnityEngine;
using TMPro;

public class PointsManager : Singleton<PointsManager>
{
    private int m_Points = 0;
    public int Points => Instance.m_Points;

    protected override void HandleAwake()
    {
        base.HandleAwake();
        GlobalEvents.EnemyEvents.OnEnemyDeath += HandleEnemyDeath;
    }

    protected override void HandleDestroy()
    {
        base.HandleDestroy();
        GlobalEvents.EnemyEvents.OnEnemyDeath -= HandleEnemyDeath;
    }

    private void HandleEnemyDeath()
    {
        m_Points += 1;
    }
}