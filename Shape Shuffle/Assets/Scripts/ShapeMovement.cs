using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeMovement : MonoBehaviour
{
    GameManager gm;

    public float xVec, yVec;

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

        mouseTemp = GameObject.FindGameObjectWithTag("mouseTemp");
    }

    void Start()
    { 
       shpR = GetComponent<Rigidbody>();
       shpT = GetComponent<Transform>();

       Move();
    }

    void Update()
    {
        StartCoroutine(Shuffle());
    }

    void Move()
    {
        xVec = Mathf.Sin(1.308997f);             //75 * (Mathf.PI/180)
        yVec = Mathf.Cos(1.308997f);             //75 * (Mathf.PI/180)

        shpR.AddForce(xVec * gm.speed, -yVec * gm.speed, 0);
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
                    if(shapePos.z > -1.75f * ((gm.laneNum -1) / 2)){
                        rightSwiped = true;
                    }
                }else if(mouseTempPos.x < 0){
                    if(shapePos.z < 1.75f * ((gm.laneNum -1) / 2)){
                        leftSwiped = true;
                    }
                }
        }

        if(leftSwiped){
            shapePos.z = shapePos.z + 2.75f;
            transform.position = shapePos;
            leftSwiped = false;
        }
        
        if(rightSwiped){
            shapePos.z = shapePos.z - 2.75f;
            transform.position = shapePos;
            rightSwiped = false;
        }

        //key press input

        if(Input.GetKeyDown(KeyCode.LeftArrow)){
            if(shapePos.z < 1.75f * ((gm.laneNum -1) / 2)){
                shapePos.z = shapePos.z + 2.75f;
                transform.position = shapePos;
            }
        }else if(Input.GetKeyDown(KeyCode.RightArrow)){
            if(shapePos.z > -1.75f * ((gm.laneNum -1) / 2)){
                shapePos.z = shapePos.z - 2.75f;
                transform.position = shapePos;
            }
        }
    }
}
