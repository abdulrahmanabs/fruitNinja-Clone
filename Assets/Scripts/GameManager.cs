using UnityEngine;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    public Action OnPlayerLost;
    public static GameManager Instance { get; private set; }

    public enum GameState
    {
        Pause, Play
    }

    public GameState gameState { get; private set; } = GameState.Play;

    [SerializeField] private GameObject losePanel;

    private float fallingFruits;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        StartGame();
    }

    public void Pause()
    {
        Time.timeScale = 0;
        gameState = GameState.Pause;
    }

    public void Lose()
    {
        Pause();
        OnPlayerLost?.Invoke();
        SetActive(losePanel, true);
        Spawner.Instance.ClearAllObjects();
    }

    public void StartGame()
    {
        SetActive(losePanel, false);
        gameState = GameState.Play;
        Time.timeScale = 1;
        fallingFruits = 0;
    }

    public void IncreaseFallingFruits()
    {
        if (fallingFruits >= 3)
        {
            Lose();
        }
        else
        {
            fallingFruits++;
        }
    }

    private void SetActive(GameObject obj, bool active)
    {
        obj.SetActive(active);
    }
}
