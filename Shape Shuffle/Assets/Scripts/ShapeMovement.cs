using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeMovement : MonoBehaviour
{
    [SerializeField] float speed = 1;
    public float xVec, yVec;

    Rigidbody shpR;

    void Start()
    {
       shpR = GetComponent<Rigidbody>();

       Move();
    }

    void Update()
    {
        
    }

    void Move()
    {
        xVec = Mathf.Sin(1.308997f);             //75 * (Mathf.PI/180)
        yVec = Mathf.Cos(1.308997f);             //75 * (Mathf.PI/180)

        shpR.AddForce(xVec * speed, -yVec * speed, 0);
    }
}
