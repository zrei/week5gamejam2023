using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button m_Button;

    private void Awake()
    {
        m_Button.onClick.AddListener(TransitionToNextScene);
    }

    private void OnDestroy()
    {
        m_Button.onClick.RemoveListener(TransitionToNextScene);
    }

    private void TransitionToNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}