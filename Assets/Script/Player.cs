using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
   public Rigidbody2D body;
   public Sprite[] attackSprites;
    public HingeJoint2D hinge;
    public float horizontal;
    public Collider2D Pcollider;
    public int MaxHealth=120;
    private int Health = 0;
    private int immunity=0;
    public int immunity_frames=50;
    public float speed = 3.0f;
    public float jumpForce = 120f;
    public float attachForce=15f;
    private bool attached = false;
    public bool crouch = false;
    public WEAPON2 weapon;
    public Transform attachedTo;
    public OptionsMenu OPTIONS;
    private GameObject disregard;
    public Hearts hearts;
    Animator animator;
    public int WEAPONDAMAGE=0;
    
    public SpriteRenderer P_renderer;
    public int animationCounter=0;
    public int attackWait=10;
    private int attackTimer=0;
    private int strikeTimer=0;
    void OnEnable()
    {

        SceneManager.sceneLoaded += OnLevelLoad;
    }
            
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelLoad;
    }
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        body = GetComponent<Rigidbody2D>();
        hinge = GetComponent<HingeJoint2D>();
        Pcollider=GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        P_renderer = GetComponent<SpriteRenderer>();
        hearts=GameObject.FindWithTag("hearts").GetComponent<Hearts>();
        WEAPONDAMAGE = weapon.damage;
        Health = MaxHealth;
        }
    void Update(){
        hearts.HealthToHearts(Health,MaxHealth);
        if(attackTimer>0){
            attackTimer-=1;
        }
        if(strikeTimer>0){
            strikeTimer-=1;
        }
        if(immunity>0){
            immunity-=1;
        }
        if(!crouch){
            horizontal=Input.GetAxisRaw("Horizontal");
        }
        
        animator.SetBool("Is_Idle",true);
        getInputs();
        
        
        if(!attached){
            transform.position += new Vector3(horizontal, 0,0)*Time.deltaTime*speed;
        }
        
        if(horizontal!=0){
            animator.SetBool("Is_Idle",false);
            if(horizontal>0){
                if(attached){
                    body.AddRelativeForce(new Vector3(1,0,0) * attachForce);
                }
                P_renderer.flipX=false;
                weapon.flipx=false;
            }
            else if(horizontal<0){
                if(attached){
                    body.AddRelativeForce(new Vector3(-1,0,0) * attachForce);
                }
                weapon.flipx=true;
                P_renderer.flipX=true;
                
            }
            
            Animate();
            
        }
        
        if(Mathf.Abs(body.velocity.y)!=0f){
            animator.SetBool("Is_Idle",false);
            Animate();
        }
    }
    void getInputs(){
        if(Input.GetKey(KeyCode.LeftControl)){
            if(Health<MaxHealth){
                Health+=1;
            }
            crouch = true;
            animator.SetBool("Is_Crouch",true);
        }
        else{
            crouch=false;
            animator.SetBool("Is_Crouch",false);
        }
        if(Input.GetKey(KeyCode.E)&&animationCounter<6 &&attackTimer==0){
            animator.enabled = false;
            attackAnim(animationCounter);
        }
        else if(Input.GetKeyUp(KeyCode.E)){
            animationCounter=0;
        }
        else{
            if(animationCounter==6){
                attackTimer=attackWait;
            }
            animator.enabled = true;
            
            weapon.setAttack();
        }
        if(Input.GetKey(KeyCode.Escape)){
             OPTIONS.Menu.SetActive(true);
         }
        if(Input.GetButtonDown("Jump")){
            
            if(attached){
                hinge.connectedBody.gameObject.GetComponent<Rope_SEG>().isPlayerAttached=false;
                attached=false;
                hinge.enabled=false;
                Pcollider.enabled=true;
                hinge.connectedBody=null;
            }
            else{
                attachedTo=null;
            }
            if(Mathf.Abs(body.velocity.y)<0.001f && !crouch){
                body.AddForce(new Vector2(0,jumpForce), ForceMode2D.Impulse);
            }
        }
    }
    void Animate(){
        animator.SetFloat("Horizontal",body.velocity.x);
        animator.SetFloat("Vertical",body.velocity.y);
    }
    public void Attach(Rigidbody2D ropeSegment){
        ropeSegment.gameObject.GetComponent<Rope_SEG>().isPlayerAttached = true;
        hinge.connectedBody=ropeSegment;
        hinge.enabled = true;
        attached = true;
        attachedTo = ropeSegment.gameObject.transform.parent;
        Pcollider.enabled = false;
        GetComponent<Transform>().position=ropeSegment.gameObject.transform.position;

    }
    void OnTriggerEnter2D(Collider2D col){
        //Debug.Log(col.gameObject.tag);
        if(!attached){
            if(col.gameObject.tag == "Rope"){
                if(attachedTo != col.gameObject.transform.parent){
                    if(disregard==null || col.gameObject.transform.parent.gameObject!= disregard){
                        Attach(col.gameObject.GetComponent<Rigidbody2D>());
                    }
                }
            }
        }
        if(col.gameObject.tag=="EnemyWeapon"&& immunity==0){
            Debug.Log("here");
            damage(col.gameObject.GetComponent<WEAPON2>().getDamage());
        }
        if(Health<=0){
            death();
        }
    }
    
    void OnTriggerExit2D(Collider2D col){
        if(col.gameObject.tag=="EnemyWeapon"){
            animator.SetBool("Hurt",false);
        }
    }
    
    void OnLevelLoad(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Level Loaded");
        Debug.Log(scene.name);
        Debug.Log(mode);
        transform.position = GameObject.FindWithTag("Start").transform.position;
        
    }
    void death(){
        
        animator.SetBool("Dead",true);
        Health=120;
        transform.position = GameObject.FindWithTag("Start").transform.position;
        
    }
    void damage(int damage){
        Health=Health-damage;
        immunity+=immunity_frames;
        animator.SetBool("Hurt",true);
        
    }
    public Vector2 GivePosition(){
        return new Vector2(transform.position.x, transform.position.y);
    }
    public void attackAnim(int frame){
        DoDelayAction(2000f);
        P_renderer.sprite=attackSprites[frame]; 
        weapon.updateAnim(frame);
        if(strikeTimer==0){
            animationCounter+=1;
            strikeTimer=5;
        }
        

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
