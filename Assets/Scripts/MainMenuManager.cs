using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource sfx;

    [SerializeField]
    private Animator ani;

    private bool clicked;  //Bool to see if Start Button has been clicked
    private bool started;  // //Bool if buttons are active


    private void Start()
    {

        clicked = false;
        started = false;
        StartCoroutine(StartTransition());
    }

    public void ExitGame() //Method for quitting the game when Quit Button is clicked.
    {
        if(started == true)
        {
            sfx.Play();
            Application.Quit();
        }
        
        
    }

    public void StartGame()  //Method for starting the game when Start Button is clicked.
    {
        if(clicked == false && started == true)
        {
            StartCoroutine(StartingGame());
            
        }
        
    }

    private IEnumerator StartingGame() //Couroutine to start the game
    {
        
          sfx.Play();
          clicked = true;
          ani.SetTrigger("End");
          yield return new WaitForSeconds(2f);
          SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
        
        

    }

    private IEnumerator StartTransition()   //Couroutine for start transition animation
    {
        ani.SetTrigger("Start");
        yield return new WaitForSeconds(2f);
        started = true;

    }

}
