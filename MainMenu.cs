using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Multiplayer()
    {
        SceneManager.LoadScene("Multiplayer");
    }
    public void Setting()
    {
        SceneManager.LoadScene("Setting");
    }
    public void Exit()
    {
        Application.Quit();
    }
}
