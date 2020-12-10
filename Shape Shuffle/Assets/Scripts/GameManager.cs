using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] shps;
    GameObject startingShp;

    [HideInInspector] public int shpNum;
    public int laneNum;
    public int levelNum;

    float speedMax10, speedMin10, speedMin20, speedMax20, speedMin50, speedMax50;
    float distMin10, distMax10, distMin20, distMax20, distMin50, distMax50; 

    public float speedLerp, distLerp;            //manipulate this
    public float speed, dist;         //output this
    
    void Awake()
    {
        ChooseMesh();
    }

    void Start()
    {
        //levelNum = 9;
        speedMin10 = 200;
        speedMax10 = 320;
        speedMin20 = 350;
        speedMax20 = 420;
        speedMin50 = 450;
        speedMax50 = 550;
        distMin10 = 40;
        distMax10 = 30;
        distMin20 = 35;
        distMax20 = 20;
        distMin50 = 20;
        distMax50 = 10;
        Difficulty();
    }

    void Update()
    {
        
    }

    void Difficulty()
    {

        if(levelNum < 11){
            speedLerp = (float)levelNum / 10;
            speed = Mathf.Lerp(speedMin10, speedMax10, speedLerp);

            distLerp = (float)levelNum / 10;
            dist = Mathf.Lerp(distMin10, distMax10, distLerp);
        }
        else if(levelNum > 10 && levelNum < 21){
            speedLerp = (float)levelNum / 20;
            speed = Mathf.Lerp(speedMin20, speedMax20, speedLerp);
        
            distLerp = (float)levelNum / 20;
            dist = Mathf.Lerp(distMin20, distMax20, distLerp);
        }
        else if(levelNum > 20 && levelNum < 50){
            speedLerp = (float)levelNum / 50;
            speed = Mathf.Lerp(speedMin50, speedMax50, speedLerp);

            distLerp = (float)levelNum / 50;
            dist = Mathf.Lerp(distMin50, distMax50, distLerp);
        }
        
    }

    void ChooseMesh()
    {
        shpNum = Random.Range(0, 3);
        shps[shpNum].SetActive(true);
    }
}
