using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManager : MonoBehaviour
{
    ShapeMovement shapeMovement;

    public GameObject[] walls;
    public GameObject wall;

    void Start()
    {
        shapeMovement = (ShapeMovement)FindObjectOfType(typeof(ShapeMovement));

        Spawn(40);
    }

    void Update()
    {
        
    }

    void Spawn(float dist)    //leftz -21.56 centrez -13.3278 rightz -27.054 filler1 -22.935 filler2 -25.682
    {
        // calculate pos of new wall
        Instantiate(wall, new Vector3(Mathf.Sin(1.308997f) * dist, Mathf.Cos(1.308997f) * -dist + 5.65f, 0), Quaternion.identity);
    }
}
