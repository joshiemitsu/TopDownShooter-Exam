using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemySpawnManager : MonoBehaviour
{
    private const float SPAWN_PERCENTAGE = 50;

    [SerializeField] private Player m_player;

    [SerializeField] private float m_spawnRate = 0;

    [SerializeField] private float m_playerRange = 0;

    [SerializeField] private Vector2 m_minMaxWidth;
    [SerializeField] private Vector2 m_minMaxHeight;

    private float m_spawnTimer = 0;

    void Start()
    {
        m_player = GameManager.Instance.GetPlayer();
    }

    public void Update()
    {
        m_spawnTimer += Time.deltaTime;

        if(m_spawnTimer > m_spawnRate)
        {
            m_spawnTimer = 0;
            Vector3 randomPos = GetRandomPosition();

            Enemy enemy = GetRandomEnemyType();
            enemy.transform.position = randomPos;
        }
    }

    private Enemy GetRandomEnemyType()
    {
        float randomVal = UnityEngine.Random.Range(0, 100);

        if(randomVal > SPAWN_PERCENTAGE)
        {
            return GameManager.Instance.GetSpawnPoolManager().GetObject(PooledObjID.FAST_ENEMY_PREFAB).GetComponent<Enemy>();
        }
        else
        {
            return GameManager.Instance.GetSpawnPoolManager().GetObject(PooledObjID.SLOW_ENEMY_PREFAB).GetComponent<Enemy>();
        }
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 randomPos = new Vector3(UnityEngine.Random.Range(m_minMaxWidth.x, m_minMaxWidth.y), 
                                        this.transform.position.y,
                                        UnityEngine.Random.Range(m_minMaxHeight.x, m_minMaxHeight.y));

        Vector3 playerPosition = m_player.transform.position;

        // Continue creating generating a position that is outside the player's range.
        while (Vector3.Distance(playerPosition, randomPos) < m_playerRange)
        {
            // This is to prevent infinite loop if the developer failed to set a player range
            if(m_playerRange <= 0)
            {
                break;
            }

            randomPos = new Vector3(UnityEngine.Random.Range(m_minMaxWidth.x, m_minMaxWidth.y),
                                    this.transform.position.y,
                                    UnityEngine.Random.Range(m_minMaxHeight.x, m_minMaxHeight.y));
        }

        return randomPos;
    }
}
