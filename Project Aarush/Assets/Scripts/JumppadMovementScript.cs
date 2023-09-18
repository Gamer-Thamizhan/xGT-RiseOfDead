using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumppadMovementScript : MonoBehaviour
{
    Vector3 startpos;
    Vector3 endpos;
    Vector3 ballpos;    

    float padspeed = 2.5f;

    bool reachtop = true;
    bool reachbot = false;
    public bool istriggered = false;
    
    GameObject ball;

    private void Start()
    {       
        startpos =transform.position;
        endpos = startpos;
        endpos.y += 4f;        
        ball = GameObject.FindGameObjectWithTag("Ball");
        ballpos = ball.transform.position; 

    }
    
    private void Update()
    {
        if (istriggered)
        {
            if (reachtop)
            {
                transform.position = Vector3.MoveTowards(transform.position, endpos, padspeed * Time.deltaTime);

            }
            else if (reachbot)
            {
                transform.position = Vector3.MoveTowards(transform.position, startpos, padspeed * Time.deltaTime);
            }

            if (transform.position.y >=  endpos.y && reachtop == true)
            {
                reachbot = true;
                reachtop = false;
            }
            else if (transform.position.y <= startpos.y && reachbot == true)
            {
                reachbot = false;
                reachtop = true;
                istriggered = false;
                ball.transform.position = ballpos;
            }
        }       
    }
    
    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if (collision.tag == "Player" || collision.tag == "Ball")
        {
            //istriggered = true;
            print("Entered");
            jumppad.transform.position = Vector3.MoveTowards(startpos, endpos, padspeed * Time.deltaTime);
            //StartCoroutine(waittime());
        }            
    }*/
    /*
    IEnumerator waittime()
    {
        jumppad.transform.position = Vector3.MoveTowards(startpos,endpos,padspeed*Time.deltaTime);
        yield return new WaitForSeconds(3);
        jumppad.transform.position = Vector3.MoveTowards(endpos, startpos, padspeed * Time.deltaTime);
        istriggered = false;
        
    }*/





}
