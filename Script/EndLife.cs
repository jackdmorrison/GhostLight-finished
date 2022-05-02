using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLife : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col){
        GameObject obj = col.gameObject;
        if (obj.tag=="Player"){
            obj.transform.position = GameObject.FindWithTag("Start").transform.position;
        }
        if(obj.tag == "enemy"){
            Destroy(obj);
        }
        
    }
}
