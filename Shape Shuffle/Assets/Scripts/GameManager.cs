using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{   
    CameraManager cm;

    public GameObject[] shps;
    public GameObject centreShp;

    public Dictionary<int, GameObject> currShps = new Dictionary<int, GameObject>();

    public List<int> shpNum;

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
        speedMin10 = 700;
        speedMax10 = 840;
        speedMin20 = 875;
        speedMax20 = 1050;
        speedMin50 = 1750;
        speedMax50 = 2100;
        distMin10 = 40;
        distMax10 = 30;
        distMin20 = 70;
        distMax20 = 40;
        distMin50 = 80; //20
        distMax50 = 60; //10
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
        for (int i = 0; i < shpCount; i++)
        {
            shpNum.Add(Random.Range(0, 3));
            //currShps.Add()
        }

        //shpNum = Random.Range(0, 3);
        Instantiate(shps[shpNum[0]], new Vector3(7.06f, 38.5f), Rotate(shpNum[0]));

        
        if(shpCount > 1){
            int numTemp = 1;
            for (int i = 2; i < shpCount+1; i++)
            {
                if(i % 2 == 0){
                    GameObject shpTemp = Instantiate(shps[shpNum[numTemp]], new Vector3(7.06f, 38.5f, 1.75f * (1.57142f*(shpCount -1*(shpCount-1)))), Rotate(shpNum[numTemp]));    
                    for (int k = 0; k < shpTemp.transform.childCount; k++)
                    {
                        shpTemp.transform.GetChild(k).gameObject.SetActive(false);    
                    }
                    numTemp = numTemp + 1;
                }else{
                    GameObject shpTemp = Instantiate(shps[shpNum[numTemp]], new Vector3(7.06f, 38.5f, -1.75f * (1.57142f*(shpCount -2))), Rotate(shpNum[numTemp]));
                    for (int k = 0; k < shpTemp.transform.childCount; k++)
                    {
                        shpTemp.transform.GetChild(k).gameObject.SetActive(false);    
                    }
                    numTemp = numTemp + 1;
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
