using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObjID
{
    public const string FAST_ENEMY_PREFAB = "FastEnemyPrefab";
    public const string SLOW_ENEMY_PREFAB = "SlowEnemyPrefab";

    public const string BULLET_PREFAB = "BulletPrefab";

    // VFX related IDs
    public const string BULLET_HIT_VFX = "BulletHitVfx";
    public const string PLAYER_DAMAGE_VFX = "PlayerDamageVfx";
    public const string PLAYER_KILLED_VFX = "PlayerKilledVfx";
    public const string ENEMY_KILLED_VFX = "EnemyKilledVfx";
}

[System.Serializable]
public struct PooledObjData
{
    public string ID;
    public GameObject Prefab;
    public int Count;
}

public class SpawnPoolManager : MonoBehaviour
{
    [SerializeField]
    private List<PooledObjData> m_objDataList = new List<PooledObjData>();

    [SerializeField]
    private Dictionary<string, List<GameObject>> m_pooledObject = new Dictionary<string, List<GameObject>>();

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        for (int i = 0; i < m_objDataList.Count; i++)
        {
            CreatePool(m_objDataList[i]);
        }
    }

    public GameObject GetObject(string p_ID)
    {
        if (!m_pooledObject.ContainsKey(p_ID))
        {
            return null;
        }

        List<GameObject> objectPool = m_pooledObject[p_ID];

        for (int i = 0; i < objectPool.Count; i++)
        {
            if (!objectPool[i].activeInHierarchy)
            {
                objectPool[i].SetActive(true);
                return objectPool[i];
            }
        }

        // if all object are currently beng used create a new one
        return AddToPool(p_ID);
    }

    private void CreatePool(PooledObjData p_data)
    {
        List<GameObject> objectPool = new List<GameObject>();

        for (int i = 0; i < p_data.Count; i++)
        {
            GameObject obj = Instantiate(p_data.Prefab);
            obj.SetActive(false);
            objectPool.Add(obj);
        }

        m_pooledObject.Add(p_data.ID, objectPool);
    }

    private GameObject AddToPool(string p_ID)
    {
        GameObject prefab = null;
        for (int i = 0; i < m_objDataList.Count; i++)
        {
            if (m_objDataList[i].ID == p_ID)
            {
                prefab = m_objDataList[i].Prefab;
            }
        }

        List<GameObject> objectPool = m_pooledObject[p_ID];

        if (prefab != null)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(true);
            objectPool.Add(obj);

            return obj;
        }
        else
        {
            return null;
        }
    }
}
