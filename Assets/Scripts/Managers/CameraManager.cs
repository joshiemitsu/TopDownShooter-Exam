using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    void Update()
    {
        Vector3 playerPos = GameManager.Instance.GetPlayer().transform.position;
        this.transform.position = new Vector3(playerPos.x, this.transform.position.y, playerPos.z);
    }
}
