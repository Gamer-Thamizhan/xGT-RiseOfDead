using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaugeRed : MonoBehaviour
{
    Quaternion gaugeRed;
    bool reachleft = false, reachright = true;

    void Update()
    {
        gaugeRed = transform.rotation;        
        //gaugeRed = transform.rotation;       
        
        if (reachleft)
        {
            gaugeRed.z += 1f * Time.deltaTime;
        }
        else if (reachright)
        {
            gaugeRed.z -= 1f * Time.deltaTime;
        }
        if(gaugeRed.z >= 0.84f && reachleft == true)
        {
            reachleft = false;
            reachright = true;
        }
        if(gaugeRed.z <= -0.86f && reachright == true)
        {
            reachleft = true;
            reachright = false;
        }
        transform.rotation = gaugeRed;
        
    }
        
}
