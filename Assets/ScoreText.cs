using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    TextMeshProUGUI text;
    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        GameManager.Instance.OnScoreChange.AddListener(ScoreUpdate);
    }
    private void ScoreUpdate(int score)
    {
        text.text = score.ToString();
    }
}
