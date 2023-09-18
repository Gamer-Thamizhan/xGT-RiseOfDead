using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    int ipattacknum;
    int randindex;
    int randval;
    

    int[] attackarray = { 31, 32, 33 };
    int[] attackarraydummy;

    float iphorizontal;
    float jumptimecounter;
    float coyotetime = 0.2f;
    float coyotetimecounter;
    float jumpbuffertime = 0.3f;
    float jumpbuffercounter;
    [SerializeField] float dashingpower = 24f;
    [SerializeField] float dashingtime = 0.2f;
    [SerializeField] float dashingcooldown = 1f;
    [SerializeField] float jumptime;
    [SerializeField] float maxdistance;
    [SerializeField] float speedhorizontal ;
    [SerializeField] float speedsprint ;
    [SerializeField] float speedvertical ;
    [SerializeField] float impulsevertical;

    Vector2 horizontalmov;
    Vector2 verticalmov;
    [SerializeField] Vector3 boxsize;

    bool idle;
    bool hurt;
    bool canDash = true;
    bool onair = false;
    bool isdashing;
    bool ipattack;
    bool ipblock;
    bool iproll;
    bool rolling;
    bool ipvertical;
    bool isjumping;
    bool facingright = true;
    bool A1, A2, A3;
    public bool attacking;

    GameObject player;
    Rigidbody2D playerrb;
    CapsuleCollider2D capscoll;
    CircleCollider2D circlcoll;
    BoxCollider2D boxcoll;
    [SerializeField] LayerMask platformlayermask;
    Animator animator;
    [SerializeField]TrailRenderer tr;
           
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerrb = player.GetComponent<Rigidbody2D>();
        animator = player.GetComponent<Animator>();
        capscoll = GetComponent<CapsuleCollider2D>();
        circlcoll = GetComponent<CircleCollider2D>();
        boxcoll = GetComponent<BoxCollider2D>();
        tr.emitting = false;

        attackarraydummy = attackarray;        

        horizontalmov = Vector2.zero;
        verticalmov = new Vector2(0f, 1f);

        speedhorizontal = 250f;
        speedsprint = 5.0f;
        speedvertical = 3.5f;
        impulsevertical = 10f;        
        jumptime = 0.25f;
        maxdistance = -0.11f;
    }    
    private void FixedUpdate()
    {
        if (attacking == false && ipblock == false && isdashing == false)
        {
            playerrb.velocity = new Vector2(horizontalmov.x * speedhorizontal * Time.deltaTime, playerrb.velocity.y);
        }
        else if (attacking == true && playerrb.velocity.y != 0 && isdashing == false)
        {
            playerrb.velocity = new Vector2(horizontalmov.x * speedhorizontal * Time.deltaTime, playerrb.velocity.y);
        }
        //print(playerrb.velocity.x);
               
        if(playerrb.velocity.x != 0 && IsGrounded() && attacking == false && ipblock == false && rolling == false && hurt == false)
        {
            animator.SetInteger("Anim", 1);
        }
        if (playerrb.velocity.y > 0.1f && !IsGrounded() && attacking == false && hurt == false)
        {
            //print("Jumping");
            animator.SetInteger("Anim", 2);
        }
        if (playerrb.velocity.y < 0 &&  !IsGrounded() && attacking == false && hurt == false  && idle == false)
        {
            // Debug.Log(playerrb.velocity);
            animator.SetInteger("Anim", 6);
        }        
    }
    void Update()
    {        
        
        /*
        if (playerrb.velocity.y > 0)
        {
            Debug.Log(playerrb.velocity);
        }
        */
        
        ipattack = Input.GetMouseButtonDown(0);
        if (ipattack)
        {            
            if(attacking == false && ipblock == false && rolling == false && hurt == false)
            {                
                randindex = Random.Range(0, attackarraydummy.Length);
                randval = attackarray[randindex];
                animator.SetInteger("Anim", randval);
                playerrb.velocity = new Vector2(0f,playerrb.velocity.y);
                attacking = true;
            }
        }

        if(IsGrounded())                //playerrb.velocity.y == 0
        {
            ipblock = Input.GetMouseButton(1);
            if (ipblock && attacking == false && rolling == false && hurt == false)
            {
                animator.SetInteger("Anim", 5);
                playerrb.velocity = new Vector2(0f,0f);
            }
        }

        if(attacking == false && IsGrounded())          //playerrb.velocity.y == 0
        {
            iproll = Input.GetKeyDown(KeyCode.LeftControl);
            if (iproll && hurt == false)
            {
                animator.SetInteger("Anim", 7);
                rolling = true;
                capscoll.enabled = false;
                circlcoll.enabled = true;
            }            
        }

        if (IsGrounded())
        {
            onair = false;
        }
        else
        {
            onair = true;
        }
        
        iphorizontal = Input.GetAxisRaw("Horizontal") ;
        horizontalmov.x = iphorizontal;        
        if(iphorizontal == 0 && IsGrounded() && attacking == false && ipblock == false && rolling == false && hurt == false)
        {
            idle = true;            
            animator.SetInteger("Anim", 0);
        }
        else
        {
            idle = false;
        }
        if(iphorizontal > 0 && facingright == false && attacking == false)
        {
            flip();
            facingright = true;
        }
        else if( iphorizontal < 0 && facingright == true)
        {
            flip();
            facingright = false;
        }
        ipvertical = Input.GetKeyDown("space") || Input.GetKeyDown("w");
       
        if ( onair == true && Input.GetKeyDown(KeyCode.LeftShift) == true)
        {
            
            StartCoroutine(Dash());
        }
        if(IsGrounded())
        {
            coyotetimecounter = coyotetime;           
        }
        else
        {
            coyotetimecounter -= Time.deltaTime;                        
        }
        if (ipvertical)
        {
            jumpbuffercounter = jumpbuffertime;
        }
        else
        {
            jumpbuffercounter -= Time.deltaTime;
        }
        if ( coyotetimecounter> 0f   && jumpbuffercounter >0f)
        {
            isjumping = true;
            rolling = false;
            jumptimecounter = jumptime;
            jumpbuffercounter = 0f;
            playerrb.AddForce(Vector2.up * (impulsevertical+(-playerrb.velocity.y)), ForceMode2D.Impulse);
            capscoll.enabled = true;
            circlcoll.enabled = false;
            if (attacking == false && hurt == false)
            {
                animator.SetInteger("Anim", 2);
            }
        }
        if((Input.GetKey("space") || Input.GetKey("w")) && isjumping == true)
        {            
            if(jumptimecounter > 0)
            {
                playerrb.AddForce(Vector2.up * speedvertical, ForceMode2D.Force);
                if (attacking == false && hurt == false)
                {
                    animator.SetInteger("Anim", 2);
                }
                jumptimecounter -= Time.deltaTime;
            }
            else
            {
                isjumping = false;
            }
        }
        if (Input.GetKeyUp("space") || Input.GetKeyUp("w"))
        {           
            isjumping = false;
            coyotetimecounter = 0f;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position - transform.up * maxdistance, boxsize);
    }

    public void animationfinishedtrigger()
    {
        if(attacking == true)
        {
            attacking = false;
        }
        else if(rolling == true)
        {
            capscoll.enabled = true;
            circlcoll.enabled = false;
            rolling = false;
        }
        else if(hurt == true)
        {
            hurt = false;
        }        
    }
    
    void flip()
    {
        transform.Rotate(0f, 180f, 0f);        
    }

    public bool Attacking()
    {
        A1 = this.animator.GetCurrentAnimatorStateInfo(0).IsName("Hero_Attack1");
        A2 = this.animator.GetCurrentAnimatorStateInfo(0).IsName("Hero_Attack2");
        A3 = this.animator.GetCurrentAnimatorStateInfo(0).IsName("Hero_Attack3");

        if(A1 || A2 || A3)
        {
            attacking = true;
            return true;
        }
        else
        {
            attacking = false;
            return false;
        }
    }

    public void playerhitanim()
    {
        attacking = false;
        capscoll.enabled = true;
        circlcoll.enabled = false;
        rolling = false;
        hurt = true;
        animator.SetInteger("Anim", 4);
    }

    IEnumerator Dash()
    {
        canDash = false;
        isdashing = true;
        float orginalgravity = playerrb.gravityScale;
        playerrb.gravityScale = 0f;
        if(facingright == true)
        {
            playerrb.velocity = new Vector2(dashingpower, 0f);
        }
        else
        {
            playerrb.velocity = new Vector2(-dashingpower, 0f);
        }        
        tr.emitting = true;
        yield return new WaitForSeconds(dashingtime);
        tr.emitting = false;
        playerrb.gravityScale = orginalgravity;
        isdashing = false;
        yield return new WaitForSeconds(dashingcooldown);
        canDash = true;
    }
    private bool IsGrounded()
    {
        if (Physics2D.BoxCast(transform.position,boxsize,0, - transform.up , maxdistance, platformlayermask))
        {            
            return true;
        }
        else
        {
            return false;
        }
    }     
}




 


















