using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{   //store inputType in PLayerPrefs
    [SerializeField] GameManager gm;
    [SerializeField] CameraManager cm;
    //[SerializeField] ShapeMovement sm;
    [SerializeField] WallManager wm;
    [SerializeField] ShapeMovement[] sms;

    [SerializeField] Canvas mainCan;
    [SerializeField] Text completedTxt, levelTxt, tap2playTxt, swipeTxt, tapTxt;
    [SerializeField] Button inputBtn;

    bool playGame;
    bool runOnce = false;
 
    void Awake()
    {
        //PlayerPrefs.SetInt("Level", gm.levelNum);
    }

    void Start()
    {
        //PlayerPrefs.DeleteKey("Level");
        
        levelTxt.text = PlayerPrefs.GetInt("Level", 1).ToString();
        gm.swipeInput = (bool)(PlayerPrefs.GetInt("InputType", 1) == 1);
        print(gm.swipeInput);
        UpdateInput();
    }

    void Update()
    {
        if(sms[0] == null){                                         //assign shapemovements
            sms = GameObject.FindObjectsOfType<ShapeMovement>();
        }

        if(playGame){               //pressed play
            cm.CamMove();

            OnWon();        //check won
            OnLost();
        }
    }

    public void PlayButton()
    {
        ActivateScripts();
        print("click");
    }

    void ActivateScripts()  //everythinh to happen once, once pressed play
    {
        playGame = true;

        for (int i = 0; i < gm.shpCount; i++)
        {
            sms[i].enabled = true;
        }
        cm.enabled = true;

        cm.sm = gm.currShps[0].gameObject.GetComponent<ShapeMovement>();
    }

    void OnWon()
    {
        if(gm.won){ 
            completedTxt.enabled = true;

            Invoke("LoadNextSccene", 3);
            
        }
    }

    void OnLost()
    {
        if(gm.lost){
            Invoke("LoadSameSccene", 3);
        }
    }

    void LoadNextSccene()
    {
        
        if(!runOnce){
            gm.won = false;
            playGame = false;

            int levelTemp = 0;
            levelTemp = PlayerPrefs.GetInt("Level", 1);
            levelTemp += 1;
            PlayerPrefs.SetInt("Level", levelTemp);

            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            runOnce = true;
        }
        
    }

    void LoadSameSccene()
    {
        if(!runOnce){
            gm.lost = false;
            playGame = false;

            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            runOnce = true;
        }
    } 
    

    public void ShowSettings()
    {
        if(gm.showSettings){
            inputBtn.gameObject.SetActive(false); 
            gm.showSettings = false;
        }else{
            inputBtn.gameObject.SetActive(true);
            gm.showSettings = true;
        }
    }

    public void SwitchInput()
    {
        if(gm.swipeInput){
            swipeTxt.enabled = false;
            tapTxt.enabled = true;
            gm.swipeInput = false;
        }else{
            swipeTxt.enabled = true;
            tapTxt.enabled = false;
            gm.swipeInput = true;
        }

        PlayerPrefs.SetInt("InputType", (gm.swipeInput ? 1 : 0));
        print("saved" + PlayerPrefs.GetInt("InputType"));
    }
    void UpdateInput()
    {
        if(gm.swipeInput){
            swipeTxt.enabled = true;
            tapTxt.enabled = false;
        }else{
            swipeTxt.enabled = false;
            tapTxt.enabled = true;
        }
    }
}
