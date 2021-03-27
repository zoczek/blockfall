using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class GameOverMessage : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText = null;


    private void Start()
    {
        GameObject.FindGameObjectWithTag("GameManager").GetComponentInChildren<GameManager>().gameOver += OnGameOver;
        gameObject.SetActive(false);
    }


    private void OnGameOver(int score)
    {
        gameObject.SetActive(true);
        _scoreText.text = "Your Score: " + score.ToString();
    }

}
