using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] string m_SceneName;

    private void OnGUI()
    {
        if (GUI.Button(new Rect(Screen.width - 100, 10, 90, 50), "Switch Scene"))
            SceneManager.LoadScene(m_SceneName);
    }

}
