using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneSwitch : MonoBehaviour
{
    public int LevelAsInt;
    public string LevelAsStr;
    public bool UseInt=false;
    void OnTriggerEnter2D(Collider2D col){
        GameObject PlayerObj = col.gameObject;
        Debug.Log("Entered");
        if(PlayerObj.name == "Player"){
            if(UseInt){
                SceneManager.LoadScene(LevelAsInt);
            }
            else{
                SceneManager.LoadScene(LevelAsStr);
            }
        }
        
    }
}
