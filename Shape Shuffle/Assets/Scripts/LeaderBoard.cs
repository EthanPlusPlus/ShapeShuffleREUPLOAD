using System.Collections;
using System.Collections.Generic;
using LootLocker.Requests;
using UnityEngine;

public class LeaderBoard : MonoBehaviour
{

    public int levelLbId;

    void Start()
    {
        LootLockerSDKManager.StartSession("Player", (response) =>
        {    
        
            if(response.success){

                print("y");

            }else{

                print("n");

            }
        });

        SubmitScore();
    }

    
    void Update()
    {
        
    }

    public void SubmitScore()
    {
        LootLockerSDKManager.SubmitScore(GenMemberID(), 10, levelLbId, (response) =>
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
