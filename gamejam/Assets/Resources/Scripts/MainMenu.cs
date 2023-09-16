using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button m_StartButton;
    [SerializeField] private Button m_QuitButton;

    private void Awake()
    {
        m_StartButton.onClick.AddListener(TransitionToNextScene);
        m_QuitButton.onClick.AddListener(OnQuitGame);
    }

    private void OnDestroy()
    {
        m_StartButton.onClick.RemoveListener(TransitionToNextScene);
        m_QuitButton.onClick.RemoveListener(OnQuitGame);
    }

    private void TransitionToNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void OnQuitGame()
    {
        Application.Quit();
    }
}