using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public Rigidbody2D hook;
    public GameObject[] prefabRopeSegs;
    public int numLinks = 3;
    void Start()
    {
        GenerateRope();
    }

    
    void GenerateRope()
    {
        Rigidbody2D prevBod = hook;
        for(int i = 0; i < numLinks; i++)
        {
            int index = Random.Range(0, prefabRopeSegs.Length);
            GameObject newseg = Instantiate(prefabRopeSegs[index]);
            newseg.transform.parent = transform;
            newseg.transform.position = transform.position;
            HingeJoint2D hj = newseg.GetComponent<HingeJoint2D>();
            hj.connectedBody = prevBod;
            prevBod = newseg.GetComponent<Rigidbody2D>();
        }
    }
}
