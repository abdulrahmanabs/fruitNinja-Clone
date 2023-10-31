
using UnityEngine;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Pause, Play
    }
    public static GameManager instance;
    public GameState gameState;
    int currentScore = 0, highScore;
    [SerializeField] GameObject losePnl;
    [SerializeField] TextMeshProUGUI scoreText, highScoreText;

    float fallingFruits = 0;

    private void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);
        else
            instance = this;

        highScore = PlayerPrefs.GetInt("High Score", 0);
        StartGame();
    }
    public void Pause()
    {
        Time.timeScale = 0;
        gameState = GameState.Pause;
    }
    public void Lose()
    {
        //SoundManager.instance.PlaySound(SoundManager.sounds.hit);
        Pause();
        CheckHighScore();
        highScoreText.text = "High Score : " + highScore.ToString();
        losePnl.gameObject.SetActive(true);
    }

    private void CheckHighScore()
    {
        if (currentScore > highScore)
        {
            PlayerPrefs.SetInt("High Score", currentScore);
            highScore = PlayerPrefs.GetInt("High Score");
            highScoreText.text = "New High Score : " + highScore.ToString();
        }
    }

    public void IncreaseScore(int score)
    {
        currentScore += score;
        scoreText.text = currentScore.ToString();
    }
    public void StartGame()
    {

        losePnl.gameObject.SetActive(false);
        gameState = GameState.Play;
        currentScore = 0;
        scoreText.text = string.Empty;
        //ObjectPooler.instance.DisableAll();
        Time.timeScale = 1;
        fallingFruits = 0;
    }

    public void IncreaseFallingFruits()
    {
        if (fallingFruits >= 3)
            Lose();
        else
            fallingFruits++;
    }
}
