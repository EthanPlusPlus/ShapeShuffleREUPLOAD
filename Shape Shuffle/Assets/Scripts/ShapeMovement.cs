using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeMovement : MonoBehaviour
{
    GameManager gm;
    WallManager wm;

    public float xVec, yVec;

    public int lMoves, rMoves;
    public int currentLane;


    public Vector3 mousePos;
    public Vector3 mouseStartClick;
    public Vector2 mouseTempPos;

    public bool clicked, leftSwiped, rightSwiped;

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

        Move(shpR);
    }

    void Update()
    {
        StartCoroutine(Shuffle());
    }

    public void Move(Rigidbody r)
    {
        xVec = Mathf.Sin(1.308997f);             //75 * (Mathf.PI/180)
        yVec = Mathf.Cos(1.308997f);             //75 * (Mathf.PI/180)

        r.AddForce(xVec * gm.speed, -yVec * gm.speed, 0);

        
        
        
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
            transform.position = shapePos;
            lMoves -= 1;
            rMoves += 1;
            leftSwiped = false;
        }
        
        if(rightSwiped){
            shapePos.z = shapePos.z - 2.75f;
            transform.position = shapePos;
            lMoves += 1;
            rMoves -= 1;
            rightSwiped = false;
        }

        //key press input

        if(Input.GetKeyDown(KeyCode.LeftArrow)){
            if(lMoves > 0){          //shapePos.z < 1.75f * ((gm.laneNum -1) / 2)
                shapePos.z = shapePos.z + 2.75f;
                transform.position = shapePos;
                lMoves -= 1;
                rMoves += 1;
            }
        }else if(Input.GetKeyDown(KeyCode.RightArrow)){
            if(rMoves > 0){       //shapePos.z > -1.75f * ((gm.laneNum -1) / 2
                shapePos.z = shapePos.z - 2.75f;
                transform.position = shapePos;
                lMoves += 1;
                rMoves -= 1;
            }
        }

    }

    void LaneDetect()
    {
        for (int i = gm.shpCount-1; i < gm.laneNum; i++)
        {
            //wm.totalWalls[0].transform.GetChild(i).gameObject.tag    
        }
    }
}
