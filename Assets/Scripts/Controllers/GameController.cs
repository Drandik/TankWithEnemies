using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : Singleton<GameController>
{
    public GameObject PausePanel;
    public GameObject GameOverPanel;
    public Text Score;

    private int score;

    private void Start()
    {
        PausePanel.SetActive(false);
        GameOverPanel.SetActive(false);
        Time.timeScale = 1;
        Score.text = "0";
        score = 0;
    }

    public void SetScore()
    {
        score++;
        Score.text = score.ToString();
    }

    private void Update()
    {
        if (InputController.Instance.Cancel && !GameOverPanel.activeSelf)
            Pause(!PausePanel.activeSelf);
    }

    public void Pause(bool toPause)
    {
        if (toPause)
        {
            Time.timeScale = 0;
            PausePanel.SetActive(true);
        }
        else
        {
            PausePanel.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void Reload()
    {
        SceneManager.LoadScene("Main");
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        GameOverPanel.SetActive(true);
    }
}
