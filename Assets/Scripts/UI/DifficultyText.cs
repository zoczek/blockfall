using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class DifficultyText : MonoBehaviour
{
    private Text _text = null;
    [SerializeField]
    private string[] _difficultyNames = { "Easy", "Normal", "Hard" };


    private void Awake()
    {
        _text = GetComponentInChildren<Text>();
        _text.text = "Difficulty: " + _difficultyNames[PlayerPrefs.GetInt("difficulty", 1)];
    }


}
