using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FolowPlatfome : MonoBehaviour
{
    [SerializeField] CharacterController m_CharacterController;


    void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.tag == "P" )
        {
            Debug.Log(col.gameObject.name);
            //m_CharacterController.gameObject.transform.SetParent(myCarpet.transform);
            Score.scoreValue = col.gameObject.name;
        }
    }

    void OnCollisionExit(Collision col)
    {

        if (col.gameObject.tag == "P")
        {
            Debug.Log("Leave");
            m_CharacterController.gameObject.transform.SetParent(null);
        }
    }
}