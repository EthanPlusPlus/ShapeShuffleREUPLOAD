﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeMovement : MonoBehaviour
{
    GameManager gm;
    WallManager wm;

    public float xVec, yVec;

    public int lMoves, rMoves;
    public int currentLane, currentWall;


    public Vector3 mousePos;
    public Vector3 mouseStartClick;
    public Vector2 mouseTempPos;

    public bool clicked, leftSwiped, rightSwiped;
    public bool correctLane;
    public bool accel;

    public GameObject mouseTemp;

    Rigidbody shpR;
    Transform shpT;

    void Awake()
    {
        gm = (GameManager)FindObjectOfType(typeof(GameManager));
        wm = (WallManager)FindObjectOfType(typeof(WallManager));

        mouseTemp = GameObject.FindGameObjectWithTag("mouseTemp");

        // if(transform.position.z == 0){
        //     gm.centreShp = gameObject;
        // }
    }

    void Start()
    {

        shpR = GetComponent<Rigidbody>();
        shpT = GetComponent<Transform>();
    }

    void Update()
    {

        StartCoroutine(Shuffle());
        LaneDetect();
    }

    public void Move(Rigidbody r, Transform wallTrans)
    {
        xVec = Mathf.Sin(1.308997f);             //75 * (Mathf.PI/180)
        yVec = Mathf.Cos(1.308997f);             //75 * (Mathf.PI/180)

        if(currentWall == 0){
            r.AddForce(xVec * gm.speed, -yVec * gm.speed, 0);
        }
        else if(wallTrans.position.x - transform.position.x < gm.dist * 0.75f
                && currentWall != 0){
            accel = true;
            r.AddForce(xVec * gm.speed * 0.9f, -yVec * gm.speed * 0.9f, 0);
        
        }else{
            accel = false;
            r.AddForce(-xVec * gm.speed * 3f, yVec * gm.speed * 3f, 0);

        }

        
        
        
        //detect lane

        int openLanes = gm.laneNum - gm.shpCount;
        if(openLanes % 2 == 0){
            lMoves = openLanes / 2;
            rMoves = openLanes / 2;
        }else{
            lMoves = openLanes / 2;
            rMoves = openLanes / 2 + 1; 
        }
    }

    IEnumerator Shuffle()
    {
        //mouse swipe input

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseTemp.transform.position = 10 * new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        mouseTempPos = mouseTemp.transform.position;    

        Vector3 shapePos = transform.position;

        if(Input.GetMouseButtonDown(0)){
            
                mouseStartClick = mouseTempPos;

                yield return new WaitForSeconds(0.1f);

                if(mouseTempPos.x > 0){
                    if(rMoves > 0){       //shapePos.z > -1.75f * ((gm.laneNum -1) / 2
                        rightSwiped = true;
                    }
                }else if(mouseTempPos.x < 0){
                    if(lMoves > 0){       //shapePos.z < 1.75f * ((gm.laneNum -1) / 2)
                        leftSwiped = true;
                    }
                }
        }

        if(leftSwiped){
            shapePos.z = shapePos.z + 2.75f;
            LeanTween.moveZ(gameObject, shapePos.z, 0.1f).setEaseOutBack();
            lMoves -= 1;
            rMoves += 1;
            --currentLane;
            leftSwiped = false;
        }
        
        if(rightSwiped){
            shapePos.z = shapePos.z - 2.75f;
            LeanTween.moveZ(gameObject, shapePos.z, 0.1f).setEaseOutBack();
            lMoves += 1;
            rMoves -= 1;
            ++currentLane;
            rightSwiped = false;
        }

        //key press input

        if(Input.GetKeyDown(KeyCode.LeftArrow)){
            if(lMoves > 0){          //shapePos.z < 1.75f * ((gm.laneNum -1) / 2)
                shapePos.z = shapePos.z + 2.75f;
                LeanTween.moveZ(gameObject, shapePos.z, 0.1f).setEaseOutBack();
                // transform.position = shapePos;
                lMoves -= 1;
                rMoves += 1;
                --currentLane;
            }
        }else if(Input.GetKeyDown(KeyCode.RightArrow)){
            if(rMoves > 0){       //shapePos.z > -1.75f * ((gm.laneNum -1) / 2
                shapePos.z = shapePos.z - 2.75f;
                LeanTween.moveZ(gameObject, shapePos.z, 0.1f).setEaseOutBack();
                //transform.position = shapePos;
                lMoves += 1;
                rMoves -= 1;
                ++currentLane;
            }
        }

    }

    void LaneDetect()
    {
        if(currentWall >= gm.wallNum){
            gm.won = true;
            GetComponent<MeshRenderer>().enabled = false;
            return;
        }
        

        Transform wallTransform = wm.totalWalls[currentWall].transform;

        Move(shpR, wallTransform);
        
        if(transform.position.x > wallTransform.position.x){        //make offset to make delay or quicker reaction (+pos.x)
            if(gm.allShpCorrect){
                wm.WallExit(wallTransform.gameObject);
                ++currentWall;
            }
            else{
                print("dead");
                return;
            }
        }

        Transform singleWall = wallTransform.GetChild((gm.laneNum-1) + currentLane);
        string wallName = singleWall.gameObject.tag;

        if(tag == wallName){
            singleWall.GetChild(0).gameObject.GetComponent<BoxCollider>().enabled = false;
            correctLane = true;
        }else{
            singleWall.GetChild(0).gameObject.GetComponent<BoxCollider>().enabled = true;
            correctLane = false;
        }
    }
    
    void OnCollisionEnter(Collision col)
    {
        if(col != null){
            gm.lost = true;
            shpR.AddForce(new Vector3(-500, Random.Range(300, 500), Random.Range(-700, 700)));
        }
    }

}
