using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    public GameObject OptionsSCREEN;
    public GameObject RULES;
    public GameObject LevelSelect;
    private GameObject Player;
    void Start(){
        GameObject[] players=GameObject.FindGameObjectsWithTag("Player");
        if(players.Length>1)
        {
            for(int i = 0; i < players.Length-1;i++){
                Destroy(players[i]);
                Player=players[i+1];
            };
            
        }else{
            Player=players[0];
        }
        Player.SetActive(false);
    }
    
    public void Exit(){
        Application.Quit();
        Debug.Log("EXIT");
    }
    public void Options(){
        Debug.Log("OPTIONS");
        OptionsSCREEN.SetActive(true);
    }
    public void Play(){
        LevelSelect.SetActive(true);
        //SceneManager.LoadScene(1);
        Debug.Log("PLAY");
    }
    public void playLevel(int level){
        Player.SetActive(true);
        SceneManager.LoadScene(level);
    }
    public void LeaveOptions(){
        Debug.Log("LEFT OPTIONS");
        OptionsSCREEN.SetActive(false);
        RULES.SetActive(false);
        LevelSelect.SetActive(false);
    }
    public void Rules(){
        Debug.Log("Rules");
        RULES.SetActive(true);
    }
    public void LeaveRules(){
        RULES.SetActive(false);
    }
    public void fullScreen(){
        Screen.fullScreen = !Screen.fullScreen;
    }
}
