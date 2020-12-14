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
        Vector3 cShp;
        int randShp = Random.Range(0, wallShps.Count - 3);
        print(randShp);
        for (int i = 0; i < gm.shpCount; i++)
        {
            cShp = correctWalls[i].transform.position;
            correctWalls[i].transform.position = wallShps[randShp + i].transform.position;
            wallShps[randShp + i].transform.position = cShp;    
            print(wallShps[randShp + i].transform.position);
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
