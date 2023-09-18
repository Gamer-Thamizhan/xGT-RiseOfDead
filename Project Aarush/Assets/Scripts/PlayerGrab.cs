using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrab : MonoBehaviour
{
    Animator animator;
    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("collided");
        if (collision.gameObject.tag == "Chain")
        {
            print("Chain Detected");
            if (Input.GetKeyDown("g"))
            {
                print("Grabbed");
                animator.SetInteger("Anim", 8);
            }
        }
    }
    
}
