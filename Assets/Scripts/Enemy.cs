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
    [SerializeField] private EnemyState m_currentState;
    [SerializeField] private Player m_targetPlayer;

    private float m_attackTimer = 0;

    private void OnEnable()
    {
        Debug.Log("SetInitialHealth");
        m_enemyData.Health = m_enemyData.MaxHealth;
        m_attackTimer = m_enemyData.AttackCooldown;
        ChangeState(EnemyState.INIT);
    }

    private void Update()
    {
        if(GameManager.Instance.GetGameState() != GameState.IN_GAME)
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

    public void ChangeState(EnemyState p_newState)
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

    public void Init()
    {
        ChangeState(EnemyState.MOVE);
    }

    private void MoveToTarget()
    {
        // Set the target if there is no target yet.
        if (m_targetPlayer == null)
        {
            Player player = GameManager.Instance.GetPlayer();
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
        this.transform.position += direction * m_enemyData.MovementSpeed * Time.deltaTime;
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

        if(m_attackTimer >= m_enemyData.AttackCooldown)
        {
            m_attackTimer = 0;
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, m_enemyData.AttackRange);

            foreach(Collider hitCollider in hitColliders)
            {
                Player m_targetPlayer = hitCollider.GetComponent<Player>();
                if(m_targetPlayer)
                {
                    m_targetPlayer.Damage(m_enemyData.AttackDamage);
                }
            }
        }
    }

    private bool IsWithinAttackRange()
    {
        return (Vector3.Distance(m_targetPlayer.transform.position, this.transform.position) < m_enemyData.AttackRange);
    }

    public void Damage(float p_damage)
    {
        m_enemyData.Health -= p_damage;

        if(m_enemyData.Health <= 0)
        {
            // Set Data and UI
            GameManager.Instance.GetScoreManager().EnemyKilled++;
            int enemyKilled = GameManager.Instance.GetScoreManager().EnemyKilled;
            GameManager.Instance.GetUIManager().GetEnemiesKilledUI().SetText(enemyKilled);

            // Display effects
            GameObject particle = GameManager.Instance.GetSpawnPoolManager()
                                    .GetObject(PooledObjID.ENEMY_KILLED_VFX);
            particle.GetComponent<ParticleSystem>().Stop();
            particle.GetComponent<ParticleSystem>().Play();
            particle.transform.position = this.transform.position;

            ChangeState(EnemyState.DEAD);
        }
    }
}
