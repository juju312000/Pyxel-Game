using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static string scoreValue = "0";


    private void OnGUI()
    {
        GUI.Label(new Rect(20, 20, 200, 40), "Score: " + scoreValue);
    }
}
