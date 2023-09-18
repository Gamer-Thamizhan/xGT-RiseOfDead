using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonoBehaviour
{
    Rigidbody2D FZrb;
    GameObject player;

    Vector2 fly;
    void Start()
    {
        FZrb = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        fly = (Vector2.up) ;
    }    
    void Update()
    {
        FZrb.AddForce(-(FZrb.velocity));        
    }
}
