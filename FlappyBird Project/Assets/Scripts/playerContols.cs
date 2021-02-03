using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerContols : MonoBehaviour
{
    [SerializeField] private Sprite[] bird;
    private SpriteRenderer spriteRenderer;
    private int birdWings=0;
    private bool wingsCanMove=true;
    [SerializeField] private float wingsCD=0.05f;
    private float timeCounter;
    Rigidbody2D r_Rigidbody;
    [SerializeField] private float jumpForce;
    private bool birdCanMove=true;
    private GameObject gameControl;
    private int point=0;
    [SerializeField] private Text pointText;
    private AudioSource[] audio;
    private int bestRecord=0;

    void Start()
    {
        spriteRenderer=GetComponent<SpriteRenderer>();
        r_Rigidbody=GetComponent<Rigidbody2D>();
        gameControl=GameObject.FindGameObjectWithTag("gameControlTag");
        audio=GetComponents<AudioSource>();
        PlayerPrefs.GetInt("bestrecord");
    }

    void FixedUpdate() 
    {
        birdWingsAni();
    }
    void Update()
    {
        
        birdMovement();
    }

    void birdWingsAni() 
    {
        timeCounter+=Time.deltaTime;
        if(timeCounter>wingsCD)
        {
            timeCounter=0;
            if(wingsCanMove)
            {
                spriteRenderer.sprite=bird[birdWings];
                birdWings++;
                if(birdWings>=bird.Length)
                {
                    birdWings--;
                    wingsCanMove=false;
                    
                }
            }
            else 
            {
                birdWings--;
                spriteRenderer.sprite=bird[birdWings];
                if(birdWings==0)
                {
                    birdWings++;
                    wingsCanMove=true;
                }
            }
        }
    }
    void birdMovement() 
    {
        if(Input.GetMouseButtonDown(0) && birdCanMove)
        {
            r_Rigidbody.velocity=new Vector2(0,jumpForce);
            audio[0].Play();

        }
        if(r_Rigidbody.velocity.y>0)
        {
            transform.eulerAngles=new Vector3(0,0,45);
        }
        if(r_Rigidbody.velocity.y<0)
        {
            transform.eulerAngles=new Vector3(0,0,-45);
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag=="barrierTag")
        {
            birdCanMove=false;
            gameControl.GetComponent<gameControlScript>().gameOver();
            Invoke("playAgain",2);
            audio[2].Play();
            GetComponent<CapsuleCollider2D>().enabled=false;
        }

        if(other.gameObject.tag=="pointCol")
        {
            point++;
            pointText.text="Point: "+point;
            audio[1].Play();
            PlayerPrefs.SetInt("point",point);
            if(point>bestRecord)
            {
                bestRecord=point;
                PlayerPrefs.SetInt("bestrecord",bestRecord);
            }
            
        }
    }

    private void playAgain() {
        SceneManager.LoadScene("Menu");
    }
   
}
