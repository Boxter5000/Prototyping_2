using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    private float rotZ;
    [SerializeField]private float rotationspeed;
    [SerializeField]private bool clockwiserotation;
  
    void Update()
    {
        if(clockwiserotation == false)
        {
            rotZ += Time.deltaTime * rotationspeed;
        }

        else
        {
            rotZ += -Time.deltaTime * rotationspeed;
        }

        transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }
}
