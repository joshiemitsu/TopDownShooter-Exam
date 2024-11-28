using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    INIT,
    MOVE,
    ATTACK,
    DEAD
}

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyData m_enemyData;
    private EnemyState m_currentState;
    private Player m_targetPlayer;

    private float m_health = 0;
    private float m_maxHealth = 0;
    private float m_attackCooldown = 0;
    private float m_attackDamage = 0;
    private float m_attackRange = 0;
    private float m_movementSpeed = 0;
    private float m_attackTimer = 0;

    private void OnEnable()
    {
        m_health = m_enemyData.MaxHealth;
        m_maxHealth = m_enemyData.MaxHealth;
        m_attackDamage = m_enemyData.AttackDamage;
        m_attackRange = m_enemyData.AttackRange;
        m_attackCooldown = m_enemyData.AttackCooldown;
        m_attackTimer = m_enemyData.AttackCooldown;
        m_movementSpeed = m_enemyData.MovementSpeed;

        ChangeState(EnemyState.INIT);
    }

    private void Update()
    {
        if(GameManager.Instance.GameState != GameState.IN_GAME)
        {
            return;
        }

        switch (m_currentState)
        {
            case EnemyState.INIT:
                {

                }
                break;
            case EnemyState.MOVE:
                {
                    MoveToTarget();
                }
                break;
            case EnemyState.ATTACK:
                {
                    AttackTarget();
                }
                break;
            case EnemyState.DEAD:
                {

                }
                break;
            default:
                {

                }
                break;
        }
    }

    private void ChangeState(EnemyState p_newState)
    {
        m_currentState = p_newState;

        switch (m_currentState)
        {
            case EnemyState.INIT:
                {
                    Init();
                }
                break;
            case EnemyState.MOVE:
                {

                }
                break;
            case EnemyState.ATTACK:
                {
                }
                break;
            case EnemyState.DEAD:
                {
                    this.gameObject.SetActive(false);
                }
                break;
            default:
                {

                }
                break;
        }
    }

    private void Init()
    {
        ChangeState(EnemyState.MOVE);
    }

    private void MoveToTarget()
    {
        // Set the target if there is no target yet.
        if (m_targetPlayer == null)
        {
            Player player = GameManager.Instance.Player;
            if (player)
            {
                m_targetPlayer = player;
            }

        }

        // If target is within range proceed to attack again.
        if (IsWithinAttackRange())
        {
            ChangeState(EnemyState.ATTACK);
            return;
        }

        Vector3 direction = (m_targetPlayer.transform.position - this.transform.position).normalized;
        this.transform.position += direction * m_movementSpeed * Time.deltaTime;
    }

    private void AttackTarget()
    {
        // If target is too far away make enemy move again.
        if (!IsWithinAttackRange())
        {
            ChangeState(EnemyState.MOVE);
            return;
        }

        m_attackTimer += Time.deltaTime;

        if(m_attackTimer >= m_attackCooldown)
        {
            m_attackTimer = 0;
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, m_attackRange);

            foreach(Collider hitCollider in hitColliders)
            {
                Player m_targetPlayer = hitCollider.GetComponent<Player>();
                if(m_targetPlayer)
                {
                    m_targetPlayer.Damage(m_attackDamage);
                }
            }
        }
    }

    private bool IsWithinAttackRange()
    {
        return (Vector3.Distance(m_targetPlayer.transform.position, this.transform.position) < m_attackRange);
    }

    public void Damage(float p_damage)
    {
        m_health -= p_damage;

        if(m_health <= 0)
        {
            // Set Data and UI
            GameManager.Instance.ScoreManager.EnemyKilled++;
            int enemyKilled = GameManager.Instance.ScoreManager.EnemyKilled;
            GameManager.Instance.UIManager.EnemiesKilledUI.SetText(enemyKilled);

            // Display effects
            GameObject particle = GameManager.Instance.SpawnPoolManager
                                    .GetObject(PooledObjID.ENEMY_KILLED_VFX);
            particle.GetComponent<ParticleSystem>().Stop();
            particle.GetComponent<ParticleSystem>().Play();
            particle.transform.position = this.transform.position;

            ChangeState(EnemyState.DEAD);
        }
    }
}
