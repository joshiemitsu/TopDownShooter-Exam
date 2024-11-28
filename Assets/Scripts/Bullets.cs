using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    [SerializeField] private float m_damage = 0;
    [SerializeField] private float m_speed = 0;

    private SpawnPoolManager m_spawnPoolManager = null;
    private Rigidbody m_rigidbody = null;

    private void OnEnable()
    {
        if(m_rigidbody == null)
        {
            m_rigidbody = this.GetComponent<Rigidbody>();
        }

        if(m_spawnPoolManager == null)
        {
            m_spawnPoolManager = GameManager.Instance.SpawnPoolManager;
        }
    }

    public void Shoot()
    {
        if(m_rigidbody)
        {
            m_rigidbody.velocity = this.transform.forward * m_speed;
        }
    }

    private void OnTriggerEnter(Collider p_collider)
    {
        Enemy enemy = p_collider.GetComponent<Enemy>();
        if(enemy)
        {
            enemy.Damage(m_damage);

            GameObject particle = m_spawnPoolManager.GetObject(PooledObjID.BULLET_HIT_VFX);
            particle.GetComponent<ParticleSystem>().Stop();
            particle.GetComponent<ParticleSystem>().Play();
            particle.transform.position = this.transform.position;

            this.gameObject.SetActive(false);
        }
    }
}
