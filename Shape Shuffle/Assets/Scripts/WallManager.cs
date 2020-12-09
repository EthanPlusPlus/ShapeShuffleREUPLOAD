using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManager : MonoBehaviour
{
    GameManager gm;
    ShapeMovement shapeMovement;

    public GameObject[] walls;
    public GameObject wallParent;
    GameObject correctWall;

    void Start()
    {
        shapeMovement = (ShapeMovement)FindObjectOfType(typeof(ShapeMovement));
        gm = (GameManager)FindObjectOfType(typeof(GameManager));

        Spawn(40);
    }

    void Update()
    {
        
    }

    void Spawn(float dist)    //leftz -21.56 centrez -13.3278 rightz -27.054 filler1 -22.935 filler2 -25.682
    {
        //build wall
        
        wallParent = Instantiate(wallParent, Vector3.zero, Quaternion.identity);

        correctWall = Instantiate(walls[gm.shpNum], Vector3.zero, Rotate(gm.shpNum));
        correctWall.transform.parent = wallParent.transform;


        float zR = 0;
        float zL = 0;

        for (int i = 2; i < gm.laneNum + 1; i++)    //right side
        {
            if(i % 2 == 1){
            
                int r = Random.Range(0, 3);
                zR -= 1.376f;
                GameObject wallRTemp = Instantiate(walls[r], new Vector3(0,0,zR), Rotate(r));
                wallRTemp.transform.SetParent(wallParent.transform);

            }else if(i % 2 == 0){
                
                zR -= 1.376f;
                GameObject wallRTemp = Instantiate(walls[walls.Length - 1], new Vector3(0,0,zR), Rotate(100));
                wallRTemp.transform.SetParent(wallParent.transform);
            }
        }

        for (int i = 2; i < gm.laneNum + 1; i++)    //left side
        {
            if(i % 2 == 1){
                
                int l = Random.Range(0, 3);
                zL += 1.375f;
                //Instantiate(walls[l], new Vector3(0,0,zL), Rotate(l));
                GameObject wallLTemp = Instantiate(walls[l], new Vector3(0,0,zL), Rotate(l));//Instantiate(walls[walls.Length - 1], new Vector3(0,0,zR), Rotate(100));
                wallLTemp.transform.SetParent(wallParent.transform);
            
            }else if(i % 2 == 0){
                zL += 1.375f;
                //Instantiate(walls[walls.Length - 1], new Vector3(0,0,zL), Rotate(100));
                GameObject wallLTemp = Instantiate(walls[walls.Length - 1], new Vector3(0,0,zL), Rotate(100));
                wallLTemp.transform.SetParent(wallParent.transform);
            }
        }

        // spawn new wall
        //Instantiate(wall, new Vector3(Mathf.Sin(1.308997f) * dist, Mathf.Cos(1.308997f) * -dist + 5.65f, 0), Quaternion.identity);

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
            default:
                rot = Quaternion.Euler(15,90,0);
                break;
        }

        return rot;
    }
}
