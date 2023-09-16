using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private Button m_ReplayButton;
    [SerializeField] private Button m_ReturnToMainButton;
    [SerializeField] private TMP_Text scoreText;

    private void Awake()
    {
        m_ReplayButton.onClick.AddListener(RestartGame);
        m_ReturnToMainButton.onClick.AddListener(ReturnToMainMenu);
        scoreText.SetText("Final Score: " + PointsManager.Instance.Points);
    }

    private void OnDestroy()
    {
        m_ReplayButton.onClick.RemoveListener(RestartGame);
        m_ReturnToMainButton.onClick.RemoveListener(ReturnToMainMenu);
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        GlobalEvents.LeaveEndScreenEvent?.Invoke();
    }

    private void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
        GlobalEvents.LeaveEndScreenEvent?.Invoke();
    }
}
