using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraShakeTest : MonoBehaviour
{
    public GameObject m_shakeAmountSlider;

    private float m_shakeIntensity;
    private float m_shakeDecay;

    private Vector3 m_originPosition;
    private Quaternion m_originRotation;

    void Start()
    {
        m_originPosition = transform.position;
        m_originRotation = transform.rotation;
    }

    void Update()
    {
        if (m_shakeIntensity > 0)
        {
            transform.position = m_originPosition + Random.insideUnitSphere * m_shakeIntensity;
            transform.rotation = new Quaternion(
                                                    m_originRotation.x + Random.Range(-m_shakeIntensity, m_shakeIntensity) * 0.1f,
                                                    m_originRotation.y + Random.Range(-m_shakeIntensity, m_shakeIntensity) * 0.1f,
                                                    m_originRotation.z + Random.Range(-m_shakeIntensity, m_shakeIntensity) * 0.1f,
                                                    m_originRotation.w + Random.Range(-m_shakeIntensity, m_shakeIntensity) * 0.1f
                                               );
            m_shakeIntensity -= m_shakeDecay;
        }
        else
        {
            transform.position = Vector3.back * 10;
            transform.rotation = Quaternion.Euler(Vector3.zero);
        }
    }

    public void Shake()
    {
        if (m_shakeIntensity <= 0)
        {
            m_shakeIntensity = m_shakeAmountSlider.GetComponent<Slider>().value;
            m_shakeDecay = 0.03f;
        }
    }
}
