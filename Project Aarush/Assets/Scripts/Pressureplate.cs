using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pressureplate : MonoBehaviour
{
    public JumppadMovementScript script;
  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" || collision.tag == "Ball")
        {
            script.istriggered = true;
        }
        
    }




}
