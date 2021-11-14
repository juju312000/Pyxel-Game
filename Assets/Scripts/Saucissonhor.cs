using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saucissonhor : MonoBehaviour
{
    [SerializeField] float m_Speed;
    [SerializeField] char m_Rotation;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (m_Rotation=='1')
        {
            transform.Rotate(Vector3.right * m_Speed * Time.deltaTime);
        }
        
        if (m_Rotation=='2')
        {
            transform.Rotate(Vector3.left * m_Speed * Time.deltaTime);
        }
    }
}
