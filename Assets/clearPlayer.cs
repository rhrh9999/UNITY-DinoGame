using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clearPlayer : MonoBehaviour
{
    public AudioClip bkSE; //코인소리
    AudioSource aud;

    // Start is called before the first frame update
    void Start()
    {
        this.aud.PlayOneShot(this.bkSE);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
