using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FolowPlatfome : MonoBehaviour
{
    [SerializeField] CharacterController m_CharacterController;
    [SerializeField] GameObject myCarpet;


    void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.name == myCarpet.name)
        {
            Debug.Log(col.gameObject.name);
            m_CharacterController.gameObject.transform.SetParent(myCarpet.transform);
        }
    }

    void OnCollisionExit(Collision col)
    {

        if (col.gameObject.name == myCarpet.name)
        {
            Debug.Log("Leave");
            m_CharacterController.gameObject.transform.SetParent(null);
        }
    }
}