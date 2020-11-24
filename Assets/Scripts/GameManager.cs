using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject restartPanel;
    public GameObject completedPanel;
    public TextMeshProUGUI score;

    public int levelIndex = 0;
    public float timeToComplete;
    private float _score = 0;

    public static bool _gameOver = false;


    private void Update()
    {
        if (_gameOver)
            return;

        _score += Time.deltaTime;
        if (score != null)
            score.text = "SCORE: " + _score.ToString("F0");

        if (_score >= timeToComplete)
        {
            LevelCompelte();
        }
    }

    public void GameOver()
    {
        if (!_gameOver)
        {
            _gameOver = true;
            Invoke("DelayedGameOver", 1);
        }
    }

    public void DelayedGameOver()
    {
        restartPanel.SetActive(true);
    }

    public void PlayButton()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void RestartBtn()
    {
        _score = 0;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MenuBtn()
    {
        SceneManager.LoadScene("Main Menu");
    }

    void LevelCompelte()
    {
        _gameOver = true;
        completedPanel.SetActive(true);
    }

    public void NextLevel()
    {
        _score = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}