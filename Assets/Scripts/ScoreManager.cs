
using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public TextMeshProUGUI scoreText,highScoreText;
    public float animationDuration = 2.0f;

    private int currentScore;
    private int highScore;
    private int finalScore;
    private float elapsedTime;

    private void Awake()
    {
        Instance=this;
        highScore = PlayerPrefs.GetInt("High Score", 0);
     
    }
    void Start()
    {
           GameManager.Instance.OnPlayerLost += OnPlayerLost;
    }

    private void OnPlayerLost()
    {
        print("Entered");
        currentScore=0;
        CheckHighScore();
        StartCoroutine(CountScore());
    }

    private void CheckHighScore()
    {
        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("High Score", highScore);
        }
    }

    public void IncreaseScore(int score)
    {
        currentScore += score;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + currentScore;
    }

    private IEnumerator CountScore()
    {
        print("Joined");
        while (currentScore < finalScore && elapsedTime < animationDuration)
        {
            float progress = elapsedTime / animationDuration;
            currentScore = (int)Mathf.Lerp(0, finalScore, progress);
            highScoreText.text=$"{currentScore}";

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}