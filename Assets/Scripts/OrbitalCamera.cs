using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyMathTools;

public class OrbitalCamera : MonoBehaviour
{
    [SerializeField] float m_Rho;
    [SerializeField] float m_Theta;
    [SerializeField] float m_Phi;

    //[SerializeField] Spherical m_SphCoord;

    [SerializeField] Transform m_Target;

    Vector3 m_PreviousMousePos;

    // Start is called before the first frame update
    void Start()
    {
        m_PreviousMousePos = Input.mousePosition;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentMousePos = Input.mousePosition;
        Vector3 mouseVect = currentMousePos - m_PreviousMousePos;

        //mouseVect.x .... in pixels
        //mouseVect.y ... in pixels
        m_Theta += mouseVect.x * 0.1f;
        m_Phi = Mathf.Clamp(mouseVect.y * 0.1f,0.1f*Mathf.PI,99f*Mathf.PI);
        Spherical m_SphCoord = new Spherical(m_Rho, m_Theta, m_Phi);
        transform.position = m_Target.position + CoordConvert.SphericalToCartesian(m_SphCoord);
        transform.LookAt(m_Target);


        m_PreviousMousePos = currentMousePos;
    }
}
