using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class OptionsMenu : MonoBehaviour
{
    public GameObject OptionsSCREEN;
    public GameObject RULES;
    public GameObject Menu;
    void Start(){
        DontDestroyOnLoad(gameObject);
    }
    
    public void Exit(){
        SceneManager.LoadScene(0);
        Menu.SetActive(false);
        RULES.SetActive(false);
        OptionsSCREEN.SetActive(false);
        Debug.Log("EXIT");
    }
    public void Options(){
        Debug.Log("OPTIONS");
        Menu.SetActive(false);
        OptionsSCREEN.SetActive(true);
    }
    public void Play(){
        Menu.SetActive(false);
        Debug.Log("PLAY");
    }
    public void LeaveOptions(){
        Debug.Log("LEFT OPTIONS");
        Menu.SetActive(true);
        OptionsSCREEN.SetActive(false);
    }
    public void Rules(){
        Debug.Log("Rules");
        Menu.SetActive(false);
        RULES.SetActive(true);
    }
    public void LeaveRules(){
        RULES.SetActive(false);
        Menu.SetActive(true);
    }
    public void fullScreen(){
        Screen.fullScreen = !Screen.fullScreen;
    }
}
