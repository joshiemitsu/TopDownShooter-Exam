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
    [SerializeField] private PlayerInput m_playerInput;

    [SerializeField] private float m_health = 0;
    [SerializeField] private float m_maxHealth = 100;

    [SerializeField] private float m_movementSpeed = 0;
    [SerializeField] private float m_rotationSpeed = 0;

    [Space]
    [SerializeField] private GameObject m_bulletPrefab;
    [SerializeField] private float m_fireRate = 0;
    private float m_fireRateTimer = 0;

    [SerializeField] private HealthBarUI m_healtBarUI;

    [SerializeField] private PlayerState m_currentState;

    // Minimum movement to consider a joystick value
    private const float AIM_MIN_MOVEMENT = 0.01f;

    private const string MOVEMENT_INPUT = "Movement";
    private const string AIM_INPUT = "Aim";

    private void Awake()
    {
        m_currentState = PlayerState.ALIVE;
        m_playerInput = GetComponent<PlayerInput>();
        m_healtBarUI = GameManager.Instance.GetUIManager().GetHealthBarUI();

        InitHealth();
    }

    private void Update()
    {
        if(m_currentState == PlayerState.DEAD)
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
        m_health -= p_damage;

        if(m_health < 0)
        {
            m_currentState = PlayerState.DEAD;
        }
        else
        {
            float percentage = m_health / m_maxHealth;
            m_healtBarUI.SetHealth(percentage);
        }

        Debug.Log("Damage! Current Health: " + m_health);
    }

    private void ShootBullets()
    {
        m_fireRateTimer += Time.deltaTime;

        if(m_fireRateTimer > m_fireRate)
        {
            m_fireRateTimer = 0;
            GameObject bullet = Instantiate(m_bulletPrefab, this.transform.position, Quaternion.identity);
            bullet.transform.localEulerAngles = this.transform.localEulerAngles;
            bullet.GetComponent<Bullets>().Shoot();
        }
    }

    private void InitHealth()
    {
        m_health = m_maxHealth;
        m_healtBarUI.SetHealth(m_maxHealth);
    }
}
