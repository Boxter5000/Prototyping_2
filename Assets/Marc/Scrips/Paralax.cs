using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Transform player;
    
    private Vector2 startPos;
    private float startZ;

    private Vector2 travel => (Vector2) cam.transform.position - startPos;

    private float distanceFromSubject => transform.position.z - player.position.z;

    private float clippingPlane =>
        (cam.transform.position.z + (distanceFromSubject > 0 ? cam.farClipPlane : cam.nearClipPlane));

    private float parralexFactor => Mathf.Abs(distanceFromSubject) / clippingPlane;

    public void Start()
    {
        startPos = transform.position;
        startZ = transform.position.z;
    }

    private void Update()
    {
        Vector2 newPos = startPos + travel * parralexFactor;
        transform.position = new Vector3(newPos.x, newPos.y, startZ);

    }
}
