using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] shps;
    GameObject startingShp;

    int levelNum, laneNum;

    void Start()
    {
        ChooseMesh();
    }

    void Update()
    {
        
    }

    void Difficulty()
    {
        
    }

    void ChooseMesh()
    {
        shps[Random.Range(0, 4)].SetActive(true);
    }
}
