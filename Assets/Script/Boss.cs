using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Boss : MonoBehaviour
{
    public Rigidbody2D body;
    public Player player;
    public int health=300;
    public float speed=1;
    private bool visible;
    public Animator animator;
    private int immunity=0;
    public int immunity_frames=10;
    public Collider2D B_Collider;
    public int pause=20;
    public int limit=10;
    public int pausetimer=0;
    public int timer=0;
    public float PSPEED=1;
    public bool movingLeft=false;
    public bool movingRight=false;
    public GameObject Door;
    private Vector2 target;
    public static Boss Instance = null;
    [SerializeField] GameObject Fireball_Prefab;
    void Awake(){
        if(Instance==null){
            Instance=this;
        }
        else if(Instance!=this){
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        B_Collider=GetComponent<Collider2D>();
        player=GameObject.FindWithTag("Player").GetComponent<Player>();
        target=player.GivePosition();
    }

    // Update is called once per frame
    void Update()
    {
        if(immunity>0){
            immunity-=1;
        }
        if(movingRight){
                
                animator.SetBool("Right",true);
                animator.SetBool("Left",false);
                body.velocity=new Vector2(speed,0);
                timer+=1;
            }
        else if(movingLeft){
                animator.SetBool("Right",false);
                animator.SetBool("Left",true);
                timer-=1;
                
                body.velocity=new Vector2(-speed,0);
        }
        if(timer==-limit&&!movingRight){
            
            movingLeft=false;
            animator.SetBool("Left",false);
            animator.SetBool("Right",false);
            body.velocity=new Vector2(0,0);
            if(pausetimer<pause){
                pausetimer+=1;
            }
            else {
                attack();
                pausetimer=0;
                movingRight=true;
            }
        }
        else if(timer==limit && !movingLeft){
            movingRight=false;
            animator.SetBool("Left",false);
            animator.SetBool("Right",false);
            body.velocity=new Vector2(0,0);
            if(pausetimer<pause){
                pausetimer+=1;
            }
            else{
                attack();
                pausetimer=0;
                movingLeft=true;
            }
            
        }
        else if(timer==0){
            animator.SetBool("Left",false);
            animator.SetBool("Right",false);
            body.velocity=new Vector2(0,0);
            if(pausetimer<pause){
                pausetimer+=1;
            }
            else if(movingLeft==true){
                attack();
                pausetimer=0;
                timer-=1;
                movingLeft=false;
                movingRight=true;
            }
            else if(movingRight==true){
                attack();
                pausetimer=0;
                timer+=1;
                movingLeft=true;
                movingRight=false;
            }
        }
        
    }
    void damage(){
        health=health-player.WEAPONDAMAGE;
        immunity+=immunity_frames;
        animator.SetTrigger("IsHurt");
        
    }
    void attack(){
        //attack animation
        animator.SetTrigger("attack");
        Vector2 position= new Vector2(transform.position.x, transform.position.y);
        
        //find player position
        target=player.GivePosition();
        float angle=Vector2.Angle(position,target);
        //create prefab projectile aim it at player position
        Instantiate(Fireball_Prefab,new Vector3(transform.position.x,transform.position.y,transform.position.z),Fireball_Prefab.transform.rotation);
        GameObject.FindWithTag("fire").GetComponent<FIREBALL>().Shoot(new Vector2((position.x+target.x)*PSPEED,(position.y+target.y)*PSPEED),-angle-90);
        try{
            GameObject.FindWithTag("fire").GetComponent<FIREBALL>().Shoot(new Vector2((position.x+target.x)*PSPEED,(position.y+target.y)*PSPEED),-angle-90);
        }catch(Exception e){
            Debug.Log(e.Message);
        }
            
        
    }
     void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag=="P_Weapon"&& immunity==0){
            damage();
        }
        if(health<=0){
            death();
        }
    } 
    void death(){
        
        animator.SetBool("Is_Dead",true);
        B_Collider.enabled=false;
        Destroy(gameObject);
        Door.SetActive(true);
    }
}
