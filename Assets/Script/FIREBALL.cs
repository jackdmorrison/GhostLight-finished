using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FIREBALL : MonoBehaviour
{
     public Rigidbody2D body;
    // Start is called before the first frame update
    void Start()
    {
        body=this.GetComponent<Rigidbody2D>();
    }
    public void Shoot(Vector2 force,float angle){
        body.AddForce(force, ForceMode2D.Impulse);
        transform.Rotate(new Vector3(0,0,angle));
        Destroy(gameObject,2f);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
