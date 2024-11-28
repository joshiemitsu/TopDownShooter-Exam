using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private void Update()
    {
        Player player = GameManager.Instance.Player;
        if (player)
        {
            Vector3 playerPos = player.transform.position;
            this.transform.position = new Vector3(playerPos.x, this.transform.position.y, playerPos.z);
        }
    }
}
