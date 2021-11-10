using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHoriz : MonoBehaviour
{

    [SerializeField] char m_typeDeDéplacement;
    [SerializeField] float m_Speed;
    [SerializeField] float m_deplacementHaut;
    [SerializeField] float m_deplacementBas;

    float deltaDeplacement;
    float positionInitialex;
    float positionInitialey;
    float positionInitialez;

    // Start is called before the first frame update
    void Start()
    {
        deltaDeplacement = m_deplacementHaut + m_deplacementBas;
        positionInitialex = GetComponent<Transform>().position.x;
        positionInitialey = GetComponent<Transform>().position.y;
        positionInitialez = GetComponent<Transform>().position.z;

    }


    // Update is called once per frame
    void Update()
    {
        if (m_typeDeDéplacement=='z')
        {
                        transform.position= new Vector3(positionInitialex, positionInitialey,positionInitialez +Mathf.PingPong(Time.time * m_Speed, deltaDeplacement)-m_deplacementBas);
        }
    }   
}
