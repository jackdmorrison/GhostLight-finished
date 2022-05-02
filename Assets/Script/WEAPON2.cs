using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WEAPON2 : MonoBehaviour
{
    //float will change the side the weapon is on whether it is 1 or -1 
    private float f=1f;
    public int damage = 60;
    
    //this is the renderer of the weapon
    public SpriteRenderer W_Renderer;
    public Sprite idleSprite;
    public Sprite attackSprite;
    private Vector3[] pos;
    public Vector3[] rotations;
    //this is the differential of the players position to the weapons position it changes relative to the players animation 
    private Vector2 differential;
    
    //whether or not to flip the side the weapon is on
    public bool flipx =false;
    //name of the sprite that the players render is displaying each frame
    private Sprite currSprite;
    //dictionary used to determine the differential positon relative to the players sprite
    IDictionary<string,Vector2> allignment= new Dictionary<string,Vector2>();
    private bool attack=false;
    //transform of the player that the weapon is attached to 
    public Transform followTransform;
    // Start is called before the first frame update
    void Start() 
    {
        DontDestroyOnLoad(gameObject);
        W_Renderer=GetComponent<SpriteRenderer>();
        transform.parent=followTransform;
        differential = new Vector2(-0.1f,-0.15f);
        pos=new Vector3[]{new Vector3(-0.2f,0.55f,0f),new Vector3(0.04f,0.55f,0f),new Vector3(0.13f,0.55f,0f),new Vector3(0.3f,0.45f,0f),new Vector3(0.4f,0.1f,0f),new Vector3(0.4f,0f,0f)};
        currSprite=idleSprite;
        W_Renderer.sprite=currSprite;
        

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(flipx){
            W_Renderer.flipX=true;
            f=-1f;
        }
        else{
            W_Renderer.flipX=false;
            f=1f;
        }   
        W_Renderer.sprite=currSprite;
        if(!attack){
            transform.rotation = Quaternion.Euler(0,0,0);
            this.transform.position= new Vector3( followTransform.position.x + ( differential.x*f), followTransform.position.y + differential.y , 0f );
        }
        else{
            currSprite=attackSprite;
        }
        
    }
    public int getDamage(){
        return damage;
    }
    
    public void updateAnim(int frame){
            updateSprite();
            currSprite=attackSprite;
            Vector3 position=pos[frame];
            position.x=position.x*f;
            transform.localPosition=position;
            transform.rotation = Quaternion.Euler(0,0,rotations[frame].z*f);
            
            attack=true;
        
    }public void updateSprite(){
        currSprite=attackSprite;
        W_Renderer.sprite=currSprite;
    }
    public void setAttack(){
        attack=false;
        currSprite=idleSprite;
    }
}
