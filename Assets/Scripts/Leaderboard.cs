using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using TMPro;

public class Leaderboard : MonoBehaviour
{


    private string LeaderboardKey = "pizzapocalypse_lb";

    public TextMeshProUGUI playerNames;
    public TextMeshProUGUI playerScores;


    public IEnumerator SubmitScore(int scoreToUpload)
    {
        bool done = false;
        string PlayerID = PlayerPrefs.GetString("PlayerID");

        LootLockerSDKManager.SubmitScore(PlayerID, scoreToUpload, LeaderboardKey, (response) =>
        {

            if (response.success)
            {
                Debug.Log("Score uploaded succesfully");
                done = true;
            }

            else
            {
                Debug.Log("Failed: " + response.errorData);
                done = true;
            }


        });

        yield return new WaitWhile(() => done == false);
    }

    public IEnumerator FetchHighScores()
    {
        bool done = false;

        LootLockerSDKManager.GetScoreList(LeaderboardKey, 10, 0, (response) =>
        {

            if (response.success)
            {
                string tempPlayerNames = "Names\n";
                string tempPlayerScores = "Scores\n";

                LootLockerLeaderboardMember[] members = response.items; 

                for(int i = 0; i < members.Length; i++)
                {
                    tempPlayerNames += members[i].rank + ". ";
                    if(members[i].player.name != "")
                    {
                        tempPlayerNames += members[i].player.name;
                    }
                    else
                    {
                        tempPlayerNames += members[i].player.id;
                    }

                    tempPlayerScores += members[i].score + "\n";
                    tempPlayerNames += "\n";
                }
                done = true;
                playerNames.gameObject.SetActive(true);
                playerScores.gameObject.SetActive(true);
                playerNames.text = tempPlayerNames;
                playerScores.text = tempPlayerScores;
            }

            else
            {
                Debug.Log("Error Getting Scores: " + response.errorData);
                done = true;
            }


        });

        yield return new WaitWhile(() => done == false);
    }
}
