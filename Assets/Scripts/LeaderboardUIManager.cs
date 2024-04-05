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

    public PlayerManager playerManager;  //PlayerManager script

    public TMP_InputField input;

    public ManagerMask Mask; //Mask to filter bad language etc.
    public string[] Sources; //Sources for bad language

    [SerializeField]
    private AudioSource sfx;

    [SerializeField]
    private Animator transition;

    private bool active; //Bool if buttons are active
    private bool nameEntered; //Bool to see if name is entered

    [SerializeField]
    private TextMeshProUGUI yourScore;

    private void Start()
    {
        input = playerManager.playerNameInputField;
        input.characterLimit = 12;
        active = false;

        nameEntered = false;

        yourScore.text = "YOUR SCORE: " + PlayerPrefs.GetInt("CurrentScore").ToString(); //Gets latest score from PlayerPrefs.

        StartCoroutine(StartTransition());
    }


    public void BackToMainMenu()    //Method to return to main menu when button is clicked.
    {

        if(active == true)
        {

            StartCoroutine(ChangeScene());
        }

        
    }

    public void SettingName()   //Method for setting the players name from input.
    {
        
        if(active == true && nameEntered == false)
        {
            input.text = BWFManager.Instance.ReplaceAll(input.text, Mask, Sources);
            sfx.Play();
            nameEntered = true;
            StartCoroutine(StartNameSetup());
        }
        
    }

    private IEnumerator StartNameSetup() //Couroutine to start SetupRoutine from playerManager & to activate mainMenu Button
    {
        yield return playerManager.SetupRoutine();
        mainMenu.gameObject.SetActive(true);
    }

    private IEnumerator ChangeScene()    //Couroutine to change the scene
    {
        active = false;
        sfx.Play();
        transition.SetTrigger("End");
        yield return new WaitForSecondsRealtime(2f);
        SceneManager.LoadScene("MainMenuScene", LoadSceneMode.Single);
    }

    private IEnumerator StartTransition()  //Couroutine for start transition animation
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(2);
        active = true;
    }
}
