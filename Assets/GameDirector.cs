using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    
    GameObject timerText;
    GameObject pointText;
    float time = 0.0f;
    int point = 0;

    public void GetCoin()
    {
        this.point += 100;
    }

    public void meetEnemy()
    {
        this.point -= 50;
    }

    // Start is called before the first frame update
    void Start()
    {
       
        this.timerText = GameObject.Find("Time");
        this.pointText = GameObject.Find("Point");
    }

    // Update is called once per frame
    void Update()
    {
        this.time+=Time.deltaTime;
        this.timerText.GetComponent<Text>().text = this.time.ToString("F1");
        this.pointText.GetComponent<Text>().text = this.point.ToString() + "point";
    }
}
