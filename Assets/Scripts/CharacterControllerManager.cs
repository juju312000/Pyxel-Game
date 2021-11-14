using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
public class CharacterControllerManager : MonoBehaviour,ISpeed
{
    [SerializeField] CharacterController m_CharacterController;
    [SerializeField] float m_MaxTranslationSpeed=10;   //unit: m/s

    float m_TranslationSpeed=0;

    [SerializeField] float m_RotationSpeed;

    [SerializeField] float m_JumpSpeed;
    float ySpeed ;

    public float SpeedRatio { get { return Mathf.Abs(m_TranslationSpeed) / m_MaxTranslationSpeed; } }

    private void Awake()
	{
        if (!m_CharacterController) m_CharacterController = GetComponent < CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Player").transform.position.y < -20)
        {
            SceneManager.LoadScene("DefeatMenu");
        }
        if (Score.scoreValue == "10")
        {
            SceneManager.LoadScene("Win");
        }
        ySpeed += Physics.gravity.y *Time.deltaTime;

        if (m_CharacterController.isGrounded){

            if(Input.GetButtonDown("Jump"))
            {
                ySpeed = m_JumpSpeed;
            }

        }

        
        

        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");

        m_TranslationSpeed = Mathf.Lerp(-m_MaxTranslationSpeed, m_MaxTranslationSpeed,(vInput+1)*0.5f);

        Vector3 velocity = transform.forward * m_TranslationSpeed;
        velocity.y=ySpeed;


        m_CharacterController.Move(velocity * Time.deltaTime);

        transform.Rotate(Vector3.up, m_RotationSpeed * Time.deltaTime * hInput);
    }
}
