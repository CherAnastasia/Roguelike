using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControls : MonoBehaviour
{
    public void PlayPressed()
    {
        SceneManager.LoadScene("BasementMain");
    }
    public void Exit()
    {
        Application.Quit();
        Debug.Log("Exit pressed!");
    }
    public void ExitPressed()
    {
        SceneManager.LoadScene("Exit");
    }
    public void RefusalExit()
    {
        SceneManager.LoadScene("Menu");
    }
}
