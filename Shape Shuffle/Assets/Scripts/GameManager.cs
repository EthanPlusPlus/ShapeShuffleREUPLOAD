using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] shps;
    GameObject startingShp;

    public int shpNum, levelNum, laneNum;

    
    void Awake()
    {
        ChooseMesh();
    }

    void Start()
    {

    }

    void Update()
    {
        
    }

    void Difficulty()
    {
        
    }

    void ChooseMesh()
    {
        shpNum = Random.Range(0, 2);
        shps[shpNum].SetActive(true);
    }
}
