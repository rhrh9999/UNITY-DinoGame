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

    public AudioClip coinSE; //���μҸ�
    public AudioClip enemySE; //���� �������� �Ҹ�
    public AudioClip finalSE; //������ ȯȣ�� �Ҹ�
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
        //tag�� 
        if (collision.gameObject.tag == "goldCoin")
        {
            this.director.GetComponent<GameDirector>().GetCoin(); //��������
            collision.gameObject.SetActive(false); //���� ���������
            Debug.Log("������� ȹ��!");
            this.aud.PlayOneShot(this.coinSE);
        }
        else if (collision.gameObject.tag == "goal")
        {
            Debug.Log("����!!");
            this.aud.PlayOneShot(this.finalSE); 
            Invoke("finalScene", 5);            
        }
        else if(collision.gameObject.tag == "enemy")
        {
            Debug.Log("���� ����!!");
            this.director.GetComponent<GameDirector>().meetEnemy(); //��������
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
        //����
        //�������� ����(y�� ������ �ӵ��� 0���� ����)
        //jump �ִϸ��̼� ��ī������ ����
        if (Input.GetKeyDown(KeyCode.Space) && this.rigid2D.velocity.y == 0)
        {
            this.animator.SetTrigger("JumpTrigger");
            this.rigid2D.AddForce(transform.up * this.jumpForce);
        }

        //�¿��̵�
        int key = 0;
        if (Input.GetKey(KeyCode.RightArrow)) key = 3;
        if (Input.GetKey(KeyCode.LeftArrow)) key = -3;

        //�÷��̾��� �ӵ�
        float speedx = Mathf.Abs(this.rigid2D.velocity.x);

        //���ǵ� ����
        if (speedx < this.maxWalkSpeed)
        {
            this.rigid2D.AddForce(transform.right * key * this.walkForce);
        }

        //�����̴� ���⿡ ���� ����
        if (key != 0)
        {
            transform.localScale = new Vector3(key, 3, 3);
        }

        //�÷��̾� �ӵ��� ���� �ִϸ��̼� �ӵ��� �ٲ۴�
        if (this.rigid2D.velocity.y == 0)
        {
            this.animator.speed = speedx / 2.0f;
        }
        else
        {
            this.animator.speed = 1.0f;
        }

        //�÷��̾ ȭ�� ������ ����
        //�÷��̾��� y�� ��ǥ�� -10���� �۾�����, ���Ӿ��� �ٽ÷ε��Ͽ� �����
        if (transform.position.y < -10)
        {
            Invoke("delayScene", 5);
        }  
    }
}