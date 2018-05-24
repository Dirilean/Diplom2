﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public void PlayPressed()
    {
        SceneManager.LoadScene("Loading");
    }

    public void ExitPressed()
    {
        Application.Quit();
    }
}
