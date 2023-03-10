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


        Invoke("Think", 1); //5초뒤에 Think함수 호출

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //한 방향으로만 알아서 움직이게
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);//왼쪽으로 가니까 -1, y축은 0을 넣으면 큰일남!


        //플랫폼 체크 
        //몬스터는 앞을 체크해야 
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove*0.2f, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        // 시작,방향 색깔

        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("tile"));


        if (rayHit.collider == null)
        {
           
            Debug.Log("경고! 이 앞 낭떨어지다!");
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
        Invoke("Think", nextThinkTime); //5초뒤에 Think함수 호출


    }
    void Turn()
    {
        nextMove  *= (-1);
        spriteRenderer.flipX = nextMove == 1; //nextMove가 1이면 방향바꾸기


        CancelInvoke();
        Invoke("Think", 2);
    }
}
