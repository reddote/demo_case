using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDelay : MonoBehaviour{

    public Transform player;
    public Vector3 defaultDistance;
    public float distanceDump;
    public Vector3 velocity = Vector3.one;

    private void FixedUpdate(){
        SmoothFollowPos();
    }

    private void SmoothFollowPos(){
        Vector3 toPos = player.position + ( defaultDistance);
        Vector3 curPos = Vector3.SmoothDamp(transform.position, toPos, ref velocity, distanceDump);
        transform.position = curPos;
    }

} 