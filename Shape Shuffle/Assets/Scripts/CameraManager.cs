using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    GameManager gm;
    WallManager wm;
    ShapeMovement sm;

    Rigidbody camR;

    public GameObject zoomGo;

    Vector3 startPos;

    public float zoomSpeed, magintudeShake;
    public float durationShake = 3;
    float startTime;
    float startRotX;
     
    float xVec, yVec;

    bool startRecorded = false;
    

    void Awake()
    {
        gm = (GameManager)FindObjectOfType(typeof(GameManager));
        wm = (WallManager)FindObjectOfType(typeof(WallManager));
        //sm = gm.currShps[0].gameObject.GetComponent<ShapeMovement>();

        camR = GetComponent<Rigidbody>();
    }

    void Start()
    {
        sm = gm.currShps[0].gameObject.GetComponent<ShapeMovement>();
    }

    void Update()
    {   
        EndLvlExecute();
    }

    public void CamMove()
    {
        

        xVec = Mathf.Sin(1.308997f);             //75 * (Mathf.PI/180)
        yVec = Mathf.Cos(1.308997f);             //75 * (Mathf.PI/180)

        if(sm.accel){
            camR.AddForce(xVec * gm.speed, -yVec * gm.speed, 0);
        }
        else{
            //if(sm.currentWall != 0){
                camR.AddForce(-xVec * gm.speed * 3f, yVec * gm.speed * 3, 0);
            //}
        }
    }

    void ZoomOut(float speed, float distOffset)
    {
        float distance = Vector3.Distance(new Vector3(zoomGo.transform.position.x - distOffset, zoomGo.transform.position.y + distOffset/1.5f), startPos);

        float distCovered = (Time.time - startTime) * speed;            //Time.time changes variable
        float currDistLerp = (float)distCovered/distance;
        float rotCovered = Time.time - startTime;
        float currRotLerp = (float)rotCovered/5;
    
        transform.position = new Vector3(Mathf.SmoothStep(startPos.x, zoomGo.transform.position.x - distOffset, currDistLerp),Mathf.SmoothStep(startPos.y, zoomGo.transform.position.y + distOffset/1.5f, currDistLerp),0);
        transform.rotation = Quaternion.Euler(0, 0, -Mathf.SmoothStep(0, 25, currRotLerp));
    }

    IEnumerator Shake()
    {
        while(Time.time - startTime < durationShake){
            float z = Random.Range(-1f, 1f) * magintudeShake;

            transform.position = new Vector3(transform.position.x, transform.position.y, z);

            yield return null;
        }
    }

    void EndLvlExecute()
    {
        if(gm.lost){
            
            if(!startRecorded){
                startPos = transform.position;
                startRotX = transform.rotation.x;
                startTime = Time.time;
                camR.AddForce(xVec * -gm.speed, yVec * gm.speed, 0);
                zoomGo.transform.SetParent(null);

                StartCoroutine(Shake());

                startRecorded = true;
            }
            
            ZoomOut(12, 0);
        }
        else if(gm.won){

            if(!startRecorded){
                startPos = transform.position;
                startRotX = transform.rotation.x;
                startTime = Time.time;
                camR.AddForce(xVec * -gm.speed, yVec * gm.speed, 0);
                zoomGo.transform.SetParent(null);
                
                List<GameObject> tw = new List<GameObject>();
                tw = wm.totalWalls;
                ParticleSystem[] con = gm.psConfetti;

                for (int i = 0; i < 3; i++)
                {
                    con[i].gameObject.transform.position = tw[tw.Count-1].transform.position;
                    con[i].Play();
                }
                tw[tw.Count-1].SetActive(false);
                startRecorded = true;
            }            

            ZoomOut(25, 60);

        }
    }
}
