using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    Rigidbody2D body;

    public Vector2 SpawnPos;
    bool fallen = false;
    void Start(){
        body= GetComponent<Rigidbody2D>();
        SpawnPos=new Vector2 (transform.position.x, transform.position.y);
    }
    void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.name.Equals("Player")&& !fallen){
            PlatformManager.Instance.StartCoroutine("SpawnPlatform",SpawnPos);
            Invoke("Fall",0.4f);
            fallen=true;
            Destroy(gameObject,2f);
        }
    }
    void Fall(){
        body.isKinematic = false;
    }
}
