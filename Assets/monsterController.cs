using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterController : MonoBehaviour
{
    Rigidbody2D rigid;
    public int nextMove; // �ൿ ��ǥ�� ������ ���� �ϳ� ����


    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //�� �������θ� �˾Ƽ� �����̰�
        rigid.velocity = new Vector2(-1, rigid.velocity.y);//�������� ���ϱ� -1, y���� 0�� ������ ū�ϳ�!
    }

}

