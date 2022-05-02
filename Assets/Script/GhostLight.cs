using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostLight : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform followTransform;
    void Start()
    {
        followTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position= new Vector3(followTransform.position.x,followTransform.position.y,this.transform.position.z);
    }
}
