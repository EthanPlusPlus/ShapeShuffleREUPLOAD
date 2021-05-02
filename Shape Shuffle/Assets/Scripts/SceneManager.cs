using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    [SerializeField] GameManager gm;
    [SerializeField] CameraManager cm;
    //[SerializeField] ShapeMovement sm;
    [SerializeField] WallManager wm;
    [SerializeField] ShapeMovement[] sms;
 
    bool playGame;
 
    void Start()
    {
        
        //ActivateScripts();
    }

    void Update()
    {
        if(sms[0] == null){
            sms = GameObject.FindObjectsOfType<ShapeMovement>();
        }

        if(playGame){
            cm.CamMove();
        }
    }

    void ActivateScripts()  //everythinh to happen once pressed play
    {
        playGame = true;

        for (int i = 0; i < 1; i++)
        {
            sms[i].enabled = true;
        }
        cm.enabled = true;

        cm.sm = gm.currShps[0].gameObject.GetComponent<ShapeMovement>();
    }

    public void PlayButton()
    {
        ActivateScripts();
        print("click");
    }

    
}
