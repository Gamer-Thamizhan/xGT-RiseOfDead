using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerafollow : MonoBehaviour
{
    public GameObject player;
    Transform post;
    Vector3 pos;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
       
    }

    // Update is called once per frame
    void Update()
    {
        /*
         * ymax = 3.4f
         * ymin = 1.65f
         * xmin = -13.5f
         * xmax = 71.3f
         */
        post = player.GetComponent<Transform>();
        pos = post.position;
        transform.position = new Vector3(Mathf.Clamp(pos.x +5f, -13.5f, 71.3f), Mathf.Clamp(pos.y, 1.65f, 3.4f), pos.z = -10);
        //gameObject.transform.position = pos;
        

    }
}
