using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DefeatMenu : MonoBehaviour
{
    public void playagain()
    {
        SceneManager.LoadScene("Menu");
    }

}
