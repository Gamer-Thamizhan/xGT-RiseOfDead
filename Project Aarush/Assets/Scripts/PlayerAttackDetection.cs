using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackDetection : MonoBehaviour
{

    public PlayerMovement playermov;
    public Flying_Zombie FZscript;
    public Animator animator;

    private void OnTriggerStay2D(Collider2D collision)
    {        
        if (playermov.Attacking() == true && collision.tag == "Enemy")
        {            
            FZscript.hitfz();
        }
    }
   
    
}
