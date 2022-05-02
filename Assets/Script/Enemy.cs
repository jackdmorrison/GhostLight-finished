using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
public class Enemy : MonoBehaviour
{
    public Rigidbody2D body;
    public float knockBack=30;
    public Player player;
    public int health=100;
    public float jumpthreshold=0f;
    private int direction=0;
    public bool crouch;
    private bool visible;
    public Animator animator;
    public SpriteRenderer E_renderer;
    private int immunity=0;
    public int immunity_frames=10;
    public Collider2D E_Collider;
    public WEAPON2 weapon;
    private int attackCooldown=10;
    public int animationCounter=0;
    private float EnemyPlayerDistance=0f;
    public Sprite[] attackSprites;
    private int strikeTimer=15;
    void Start()
    { 
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        E_renderer = GetComponent<SpriteRenderer>();
        E_Collider=GetComponent<Collider2D>();
        crouch=false;
        visible = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(strikeTimer>0){
            strikeTimer-=1;
        }
        if(attackCooldown>0){
            attackCooldown-=1;
        }
        
        if(player==null){
            try{
                player= GameObject.FindWithTag("Player").GetComponent<Player>();
            }catch(Exception e){
                Debug.Log("NULL PLAYER");
            }
            
        }
        Animate();
        if(immunity>0){
            immunity-=1;
        }
        else{
           movep(); 
        }
        
        
        
    }
    void Animate(){
        
        animator.SetFloat("Horizontal",body.velocity.x);
        if(body.velocity.y<0.1f){
            animator.SetFloat("Vertical",0);
        }
        else{
            animator.SetFloat("Vertical",body.velocity.y);
        }
        
    }
    void OnBecameVisible()
    {
        visible=true;
    }
    void OnBecameInvisible()
    {
        visible=false;
    }
    public void Jump(){
        
        body.AddForce(new Vector2(0,3f), ForceMode2D.Impulse);
       
 }
 public void attackAnim(int frame){
        DoDelayAction(2000f);
        E_renderer.sprite=attackSprites[frame]; 
        weapon.updateAnim(frame);
        if(strikeTimer==0){
            animationCounter+=1;
            strikeTimer=7;
        }

    }
    void OnCollisionStay2D (Collision2D collision)
    {
        if(collision.gameObject.tag == "Tile" && immunity<immunity_frames){
            movep();
        }
    }
    void movep(){
        animator.SetBool("IsIdle",true);
            if(player != null && visible==true){
                EnemyPlayerDistance= player.transform.position.x-gameObject.transform.position.x;
                if(EnemyPlayerDistance>0){
                    direction=1;
                    E_renderer.flipX=false;
                    weapon.flipx=false;
                }else if(EnemyPlayerDistance<0){
                    direction=-1;
                    E_renderer.flipX=true;
                    weapon.flipx=true;
                }
                else{
                    direction=0;
                }
                float PlayerElevation=player.transform.position.y-gameObject.transform.position.y-jumpthreshold;
                if(PlayerElevation>0){   
                    Jump();
                    animator.SetBool("IsIdle",false);
                }
                
            }
            if(direction!=0){
                animator.SetBool("IsIdle",false);
                
            }
            body.velocity=new Vector2(direction,0);
            
    }
    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag=="P_Weapon"&& immunity==0){
            
            damage();
        }
        if(health<=0){
            death();
        }
        
    } 
    void OnTriggerStay2D(Collider2D collider){
        if(collider.gameObject.tag=="Player"&&attackCooldown==0 && animationCounter<6){
            
            
            animator.enabled = false;
            attackAnim(animationCounter);
        }
        else{
            animator.enabled = true;
            animationCounter=0;
            attackCooldown=15;
            weapon.setAttack();
        }
    }
    void death(){
        
        animator.SetBool("Is_Dead",true);
        E_Collider.enabled=false;
        Destroy(gameObject,1f);
    }
    void damage(){
        health=health-player.WEAPONDAMAGE;
        immunity+=immunity_frames;
        animator.SetTrigger("IsHurt");
        
        if(EnemyPlayerDistance>=0f){
            body.AddForce(new Vector2(knockBack*-1,2), ForceMode2D.Impulse);
            Debug.Log("For");
        }else{
            body.AddForce(new Vector2(knockBack,2), ForceMode2D.Impulse);
            Debug.Log("back");
        }
        
    }
    void OnLevelLoad(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Level Loaded");
        Debug.Log(scene.name);
        Debug.Log(mode);
        
        
    }
    void DoDelayAction(float delayTime)
    {
    StartCoroutine(DelayAction(delayTime));
    }
    
    IEnumerator DelayAction(float delayTime)
    {
    //Wait for the specified delay time before continuing.
    yield return new WaitForSeconds(delayTime);
    
    //Do the action after the delay time has finished.
    }
     
 }
