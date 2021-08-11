using System.Collections;
using System.Collections.Generic;
using LootLocker.Requests;
using UnityEngine;

public class LeaderBoard : MonoBehaviour
{

    SceneManager sm;

    public int levelLbId;

    void Awake()
    {
        if(!PlayerPrefs.HasKey("PlayerID")){
            PlayerPrefs.SetString("PlayerID", GenMemberID());
        }
    }

    void Start()
    {
        sm = (SceneManager)FindObjectOfType(typeof(SceneManager));

        LootLockerSDKManager.StartSession("Player", (response) =>
        {    
        
            if(response.success){

                print("y");

            }else{

                print("n");

            }
        });

    
    }

    
    void Update()
    {
        
    }

    public void ShowScores()
    {
        LootLockerSDKManager.GetScoreList(levelLbId, 10, (response) => 
        {
            if(response.success){

                LootLocker.Requests.LootLockerLeaderboardMember[] levels = response.items;

                for (int i = 0; i < levels.Length; i++)
                {
                    sm.levelLbNames[i].text = levels[i].rank + ". " + levels[i].score;
                }

            }else{

                print("n");

            }
        });
    }

    public void SubmitLevel(int levelNum)
    {
        LootLockerSDKManager.SubmitScore(PlayerPrefs.GetString("PlayerID", "000000"), levelNum, levelLbId, (response) =>
        {
            if(response.success){

                print("y");

            }else{

                print("n");

            }
        });
    }

    string GenMemberID()
    {
        string sLine = "";
 
        for (int i = 0; i < 6; i++)
        {
            sLine = sLine + Random.Range(1, 10);
        }

        return sLine;
    }
}
