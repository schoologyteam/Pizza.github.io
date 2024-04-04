using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Crosstales.BWF;
using Crosstales.BWF.Model;
using Crosstales.BWF.Model.Enum;
using Crosstales.BWF.Manager;

public class LeaderboardUIManager : MonoBehaviour
{


    public Button mainMenu;
    public Button setName;

    public PlayerManager playerManager;

    public TMP_InputField input;

    public ManagerMask Mask;
    public string[] Sources;

    [SerializeField]
    private AudioSource sfx;

    [SerializeField]
    private Animator transition;

    private bool active;
    private bool nameEntered;

    private void Start()
    {
        input = playerManager.playerNameInputField;
        input.characterLimit = 12;
        active = false;

        nameEntered = false;

        StartCoroutine(StartTransition());
    }


    public void BackToMainMenu()
    {

        if(active == true)
        {

            StartCoroutine(ChangeScene());
        }

        
    }

    public void SettingName()
    {
        
        if(active == true && nameEntered == false)
        {
            input.text = BWFManager.Instance.ReplaceAll(input.text, Mask, Sources);
            sfx.Play();
            nameEntered = true;
            StartCoroutine(StartNameSetup());
        }
        
    }

    private IEnumerator StartNameSetup()
    {
        yield return playerManager.SetupRoutine();
        mainMenu.gameObject.SetActive(true);
    }

    private IEnumerator ChangeScene()
    {
        active = false;
        sfx.Play();
        transition.SetTrigger("End");
        yield return new WaitForSecondsRealtime(2f);
        SceneManager.LoadScene("MainMenuScene", LoadSceneMode.Single);
    }

    private IEnumerator StartTransition()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(2);
        active = true;
    }
}
