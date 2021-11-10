using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRotation : MonoBehaviour
{
    Quaternion qStart, qEnd;
    // Start is called before the first frame update
    void Start()
    {
        //Quaternion orientationQuaternion = Quaternion.AngleAxis(45,Vector3.up);
        //transform.rotation = orientationQuaternion;

        qStart = Quaternion.AngleAxis(45, Vector3.up);
        qEnd = Quaternion.AngleAxis(90, Vector3.up);
    }

    // Update is called once per frame
    void Update()
    {
        //Quaternion rotQ = Quaternion.AngleAxis(10 * Time.deltaTime, transform.right);
        //transform.rotation = rotQ * transform.rotation;

        transform.rotation = Quaternion.Slerp(qStart, qEnd, Time.time*.1f);
    }
}
