using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zoomcam : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel")>0 && transform.localScale.z>2.5){
            GetComponent<Transform>().localScale= new Vector3(transform.localScale.x,transform.localScale.y+0.2f,transform.localScale.z-0.5f);
        }

        if (Input.GetAxis("Mouse ScrollWheel")<0 && transform.localScale.z<6){
            GetComponent<Transform>().localScale= new Vector3(transform.localScale.x,transform.localScale.y-0.2f,transform.localScale.z+0.5f);
        }
    }
}
