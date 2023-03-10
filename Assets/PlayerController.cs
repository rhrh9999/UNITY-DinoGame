using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigid2D;
    Animator animator;
    float jumpForce = 500.0f;
    float walkForce = 5.0f;
    float maxWalkSpeed = 2.0f;

    public AudioClip coinSE; //코인소리
    public AudioClip enemySE; //적과 만났을때 소리
    public AudioClip finalSE; //마지막 환호성 소리
    AudioSource aud;

    GameObject director;

    public void delayScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void finalScene()
    {
        SceneManager.LoadScene("DoneScene");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //tag가 
        if (collision.gameObject.tag == "goldCoin")
        {
            this.director.GetComponent<GameDirector>().GetCoin(); //득점관리
            collision.gameObject.SetActive(false); //코인 사라지게함
            Debug.Log("골드코인 획득!");
            this.aud.PlayOneShot(this.coinSE);
        }
        else if (collision.gameObject.tag == "goal")
        {
            Debug.Log("도착!!");
            this.aud.PlayOneShot(this.finalSE); 
            Invoke("finalScene", 5);            
        }
        else if(collision.gameObject.tag == "enemy")
        {
            Debug.Log("적과 조우!!");
            this.director.GetComponent<GameDirector>().meetEnemy(); //득점관리
            this.aud.PlayOneShot(this.enemySE);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        this.rigid2D = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
        this.director = GameObject.Find("GameDirector");
        this.aud = GetComponent<AudioSource>();
    }


    // Update is called once per frame
    void Update()
    {
        //점프
        //연속점프 금지(y축 방향이 속도가 0으로 설정)
        //jump 애니메이션 메카님으로 넣음
        if (Input.GetKeyDown(KeyCode.Space) && this.rigid2D.velocity.y == 0)
        {
            this.animator.SetTrigger("JumpTrigger");
            this.rigid2D.AddForce(transform.up * this.jumpForce);
        }

        //좌우이동
        int key = 0;
        if (Input.GetKey(KeyCode.RightArrow)) key = 3;
        if (Input.GetKey(KeyCode.LeftArrow)) key = -3;

        //플레이어의 속도
        float speedx = Mathf.Abs(this.rigid2D.velocity.x);

        //스피드 제한
        if (speedx < this.maxWalkSpeed)
        {
            this.rigid2D.AddForce(transform.right * key * this.walkForce);
        }

        //움직이는 방향에 따라 반전
        if (key != 0)
        {
            transform.localScale = new Vector3(key, 3, 3);
        }

        //플레이어 속도에 맞춰 애니메이션 속도를 바꾼다
        if (this.rigid2D.velocity.y == 0)
        {
            this.animator.speed = speedx / 2.0f;
        }
        else
        {
            this.animator.speed = 1.0f;
        }

        //플레이어가 화면 밖으로 나감
        //플레이어의 y축 좌표가 -10보다 작아지면, 게임씬을 다시로드하여 재시작
        if (transform.position.y < -10)
        {
            Invoke("delayScene", 5);
        }  
    }
}