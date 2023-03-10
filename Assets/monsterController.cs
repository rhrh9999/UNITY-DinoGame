using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterController : MonoBehaviour
{
    Rigidbody2D rigid;
    public int nextMove; // 행동 지표를 결정할 변수 하나 생성


    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //한 방향으로만 알아서 움직이게
        rigid.velocity = new Vector2(-1, rigid.velocity.y);//왼쪽으로 가니까 -1, y축은 0을 넣으면 큰일남!
    }

}

