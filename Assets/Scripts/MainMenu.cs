using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void playgame()
    {
        SceneManager.LoadScene("Cube");
    }

    public void quit()
    {
        Debug.Log("QUIT !");
        Application.Quit();
    }



}
