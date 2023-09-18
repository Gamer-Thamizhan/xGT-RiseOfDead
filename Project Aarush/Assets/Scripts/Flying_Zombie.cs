using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flying_Zombie : MonoBehaviour
{
    float speed = 2.2f; 
    float distx;
    float disty;
    float distance;

    bool facingleft = false;
    bool canattack = true;
    bool attacking = false;
    bool visiblerange = false;
    bool hit = false;
    bool playerposinstance = false;
    bool waiting = false;

    Vector2 playerpos;
    Vector2 playerposfrattack;

    GameObject player;
    public Animator animator;
    PlayerMovement playerscript;
   
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = gameObject.GetComponent<Animator>();
        playerscript = player.GetComponent<PlayerMovement>();
    }
    
    void Update()
    {
        distance = Vector2.Distance(player.transform.position, gameObject.transform.position);
        
        if(distance <= 6f)
        {
            visiblerange = true;
        }
        else
        {
            visiblerange = false; ;
        }
        distx = gameObject.transform.position.x - player.transform.position.x;
        disty = gameObject.transform.position.y - player.transform.position.y;

        if(distx <= 1.8f && disty <= 2.33f && canattack == true)
        {
            attacking = true;
            canattack = false;
            if (hit == false)
            {
                animator.SetInteger("FZ", 1);
            }
            
        }
        if (attacking == true )
        {
            if (playerposinstance == false)
            {
                playerposfrattack = player.transform.position;
                playerposfrattack.y += 1f;
                playerposfrattack.x += 0.3f;
                playerposinstance = true;
            }           
            transform.position = Vector2.MoveTowards(this.transform.position, playerposfrattack, 7f * Time.deltaTime);
        }

        playerpos = player.transform.position;
        playerpos.y += 2.32f;
        if(facingleft == true)
        {
            playerpos.x += 1.7f;
        }
        else if(facingleft == false)
        {
            playerpos.x -= 1.7f;
        }

        
        if (distx > 0 && facingleft == false)
        {
            Flip();
            facingleft = true;               
        }
        else if (distx < 0 && facingleft == true)
        {
            Flip();
            facingleft = false;           
        }
        if (attacking == false && visiblerange == true)
        {
            if(hit == false)
            {
                animator.SetInteger("FZ", 0);
            }                        
            transform.position = Vector2.MoveTowards(this.transform.position, playerpos, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(this.transform.position, this.transform.position, speed * Time.deltaTime);
        }
    }

   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player" && attacking == true)
        {
            print("Player Hit");
            playerscript.playerhitanim();
            
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position,6f);
    }
    void Flip()
    {
        gameObject.transform.Rotate(0f, 180f,0f);
    }   

    IEnumerator FZattackcooldown()
    {
        attacking = false;
        if (canattack == false && playerposinstance == true && waiting == false)
        {
            waiting = true;
            yield return new WaitForSeconds(4);
            waiting = false;
            canattack = true;
            playerposinstance = false;
        }
        
    }

    IEnumerator hitanimover()
    {
        hit = false;
        animator.SetInteger("FZ", 0);
        attacking = false;
        if (canattack == false && playerposinstance == true && waiting == false)
        {
            waiting = true;
            yield return new WaitForSeconds(4);
            waiting = false;
            canattack = true;
            playerposinstance = false;
        }


    }

    public void hitfz()
    {
        hit = true;
        animator.SetInteger("FZ", 3);
        //print("Enterd hitfz");
    }
}
