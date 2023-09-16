using UnityEngine;
using UnityEngine.UI;

public class HealthBarManager : Singleton<HealthBarManager>
{
    [SerializeField] Slider m_Slider;
    private int m_Health;

    protected override void HandleAwake()
    {
        base.HandleAwake();
        m_Slider.maxValue = GlobalSettings.g_PlayerHealthValue;
        m_Health = GlobalSettings.g_PlayerHealthValue;
        m_Slider.value = m_Health;
        GlobalEvents.PlayerEvents.PlayerHealthChangeEvent += HandlePlayerHealthEvent;
    }

    protected override void HandleDestroy()
    {
        base.HandleDestroy();
        GlobalEvents.PlayerEvents.PlayerHealthChangeEvent -= HandlePlayerHealthEvent;
    }

    private void HandlePlayerHealthEvent(int changeInHealth)
    {
        m_Health += changeInHealth;
        m_Slider.value = m_Health;
        if (m_Health == 0)
            GlobalEvents.GameOverEvent?.Invoke();
    }

}