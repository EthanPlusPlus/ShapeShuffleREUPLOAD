using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    GameManager gm;
    WallManager wm;
    [HideInInspector] public ShapeMovement sm;

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
        // sm = gm.currShps[0].gameObject.GetComponent<ShapeMovement>();
    }

    void Update()
    {   
        EndLvlExecute();
    }

    public void CamMove()
    {
        

        xVec = Mathf.Sin(1.308997f);             //75 * (Mathf.PI/180)
        yVec = Mathf.Cos(1.308997f);             //75 * (Mathf.PI/180)

        if(sm.currentWall == 0){
            //camR.AddForce(xVec * gm.speed * gm.speedPhone, -yVec * gm.speed * gm.speedPhone, 0);
            camR.AddForce(xVec * gm.speed * 0.9f * gm.speedPhone, -yVec * gm.speed * 0.9f * gm.speedPhone, 0);
        }
        else if(sm.accel){
            camR.AddForce(xVec * gm.speed * 0.9f * gm.speedPhone, -yVec * gm.speed * 0.9f * gm.speedPhone, 0);
        }
        else{
            camR.AddForce(-xVec * gm.speed * 3f * gm.speedPhone, yVec * gm.speed * 3 * gm.speedPhone, 0);
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
                camR.AddForce(xVec * -gm.speed * gm.speedPhone, yVec * gm.speed * gm.speedPhone, 0);
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
                camR.AddForce(xVec * -gm.speed * gm.speedPhone, yVec * gm.speed * gm.speedPhone, 0);
                zoomGo.transform.SetParent(null);
                
                List<GameObject> tw = new List<GameObject>();
                tw = wm.totalWalls;
                Vector3 lastWallPos = tw[tw.Count-1].transform.position;
                ParticleSystem[] con = gm.psConfetti;

                for (int i = 0; i < 3; i++)
                {
                    RandomiseFireworkPos(con[i].gameObject, lastWallPos);
                    //con[i].gameObject.transform.position = new Vector3(lastWallPos.x + Random.Range(50, 70), lastWallPos.y + Random.Range(30, 40));
                    //con[i].Play();
                    StartCoroutine(PlayFireworks(con[i]));
                }
                //tw[tw.Count-1].SetActive(false);
                startRecorded = true;
            }            

            ZoomOut(30, 65);

        }
    }

    void RandomiseFireworkPos(GameObject ps, Vector3 lastWallPos)
    {
        ps.transform.position = new Vector3(lastWallPos.x + Random.Range(50, 100), lastWallPos.y + Random.Range(30, 40), lastWallPos.z + Random.Range(-80, 80));
    }

    IEnumerator PlayFireworks(ParticleSystem ps)
    {
        yield return new WaitForSeconds(2);
        ps.Play();
    }
}
