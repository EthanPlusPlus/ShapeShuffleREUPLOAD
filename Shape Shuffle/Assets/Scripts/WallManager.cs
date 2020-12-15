using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManager : MonoBehaviour
{
    GameManager gm;
    ShapeMovement shapeMovement;

    public List<GameObject> wallShps = new List<GameObject>();
    public List<GameObject> correctWalls = new List<GameObject>();
    List<GameObject> leftWalls = new List<GameObject>();
    public GameObject[] walls;
    public GameObject temp;
    GameObject wallParent;
    GameObject correctWall, wallParentClean;

    [SerializeField] GameObject road;

    float currDist;
    
    public int lC = 0;
    public int rC = 0;

    void Start()
    {
        shapeMovement = (ShapeMovement)FindObjectOfType(typeof(ShapeMovement));
        gm = (GameManager)FindObjectOfType(typeof(GameManager));

        wallParentClean = GameObject.FindGameObjectWithTag("wallParentClean");

        Execute(10, gm.dist);
    }

    void Update()
    {
        
    }

    void Execute(int wallNum, float dist) //dist 40
    {       
        //change width of road
        road.transform.localScale = new Vector3(road.transform.localScale.x, 1, (float)(gm.laneNum * 125.066) / 50.0266f); 

        for (int i = 1; i <= 1; i++)
        {
            Build(dist);    
        }
    }

    void Build(float dist)    //leftz -21.56 centrez -13.3278 rightz -27.054 filler1 -22.935 filler2 -25.682
    {
        //build wall
        
        wallParent = Instantiate(wallParentClean, Vector3.zero, Quaternion.identity);

        correctWall = Instantiate(walls[gm.shpNum[0]], Vector3.zero, Rotate(gm.shpNum[0]));
        correctWall.transform.parent = wallParent.transform;


        float zR = 0;
        float zL = 0;

        

        for (int i = 2; i < gm.laneNum + 1; i++)    //left side
        {
            if(i % 2 == 1){

                bool cWall = false;
                int l;

                lC += 1;
                if(lC < gm.shpCount - 1 * (gm.shpCount-2)){
                    l = gm.shpNum[lC];
                    cWall = true;
                }else{
                    l = Random.Range(0, walls.Length-1);
                    cWall = false;
                }
                
                zL += 1.375f;
                GameObject wallLTemp = Instantiate(walls[l], new Vector3(0,0,zL), Rotate(l));//Instantiate(walls[walls.Length - 1], new Vector3(0,0,zR), Rotate(100));
                wallLTemp.transform.SetParent(wallParent.transform);
                leftWalls.Add(wallLTemp);

                if(cWall){
                    correctWalls.Add(wallLTemp);
                    cWall = false;
                }

            }else if(i % 2 == 0){
                zL += 1.375f;
                GameObject wallLTemp = Instantiate(walls[walls.Length - 1], new Vector3(0,0,zL), Rotate(100));
                wallLTemp.transform.SetParent(wallParent.transform);
            }
        }

        wallShps.Add(correctWall);              //add middle in list
        correctWalls.Add(correctWall);          //add middle in the list
        
        for (int i = 2; i < gm.laneNum + 1; i++)    //right side
        {
            if(i % 2 == 1){
                
                bool cWall = false;
                int r;
                rC += 1;
                if(rC < gm.shpCount-1){
                    r = gm.shpNum[rC += (gm.shpCount-2)];    //this is questionable, may cause problems later
                    cWall = true;
                }else{
                    r = Random.Range(0, walls.Length-1);
                    cWall = false;
                }
                
                zR -= 1.376f;
                GameObject wallRTemp = Instantiate(walls[r], new Vector3(0,0,zR), Rotate(r));
                wallRTemp.transform.SetParent(wallParent.transform);
                wallShps.Add(wallRTemp);

                if(cWall){
                    correctWalls.Add(wallRTemp);
                    cWall = false;
                }

            }else if(i % 2 == 0){
                
                zR -= 1.376f;
                GameObject wallRTemp = Instantiate(walls[walls.Length - 1], new Vector3(0,0,zR), Rotate(100));
                wallRTemp.transform.SetParent(wallParent.transform);
            }
        }



        Swop();

        Spawn(dist);
        // spawn new wall
        

    }

    void Swop()
    {
        //add leftWall list
        int iTemp = 0;
        for (int i = leftWalls.Count-1; i > -1 ; i--)
        {
            wallShps.Insert(iTemp, leftWalls[i]);
            ++iTemp;
        }
        
        //swop
        Vector3 cShp = Vector3.zero;
        Vector3[] oWallPos = new Vector3[wallShps.Count];
        Vector3[] cWallPos = new Vector3[correctWalls.Count];
        
        int randShp = -1;//Random.Range(0, wallShps.Count - 2);     0-5
        //cShp1 = wallShps[randShp + gm.shpCount + 1].transform.position;
        
        for (int k = 0; k < wallShps.Count; k++)
        {   
            try
            {
                oWallPos[k] = wallShps[k].transform.position;    
                cWallPos[k] = correctWalls[k].transform.position;    
               
            }
            catch (System.Exception){}
        }
        
        if(randShp > -1){
            for (int j = 0; j < gm.shpCount; j++)
            {
                correctWalls[j].transform.position = oWallPos[j + gm.shpCount-1 + randShp];   //2 and randShp is offset
                if(j < randShp){
                    wallShps[gm.shpCount + gm.shpCount-1 + j].transform.position = cWallPos[j];
                    //print(cWallPos[j]);
                }
            }
            
        }else{
            int iLeftSwop = 0;
            for (int l = gm.shpCount-1; l > -1; l--)
            {
                //print(iLeftSwop + gm.shpCount-1 + randShp); 
                correctWalls[iLeftSwop].transform.position = oWallPos[iLeftSwop + gm.shpCount-1 + randShp];   //2 and randShp is offset
                if(iLeftSwop < -randShp){
                    wallShps[gm.laneNum - gm.shpCount - gm.shpCount-(3-gm.shpCount) - iLeftSwop].transform.position = cWallPos[l];
                    print(gm.laneNum - gm.shpCount - gm.shpCount - iLeftSwop);
                }
                ++iLeftSwop;
                
            }
            
        }
        //print("swopped" + randShp);
    }

    void Spawn(float dist)
    {
        currDist += dist;
        wallParent.transform.position = new Vector3(Mathf.Sin(1.308997f) * currDist, Mathf.Cos(1.308997f) * -currDist + 40.56276f, 0);  //+offset
        //wallShps.Clear();
    }

    Quaternion Rotate(int shpNum)
    {
        Quaternion rot;
        switch (shpNum)
        {
            case 0:
                rot = Quaternion.Euler(15, 90, 0);
                break;
            case 1:
                rot = Quaternion.Euler(15, 90, 0);
                break;
            case 2:
                rot = Quaternion.Euler(15, 90, -90);
                break;
            case 3:
                rot = Quaternion.Euler(15, 90, 0);
                break;
            case 4:
                rot = Quaternion.Euler(15, 90, 0);
                break;
            case 5:
                rot = Quaternion.Euler(15, 90, -90);
                break;
            default:
                rot = Quaternion.Euler(15,90,0);
                break;
        }

        return rot;
    }
}
