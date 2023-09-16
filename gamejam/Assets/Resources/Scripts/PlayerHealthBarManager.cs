using UnityEngine;
using UnityEngine.UI;
public class PlayerHealthBarManager : Singleton<PlayerHealthBarManager>
{
    [SerializeField] Slider m_Slider;
    private int m_Health;

    protected override void HandleAwake()
    {
        base.HandleAwake();
        m_Slider.maxValue = GlobalSettings.g_FoodHealthValue;
        m_Health = GlobalSettings.g_FoodHealthValue;
        m_Slider.value = m_Health;
        GlobalEvents.FoodEvents.FoodHealthEvent += HandleFoodHealthEvent;
    }

    protected override void HandleDestroy()
    {
        base.HandleDestroy();
        GlobalEvents.FoodEvents.FoodHealthEvent -= HandleFoodHealthEvent;
    }

    private void HandleFoodHealthEvent(int changeInHealth)
    {
        m_Health += changeInHealth;
        m_Slider.value = m_Health;
        if (m_Health == 0)
            GlobalEvents.GameOverEvent?.Invoke();
    }

}