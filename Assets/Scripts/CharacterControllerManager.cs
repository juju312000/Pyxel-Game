using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterControllerManager : MonoBehaviour,ISpeed
{
    [SerializeField] CharacterController m_CharacterController;
    [SerializeField] float m_MaxTranslationSpeed=10;   //unit: m/s
    float m_TranslationSpeed=0;

    [SerializeField] float m_RotationSpeed;

    public float SpeedRatio { get { return Mathf.Abs(m_TranslationSpeed) / m_MaxTranslationSpeed; } }

    private void Awake()
	{
        if (!m_CharacterController) m_CharacterController = GetComponent < CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");

        m_TranslationSpeed = Mathf.Lerp(-m_MaxTranslationSpeed, m_MaxTranslationSpeed,(vInput+1)*.5f);


        m_CharacterController.SimpleMove(transform.forward * m_TranslationSpeed);
        transform.Rotate(Vector3.up, m_RotationSpeed * Time.deltaTime*hInput);
    }
}
