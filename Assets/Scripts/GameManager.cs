using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Pause,Play
    }
    public static GameManager instance;
    public GameState gameState;
    int currentScore = 0,highScore;
    [SerializeField] GameObject startPnl, losePnl;
    [SerializeField] TextMeshProUGUI scoreText, highScoreText;
    //Player player;
    private void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);
        else
            instance = this;

       highScore= PlayerPrefs.GetInt("High Score", 0);
       // player = FindObjectOfType<Player>();
    }
    public void Pause()
    {
        Time.timeScale = 0;
        gameState = GameState.Pause;
    }
    public void Lose()
    {
        SoundManager.instance.PlaySound(SoundManager.sounds.hit);
        Pause();
        if(currentScore>highScore)
        {
            PlayerPrefs.SetInt("High Score", currentScore);
            highScore = PlayerPrefs.GetInt("High Score");
            highScoreText.text = "New High Score : " + highScore.ToString();
        }
        highScoreText.text ="High Score : "+ highScore.ToString();
        losePnl.gameObject.SetActive(true);
    }
    public void IncreaseScore()
    {
        currentScore++;
        scoreText.text = currentScore.ToString();
        SoundManager.instance.PlaySound(SoundManager.sounds.Score);
    }
    public void StartGame()
    {
        startPnl.gameObject.SetActive(false);
        losePnl.gameObject.SetActive(false);
        gameState = GameState.Play;
        currentScore = 0;
        //player.transform.position = player.startPos;
        scoreText.text = string.Empty;
        ObjectPooler.instance.DisableAll();
        Time.timeScale = 1;
    }
}
