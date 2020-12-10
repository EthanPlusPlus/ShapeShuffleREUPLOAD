using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManager : MonoBehaviour
{
    GameManager gm;
    ShapeMovement shapeMovement;

    public List<GameObject> wallShps = new List<GameObject>();
    public GameObject[] walls;
    GameObject wallParent;
    GameObject correctWall, wallParentClean;

    float currDist;

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
        for (int i = 1; i <= wallNum; i++)
        {
            Build(dist);    
        }
    }

    void Build(float dist)    //leftz -21.56 centrez -13.3278 rightz -27.054 filler1 -22.935 filler2 -25.682
    {
        //build wall
        
        wallParent = Instantiate(wallParentClean, Vector3.zero, Quaternion.identity);

        correctWall = Instantiate(walls[gm.shpNum], Vector3.zero, Rotate(gm.shpNum));
        correctWall.transform.parent = wallParent.transform;


        float zR = 0;
        float zL = 0;

        for (int i = 2; i < gm.laneNum + 1; i++)    //right side
        {
            if(i % 2 == 1){
            
                int r = Random.Range(0, walls.Length-1);
                zR -= 1.376f;
                GameObject wallRTemp = Instantiate(walls[r], new Vector3(0,0,zR), Rotate(r));
                wallRTemp.transform.SetParent(wallParent.transform);
                print("made right object");

            }else if(i % 2 == 0){
                
                zR -= 1.376f;
                GameObject wallRTemp = Instantiate(walls[walls.Length - 1], new Vector3(0,0,zR), Rotate(100));
                wallRTemp.transform.SetParent(wallParent.transform);
            }
        }

        for (int i = 2; i < gm.laneNum + 1; i++)    //left side
        {
            if(i % 2 == 1){
                
                int l = Random.Range(0, walls.Length-1);
                zL += 1.375f;
                GameObject wallLTemp = Instantiate(walls[l], new Vector3(0,0,zL), Rotate(l));//Instantiate(walls[walls.Length - 1], new Vector3(0,0,zR), Rotate(100));
                wallLTemp.transform.SetParent(wallParent.transform);
            
            }else if(i % 2 == 0){
                zL += 1.375f;
                GameObject wallLTemp = Instantiate(walls[walls.Length - 1], new Vector3(0,0,zL), Rotate(100));
                wallLTemp.transform.SetParent(wallParent.transform);
            }
        }

        Swop();

        Spawn(dist);
        // spawn new wall
        

    }

    void Swop()
    {
        //fetch childern
        for (int i = 0; i < 2 * (gm.laneNum) ; i++)
        {    
            if(i % 2 == 0 && i != 1){
                wallShps.Add(wallParent.transform.GetChild(i).gameObject);
            }
        }
        
        //swop
        Vector3 cShp;
        int randShp = Random.Range(0, wallShps.Count - 1);

        cShp = wallShps[0].transform.position;
        wallShps[0].transform.position = wallShps[randShp].transform.position;
        wallShps[randShp].transform.position = cShp;
        //print("swopped" + randShp);
    }

    void Spawn(float dist)
    {
        currDist += dist;
        wallParent.transform.position = new Vector3(Mathf.Sin(1.308997f) * currDist, Mathf.Cos(1.308997f) * -currDist + 40.56276f, 0);  //+offset
        wallShps.Clear();
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
