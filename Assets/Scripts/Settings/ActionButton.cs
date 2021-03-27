using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ActionButton : MonoBehaviour
{
    [SerializeField]
    private string _action = ""; 
    [SerializeField]
    private GameObject _buttonPressWindow = null;


    private Text _text = null;
    private KeyCode _key = KeyCode.None;
    private KeyCode _newKey = KeyCode.None;

    private void Awake()
    {
        _text = GetComponentInChildren<Text>();

        if (PlayerPrefs.HasKey(_action))
            _key = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(_action));
        else
            _key = DefaultControls.keys[_action];

        _newKey = _key;
        _text.text = _key.ToString();
    }

    private void Start()
    {
        _buttonPressWindow.SetActive(false);
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<MenuManager>().saveButtonClick += OnSave;
    }

    public void OnButtonClick()
    {
        StartCoroutine(WaitForKey());
    }

    private void OnSave()
    {
        PlayerPrefs.SetString(_action, _newKey.ToString());
    }

    private IEnumerator WaitForKey()
    {
        _buttonPressWindow.SetActive(true);
        EventSystem.current.SetSelectedGameObject(_buttonPressWindow, null);
        Input.ResetInputAxes();

        yield return new WaitUntil(() => Input.anyKey);
        foreach (KeyCode code in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                break;

            if (Input.GetKeyDown(code))
            {
                _newKey = code;
                break;
            }
        }

        _buttonPressWindow.SetActive(false);
        _text.text = _newKey.ToString();
        EventSystem.current.SetSelectedGameObject(gameObject, null);
    }

}
