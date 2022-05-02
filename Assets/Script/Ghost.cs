using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Ghost : MonoBehaviour
{
    public Animator Animator;
   
    
    public SpriteRenderer G_Renderer;
    
    public Vector3[] checkpoints;
    
    public Rigidbody2D body;
    public Collider2D collider_G;
    
    public float WalkVelocity=2f;
    public int counter=0;
    
    void Start()
    {
        
        G_Renderer=GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        collider_G = GetComponent<Collider2D>();
        transform.position= checkpoints[counter];
        counter++;
        
    }

    // Update is called once per frame
    void Update()
    {
        //the animator is set to idle 
        Animator.SetBool("IsIdle",true);
        Animator.SetBool("IsBall",false);
        
        if(body.velocity.x>=0f){
            G_Renderer.flipX=false;
        }
        else{
            G_Renderer.flipX=true;
        }  
    }
    
    void OnTriggerExit2D(Collider2D collision){
        if(collision.gameObject.tag=="Player"&& counter<checkpoints.Length){
            transform.position=checkpoints[counter];
            counter++;
        }
    }
    public void ghostEtherial(){
        body.isKinematic=true;
        collider_G.enabled=false;
    }
    public void ghostNormal(){
        body.isKinematic=false;
        collider_G.enabled=true;
    }
    void OnLevelLoad(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Level Loaded");
        Debug.Log(scene.name);
        Debug.Log(mode);
        
        
    }
}
