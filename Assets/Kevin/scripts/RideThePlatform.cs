using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RideThePlatform : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name.Equals ("Platform"))
        {
            this.transform.parent = col.transform;
        }
    }
    
    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.name.Equals ("Platform"))
        {
            this.transform.parent = null;
        }
    }
}
