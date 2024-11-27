using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShutdownTimer : MonoBehaviour
{
    [SerializeField] private float m_timeToDeactivate = 0;

    private float m_shutdownTimer = 0;
    void OnEnable()
    {
        m_shutdownTimer = 0;
    }

    void Update()
    {
        m_shutdownTimer += Time.deltaTime;
        if(m_shutdownTimer > m_timeToDeactivate)
        {
            this.gameObject.SetActive(false);
            m_shutdownTimer = 0;
        }
    }
}
