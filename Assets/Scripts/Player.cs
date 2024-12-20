using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerState
{
    ALIVE,
    DEAD,
}

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerData m_playerData;
    private PlayerInput m_playerInput;

    private float m_health = 0;
    private float m_maxHealth = 0;
    private float m_movementSpeed = 0;
    private float m_rotationSpeed = 0;
    private float m_fireRate = 0;

    private float m_fireRateTimer = 0;

    private SpawnPoolManager m_spawnPoolManager;
    private HealthBarUI m_healthBarUI;

    private PlayerState m_currentState;

    // Minimum movement to consider a joystick value
    private const float AIM_MIN_MOVEMENT = 0.01f;

    private const string MOVEMENT_INPUT = "Movement";
    private const string AIM_INPUT = "Aim";

    private void Start()
    {
        m_currentState = PlayerState.ALIVE;
        m_playerInput = GetComponent<PlayerInput>();
        m_healthBarUI = GameManager.Instance.UIManager.HealthBarUI;
        m_spawnPoolManager = GameManager.Instance.SpawnPoolManager;

        InitHealth();
    }

    private void Update()
    {
        if(m_currentState == PlayerState.DEAD ||
            GameManager.Instance.GameState != GameState.IN_GAME)
        {
            return;
        }

        Vector2 movementInput = m_playerInput.actions[MOVEMENT_INPUT].ReadValue<Vector2>();
        Vector2 aimInput = m_playerInput.actions[AIM_INPUT].ReadValue<Vector2>();

        this.transform.position += m_movementSpeed * Time.deltaTime * new Vector3(movementInput.x, 0, movementInput.y);

        // Check joystick value for change before setting the aim rotation
        if(aimInput.sqrMagnitude > AIM_MIN_MOVEMENT)
        {
            float angle = Mathf.Atan2(aimInput.x, aimInput.y) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0f, angle, 0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, m_rotationSpeed * Time.deltaTime);

            ShootBullets();
        }
    }

    public void Damage(float p_damage)
    {
        if (m_health <= 0)
        {
            return;
        }

        m_health -= p_damage;

        float percentage = m_health / m_maxHealth;
        m_healthBarUI.SetHealth(percentage);

        if (m_health <= 0)
        {
            m_currentState = PlayerState.DEAD;

            GameObject particle = m_spawnPoolManager.GetObject(PooledObjID.PLAYER_KILLED_VFX);
            particle.GetComponent<ParticleSystem>().Stop();
            particle.GetComponent<ParticleSystem>().Play();
            particle.transform.position = this.transform.position + new Vector3(0, 2, 0);

            this.gameObject.SetActive(false);

            GameManager.Instance.SetPlayerLose();
        }
        else
        {
            GameObject particle = m_spawnPoolManager.GetObject(PooledObjID.PLAYER_DAMAGE_VFX);
            particle.GetComponent<ParticleSystem>().Stop();
            particle.GetComponent<ParticleSystem>().Play();
            particle.transform.position = this.transform.position + new Vector3(0, 2, 0);
        }

    }

    private void ShootBullets()
    {
        m_fireRateTimer += Time.deltaTime;

        if(m_fireRateTimer > m_fireRate)
        {
            m_fireRateTimer = 0;
            GameObject bullet = m_spawnPoolManager.GetObject(PooledObjID.BULLET_PREFAB);
            bullet.transform.position = this.transform.position;
            bullet.transform.localEulerAngles = this.transform.localEulerAngles;
            bullet.GetComponent<Bullets>().Shoot();
        }
    }

    private void InitHealth()
    {
        m_health = m_playerData.MaxHealth;
        m_maxHealth = m_playerData.MaxHealth;
        m_movementSpeed = m_playerData.MovementSpeed;
        m_rotationSpeed = m_playerData.RotationSpeed;
        m_fireRate = m_playerData.FireRate;

        m_healthBarUI.SetHealth(m_maxHealth);
    }
}
