using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LeaderboardUIManager : MonoBehaviour
{


    public Button mainMenu;
    public Button setName;

    public PlayerManager playerManager;

    [SerializeField]
    private AudioSource sfx;


    public void BackToMainMenu()
    {

        StartCoroutine(ChangeScene());
    }

    public void SettingName()
    {
        sfx.Play();
        StartCoroutine(StartNameSetup());
        
    }

    private IEnumerator StartNameSetup()
    {
        yield return playerManager.SetupRoutine();
        mainMenu.gameObject.SetActive(true);
    }

    private IEnumerator ChangeScene()
    {
        sfx.Play();
        yield return new WaitForSecondsRealtime(0.25f);
        SceneManager.LoadScene("MainMenuScene", LoadSceneMode.Single);
    }
}
