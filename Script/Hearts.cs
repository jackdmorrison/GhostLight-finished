using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Hearts : MonoBehaviour
{
    public SpriteRenderer Heart1;
    public SpriteRenderer Heart2;
    public SpriteRenderer Heart3;
    public Sprite halfHeart;
    public Sprite FullHeart;
    public Sprite Empty;
    
    public void HalfH(int Heart){
        if(Heart==3){
            Heart3.sprite=halfHeart;
        }
        if(Heart==2){
            Heart2.sprite=halfHeart;
        }
        if(Heart==1){
            Heart1.sprite=halfHeart;
        }
    }
    public void EmptyH(int Heart){
        if(Heart==3){
            Heart3.sprite=Empty;
        }
        if(Heart==2){
            Heart2.sprite=Empty;
        }
        if(Heart==1){
            Heart1.sprite=Empty;
        }
    }
    public void FullH(int Heart){
        if(Heart==3){
            Heart3.sprite=FullHeart;
        }
        if(Heart==2){
            Heart2.sprite=FullHeart;
        }
        if(Heart==1){
            Heart1.sprite=FullHeart;
        }
    }
    public void HealthToHearts(int health, int MaxHealth){
        FullH(1);
        FullH(2);
        FullH(3);
        if(health<MaxHealth){
            if(health<(5*MaxHealth/6)){
                HalfH(1);
                if(health<(2*MaxHealth/3)){
                    EmptyH(1);
                    if(health<(3*MaxHealth/6)){
                        HalfH(2);
                        if(health<(MaxHealth/3)){
                            EmptyH(2);
                            if(health<(MaxHealth/6)){
                                HalfH(3);
                            }
                        }
                    }

                }
            }
        }
        
        
    }
}
