using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    Rigidbody2D rigid;
    public int nextMove;
    SpriteRenderer spriteRenderer;
    Animator animator;

    // Start is called before the first frame update
    void Awake()
    {
        rigid=GetComponent<Rigidbody2D>();
        spriteRenderer=GetComponent<SpriteRenderer>();
        animator=GetComponent<Animator>();


        Invoke("Think", 1); //5�ʵڿ� Think�Լ� ȣ��

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //�� �������θ� �˾Ƽ� �����̰�
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);//�������� ���ϱ� -1, y���� 0�� ������ ū�ϳ�!


        //�÷��� üũ 
        //���ʹ� ���� üũ�ؾ� 
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove*0.2f, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        // ����,���� ����

        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("tile"));


        if (rayHit.collider == null)
        {
           
            Debug.Log("���! �� �� ����������!");
            Turn();
            

        }
    }

    void Think()
    {
        nextMove = Random.Range(-1, 2);

        

        if (nextMove != 0)
        {
            spriteRenderer.flipX = nextMove == 1;
        }

        float nextThinkTime = Random.Range(2f, 5f);
        Invoke("Think", nextThinkTime); //5�ʵڿ� Think�Լ� ȣ��


    }
    void Turn()
    {
        nextMove  *= (-1);
        spriteRenderer.flipX = nextMove == 1; //nextMove�� 1�̸� ����ٲٱ�


        CancelInvoke();
        Invoke("Think", 2);
    }
}
