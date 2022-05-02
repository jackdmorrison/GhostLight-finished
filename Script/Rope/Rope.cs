using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public Rigidbody2D hook;
    public GameObject[] RopeSegments;
    public int numLink = 8;
    // Start is called before the first frame update
    void Start()
    {
        GenerateRope();
    }
    //adapted from: https://www.youtube.com/watch?v=yQiR2-0sbNw
    void GenerateRope(){
        Rigidbody2D previous = hook;
        for(int i = 0; i < numLink; i++){
            GameObject newSegment = Instantiate(RopeSegments[i]);
            newSegment.transform.parent = transform;
            newSegment.transform.position=transform.position;
            HingeJoint2D hinge = newSegment.GetComponent<HingeJoint2D>();
            hinge.connectedBody= previous;
            previous = newSegment.GetComponent<Rigidbody2D>();
        }
    }
}
