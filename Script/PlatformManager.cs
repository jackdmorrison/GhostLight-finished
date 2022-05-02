using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public static PlatformManager Instance = null;
    [SerializeField]
    GameObject platformPrefab;
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
        
        GameObject[] platforms=GameObject.FindGameObjectsWithTag("FP");
        foreach (GameObject platform in platforms){
            Instantiate(platformPrefab,new Vector3(platform.transform.position.x,platform.transform.position.y,platform.transform.position.z),platformPrefab.transform.rotation);
            Destroy(platform);
        }
    }

    IEnumerator SpawnPlatform(Vector2 spawnPos){
        yield return new WaitForSeconds(2f);
        Instantiate(platformPrefab,spawnPos,platformPrefab.transform.rotation);
    }
}
