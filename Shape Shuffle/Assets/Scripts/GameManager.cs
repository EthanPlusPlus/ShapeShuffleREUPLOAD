using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{   
    CameraManager cm;

    public GameObject[] shps;
    public GameObject centreShp;

    [HideInInspector] public int shpNum;

    public int shpCount;
    public int laneNum = 0;
    public int levelNum;

    float speedMax10, speedMin10, speedMin20, speedMax20, speedMin50, speedMax50;
    float distMin10, distMax10, distMin20, distMax20, distMin50, distMax50; 

    public float speedLerp, distLerp;            //manipulate this
    public float speed, dist;         //output this
    
    void Awake()
    {
        cm = (CameraManager)FindObjectOfType(typeof(CameraManager));
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

        ChooseMesh();
    }

    void Update()
    {
        
    }

    void Difficulty()
    {

        if(levelNum < 11){
            laneNum = 3;

            shpCount = 1;

            speedLerp = (float)levelNum / 10;
            speed = Mathf.Lerp(speedMin10, speedMax10, speedLerp);

            distLerp = (float)levelNum / 10;
            dist = Mathf.Lerp(distMin10, distMax10, distLerp);
        }
        else if(levelNum > 10 && levelNum < 21){
            laneNum = 5;

            shpCount = 2;

            speedLerp = (float)levelNum / 20;
            speed = Mathf.Lerp(speedMin20, speedMax20, speedLerp);
        
            distLerp = (float)levelNum / 20;
            dist = Mathf.Lerp(distMin20, distMax20, distLerp);
        }
        else if(levelNum > 20 && levelNum < 50){
            laneNum = 7;

            shpCount = 3;

            speedLerp = (float)levelNum / 50;
            speed = Mathf.Lerp(speedMin50, speedMax50, speedLerp);

            distLerp = (float)levelNum / 50;
            dist = Mathf.Lerp(distMin50, distMax50, distLerp);
        }
        
        cm.CamMove();
    }

    void ChooseMesh()
    {
        shpNum = Random.Range(0, 3);
        Instantiate(shps[shpNum], new Vector3(7.06f, 38.5f), Rotate(shpNum));

        if(shpCount > 1){
            for (int i = 2; i < shpCount+1; i++)
            {
                if(i % 2 == 0){
                    GameObject shpTemp = Instantiate(shps[shpNum], new Vector3(7.06f, 38.5f, 1.75f * (1.57142f*(shpCount -1*(shpCount-1)))), Rotate(shpNum));    
                    for (int k = 0; k < shpTemp.transform.childCount; k++)
                    {
                        shpTemp.transform.GetChild(k).gameObject.SetActive(false);    
                    }
                }else{
                    GameObject shpTemp = Instantiate(shps[shpNum], new Vector3(7.06f, 38.5f, -1.75f * (1.57142f*(shpCount -2))), Rotate(shpNum));
                    for (int k = 0; k < shpTemp.transform.childCount; k++)
                    {
                        shpTemp.transform.GetChild(k).gameObject.SetActive(false);    
                    }
                }
            }
        }

        //shps[shpNum].SetActive(true);
    }

    Quaternion Rotate(int shpNum)
    {
        Quaternion rot;
        switch (shpNum)
        {
            case 0:
                rot = Quaternion.Euler(0, 0, -15);
                break;
            case 1:
                rot = Quaternion.Euler(-90, 0, 0);
                break;
            case 2:
                rot = Quaternion.Euler(-75, 90, 0);
                break;

            default: 
                rot = Quaternion.identity;
                break;
        }
        return rot;
    }
}
