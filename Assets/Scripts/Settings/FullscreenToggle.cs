using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class FullscreenToggle : MonoBehaviour
{
    [SerializeField]
    private Toggle _toggle = null;
    private bool _fullscreen = false;

    private void Awake()
    {
        _fullscreen = Screen.fullScreen;
        _toggle.isOn = Screen.fullScreen;
    }

    private void Start()
    {
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<MenuManager>().returnButtonClick += OnReturn;
    }

    public void OnToggle(bool val)
    { 
        Screen.fullScreen = val;
    }

    private void OnReturn()
    {
        Screen.fullScreen = _fullscreen;
    }
}
