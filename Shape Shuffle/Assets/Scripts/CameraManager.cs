using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    GameManager gm;

    Rigidbody camR;

    void Awake()
    {
        gm = (GameManager)FindObjectOfType(typeof(GameManager));

        camR = GetComponent<Rigidbody>();
    }

    void Start()
    {
        CamMove();    
    }

    void Update()
    {
        
    }

    public void CamMove()
    {
        float xVec, yVec;

        xVec = Mathf.Sin(1.308997f);             //75 * (Mathf.PI/180)
        yVec = Mathf.Cos(1.308997f);             //75 * (Mathf.PI/180)

        camR.AddForce(xVec * gm.speed, -yVec * gm.speed, 0);
    }
}
