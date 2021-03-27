using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public delegate void ButtonClickDelegate();
    public ButtonClickDelegate returnButtonClick;
    public ButtonClickDelegate saveButtonClick;

    public void OnReturnButtonClick()
    {
        returnButtonClick?.Invoke();
        SceneManager.LoadScene("MainMenu");
    }

    public void OnSaveButtonClick()
    {
        saveButtonClick?.Invoke();
        PlayerPrefs.Save();
        SceneManager.LoadScene("MainMenu");
    }
}
