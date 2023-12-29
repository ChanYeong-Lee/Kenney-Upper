using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }
    public Player player;
    public int score = 0;
    public UnityEvent<int> OnScoreChange;
    public GameObject gameOverUI;
    private void Awake()
    {
        instance = this;
    }

    public void SetScore(float score)
    {
        if (score > this.score)
        {
            this.score = (int)score;
            player.jumpHeight = 3 + score / 100;
            OnScoreChange?.Invoke(this.score);
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        gameOverUI.SetActive(true);
    }
}
