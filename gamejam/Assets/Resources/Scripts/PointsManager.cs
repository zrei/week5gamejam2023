using UnityEngine;
using TMPro;

public class PointsManager : Singleton<PointsManager>
{
    private int m_Points = 0;
    
    [SerializeField] private TMP_Text m_Text;
    private string m_ScoreText = "Score: ";

    protected override void HandleAwake()
    {
        base.HandleAwake();
        GlobalEvents.EnemyEvents.OnEnemyDeath += HandlePlayerDeath;
    }

    protected override void HandleDestroy()
    {
        base.HandleDestroy();
        GlobalEvents.EnemyEvents.OnEnemyDeath -= HandlePlayerDeath;
    }

    private void HandlePlayerDeath()
    {
        m_Points += 1;
        m_Text.text = m_ScoreText + m_Points;
    }
}