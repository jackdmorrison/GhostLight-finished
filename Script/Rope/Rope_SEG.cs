using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope_SEG : MonoBehaviour
{
    public GameObject previous;
    public GameObject next;
    public bool isPlayerAttached;
    // Start is called before the first frame update
    //adapted from https://www.youtube.com/watch?v=_6Jq1wliUVQ
    void Start()
    {
        previous = GetComponent<HingeJoint2D>().connectedBody.gameObject;
        Rope_SEG previousSegment = previous.GetComponent<Rope_SEG>();
        if (previousSegment!=null){
            previousSegment.next = gameObject;
            float spriteBottom = previous.GetComponent<SpriteRenderer>().bounds.size.y;
            GetComponent<HingeJoint2D>().connectedAnchor = new Vector2(0,spriteBottom*-1);

        }
        else{
            GetComponent<HingeJoint2D>().connectedAnchor =new Vector2(0,0);
        }
    }
}
