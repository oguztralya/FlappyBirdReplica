using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameControlScript : MonoBehaviour
{
    [SerializeField] private GameObject background1;
    [SerializeField] private GameObject background2;
    Rigidbody2D bRigidbody1, bRigidbody2;
    private float size=0;
    [SerializeField] private float backgroundSpeed=-1.5f;
    private GameObject[] barriers;
    [SerializeField] private GameObject barrier;
    private int numberOfBarriers=5;
    private float timeCounter;
    private int totalbarrier=0;
    private bool barrierCanMove=true;


    void Start()
    {
        bRigidbody1=background1.GetComponent<Rigidbody2D>();
        bRigidbody2=background2.GetComponent<Rigidbody2D>();
        bRigidbody1.velocity=new Vector2(backgroundSpeed,0);
        bRigidbody2.velocity=new Vector2(backgroundSpeed,0);


        size=background1.GetComponent<BoxCollider2D>().size.x;
        barriers=new GameObject[numberOfBarriers];
        barrierAni();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        backgroundAni();
        barrierMov();
    }
    
    void backgroundAni () 
    {
        if(background1.transform.position.x<-size)
        {
            background1.transform.position=new Vector2(size,0);
        }
        if(background2.transform.position.x<-size)
        {
            background2.transform.position=new Vector2(size,0);
        }
    }

    void barrierAni () 
    {
        for (int i = 0; i <barriers.Length; i++)
        {
            barriers[i]=Instantiate(barrier, new Vector2(-20,-20),Quaternion.identity);
            Rigidbody2D ba_Rigidbody=barriers[i].AddComponent<Rigidbody2D>();
            ba_Rigidbody.gravityScale=0;
            ba_Rigidbody.velocity=new Vector2(-1.5f,0);
        }
    }

    void barrierMov () 
    {
        timeCounter+=Time.deltaTime;
        if(timeCounter>1.5 && barrierCanMove)
        {
            timeCounter=0;
            float horizontalVec=Random.Range(-0.9f,-3f);
            barriers[totalbarrier].transform.position=new Vector2(14,horizontalVec);
            totalbarrier++;
            if(totalbarrier>=barriers.Length)
            {
                totalbarrier=0;
            }
        }
    }

    public void gameOver () {
        bRigidbody1.velocity=Vector2.zero;
        bRigidbody2.velocity=Vector2.zero;
        barrierCanMove=false;
        for(int i=0; i<barriers.Length; i++) 
        {
            barriers[i].GetComponent<Rigidbody2D>().velocity=Vector2.zero;
        }
        
    }
}
