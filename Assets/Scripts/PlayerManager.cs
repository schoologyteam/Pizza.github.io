using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using TMPro;

public class PlayerManager : MonoBehaviour
{


    public Leaderboard Leaderboard;

    public TMP_InputField playerNameInputField;

    IEnumerator SetPlayerName() //Couroutine to set players name
    {

        bool done = false;

        LootLockerSDKManager.SetPlayerName(playerNameInputField.text, (response) =>
         {

             if (response.success)
             {
                 Debug.Log("Setting Player name was a success");
                 done = true;
             }

             else
             {
                 Debug.Log("Setting Player name was not a success: " + response.errorData);
                 done = true;
             }

         });
        yield return new WaitWhile(() => done == false);
    }

    IEnumerator LoginRoutine() //Couroutine to login player to the server.
    {
        bool done = false;

        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (response.success)
            {
                Debug.Log("Player was logged in");
                PlayerPrefs.SetString("PlayerID", response.player_id.ToString());
                done = true;
            }
            else
            {
                Debug.Log("Player couldn't log in!!!");
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }

   public IEnumerator SetupRoutine()  //Couroutine to setup Leaderboard.
    {
        yield return LoginRoutine();
        yield return SetPlayerName();
        yield return Leaderboard.SubmitScore(PlayerPrefs.GetInt("CurrentScore"));
        yield return Leaderboard.FetchHighScores();
    }
}
