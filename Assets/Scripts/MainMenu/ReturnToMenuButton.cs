﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class ReturnToMenuButton: MonoBehaviour
{
    public void OnButtonClick()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
