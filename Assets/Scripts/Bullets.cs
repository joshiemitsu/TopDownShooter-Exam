using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    [SerializeField] private float m_damage;
    [SerializeField] private float m_speed;

    private Rigidbody m_rigidbody;

    void Awake()
    {
        m_rigidbody = this.GetComponent<Rigidbody>();
    }

    public void Shoot()
    {
        if(m_rigidbody)
        {
            m_rigidbody.velocity = this.transform.forward * m_speed;
        }
    }

    public void OnTriggerEnter(Collider p_collider)
    {
        Enemy enemy = p_collider.GetComponent<Enemy>();
        if(enemy)
        {
            enemy.Damage(m_damage);
            this.gameObject.SetActive(false);
        }
    }
}
