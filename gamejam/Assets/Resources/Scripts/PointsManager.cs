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
        GlobalEvents.LeaveEndScreenEvent += HandleLeaveEndScreen;
    }

    protected override void HandleDestroy()
    {
        base.HandleDestroy();
        GlobalEvents.EnemyEvents.OnEnemyDeath -= HandleEnemyDeath;
        GlobalEvents.LeaveEndScreenEvent -= HandleLeaveEndScreen;
    }

    private void HandleEnemyDeath()
    {
        m_Points += 1;
    }

    private void HandleLeaveEndScreen()
    {
        m_Points = 0;
    }
}