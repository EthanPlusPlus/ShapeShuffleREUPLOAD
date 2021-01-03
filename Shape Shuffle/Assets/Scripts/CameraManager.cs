﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    GameManager gm;

    Rigidbody camR;

    public GameObject zoomGo;

    Vector3 startPos;

    public float zoomSpeed;
    float startTime;
    float startRotX;
    public float rotCovered, currRotLerp; 
    float xVec, yVec;

    bool startRecorded = false;

    void Awake()
    {
        gm = (GameManager)FindObjectOfType(typeof(GameManager));

        camR = GetComponent<Rigidbody>();
    }

    void Start()
    {
        
    }

    void Update()
    {   
        if(gm.lost){
            
            if(!startRecorded){
                startPos = transform.position;
                startRotX = transform.rotation.x;
                startTime = Time.time;
                camR.AddForce(xVec * -gm.speed, yVec * gm.speed, 0);
                zoomGo.transform.SetParent(null);
                startRecorded = true;
            }
            
            ZoomOut(Vector3.Distance(zoomGo.transform.position, startPos));
        }
    }

    public void CamMove()
    {
        

        xVec = Mathf.Sin(1.308997f);             //75 * (Mathf.PI/180)
        yVec = Mathf.Cos(1.308997f);             //75 * (Mathf.PI/180)

        camR.AddForce(xVec * gm.speed, -yVec * gm.speed, 0);
    }

    void ZoomOut(float distance)
    {

        float distCovered = (Time.time - startTime) * zoomSpeed;            //Time.time changes variable
        float currDistLerp = (float)distCovered/distance;
        rotCovered = Time.time - startTime;
        currRotLerp = (float)rotCovered/5;
    
        transform.position = new Vector3(Mathf.SmoothStep(startPos.x, zoomGo.transform.position.x, currDistLerp),Mathf.SmoothStep(startPos.y, zoomGo.transform.position.y, currDistLerp),0);
        transform.rotation = Quaternion.Euler(0, 0, -Mathf.SmoothStep(0, 25, currRotLerp));
    }
}
