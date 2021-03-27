using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class DifficultyDropdown : MonoBehaviour
{
    [SerializeField]
    private Dropdown _dropdown = null;


    private int _difficulty = 1;

    private void Start()
    {
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<MenuManager>().saveButtonClick += OnSave;
        
        _difficulty = PlayerPrefs.GetInt("difficulty", 1);
        _dropdown.value = _difficulty;
    }

    public void OnDropdownSelect(int selected)
    {
        _difficulty = selected;
    }

    private void OnSave()
    {
        PlayerPrefs.SetInt("difficulty", _difficulty);
    }

}
