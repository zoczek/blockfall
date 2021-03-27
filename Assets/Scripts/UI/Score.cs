using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField]
    private Text _currentScore = null;
    [SerializeField]
    private Text _highScore = null;

    private void Awake()
    {
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().scoreUpdated += OnScoreUpdated;
    }

    private void Start()
    {
        _currentScore.text = "0";
        if (PlayerPrefs.HasKey("highScore"))
            _highScore.text = PlayerPrefs.GetInt("highScore").ToString();
        else
            _highScore.text = "0";
    }

    private void OnScoreUpdated(int score)
    {
        _currentScore.text = score.ToString();
    }
}
