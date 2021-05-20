using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platformtrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        { 
           GetComponentInParent<OneWayPlatform>().onplatform = true;
        }
    }
    
    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        { 
           GetComponentInParent<OneWayPlatform>().onplatform = false;
        }
    }

}
