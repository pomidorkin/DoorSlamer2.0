using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameManagerScript gameManagerScript;

    public void OpenPauseMenu(bool value)
    {
        if (value)
        {
            gameManagerScript.PauseGame();
        }
        else
        {
            gameManagerScript.ResumeGame();
        }
        pauseMenu.gameObject.active = value;
    }
}
