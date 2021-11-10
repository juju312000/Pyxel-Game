using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
    }
}
