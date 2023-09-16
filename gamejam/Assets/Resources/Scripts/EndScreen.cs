using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private Button m_ReplayButton;
    [SerializeField] private Button m_ReturnToMainButton;

    private void Awake()
    {
        m_ReplayButton.onClick.AddListener(RestartGame);
        m_ReturnToMainButton.onClick.AddListener(ReturnToMainMenu);
    }

    private void OnDestroy()
    {
        m_ReplayButton.onClick.RemoveListener(RestartGame);
        m_ReturnToMainButton.onClick.RemoveListener(ReturnToMainMenu);
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    private void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
