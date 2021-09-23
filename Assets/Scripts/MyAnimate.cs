using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyMathTools;

public class MyAnimate : MonoBehaviour
{
    delegate Vector3 ComputePositionDelegate(float t);

    //attribute in java -> field in C#

    ComputePositionDelegate m_AnimationMethod;
    [SerializeField] float m_Rho;
    [SerializeField] float m_Speed;

    Vector3 MyAnimationMethod1(float t)
    {
        Cylindrical cyl = new Cylindrical(m_Rho, t * m_Speed, Mathf.PingPong(t * m_Speed / 4, 6));

        return CoordConvert.CylindricalToCartesian(cyl);
    }
    // Start is called before the first frame update
    void Start()
    {
        m_AnimationMethod = MyAnimationMethod1;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position += new Vector3(4f * Time.deltaTime, 0, 0);

        transform.position = m_AnimationMethod(Time.time);
        
    }
}
