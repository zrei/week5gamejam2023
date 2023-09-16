using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    protected override void HandleAwake()
    {
        base.HandleAwake();
        GlobalEvents.GameOverEvent += HandleGameOver;
    }

    protected override void HandleDestroy()
    {
        base.HandleDestroy();
        GlobalEvents.GameOverEvent -= HandleGameOver;
    }

    private void HandleGameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
