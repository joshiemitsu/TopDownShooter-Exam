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
    [SerializeField] private EnemyState m_currentState;
    [SerializeField] private Player m_targetPlayer;

    [SerializeField] private float m_health = 0;
    [SerializeField] private float m_attackCooldown = 0;
    [SerializeField] private float m_attackDamage = 0;
    [SerializeField] private float m_attackRange = 0;
    [SerializeField] private float m_movementSpeed = 0;

    private float m_attackTimer = 0;

    private void Awake()
    {
        ChangeState(EnemyState.INIT);
    }

    private void Update()
    {
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
        if(m_targetPlayer == null)
        {
            m_targetPlayer = GameManager.Instance.GetPlayer();
        }
        ChangeState(EnemyState.MOVE);
    }

    private void MoveToTarget()
    {
        // If target is within range proceed to attack again.
        if(IsWithinAttackRange())
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
                    Debug.Log("Attack Player");
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
            GameManager.Instance.GetScoreManager().EnemyKilled++;
            int enemyKilled = GameManager.Instance.GetScoreManager().EnemyKilled;
            GameManager.Instance.GetUIManager().GetEnemiesKilledUI().SetText(enemyKilled);
            ChangeState(EnemyState.DEAD);
        }
    }
}
