using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.Find("Player");
    }

    // Update is called once per frame
    // 플레이어의 x축방향과 y축방향의 변화를 카메라가 따라감
    // z방향은 변화없으므로 카메라의 원래 좌표를 그대로 대입
    void Update()
    {
        Vector3 playerPos = this.player.transform.position;
        transform.position = new Vector3(
            playerPos.x, playerPos.y, transform.position.z);
    }
}
