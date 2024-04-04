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

    private bool clicked;
    private bool started;


    private void Start()
    {

        clicked = false;
        started = false;
        StartCoroutine(StartTransition());
    }

    public void ExitGame()
    {
        if(started == true)
        {
            sfx.Play();
            Application.Quit();
        }
        
        
    }

    public void StartGame()
    {
        if(clicked == false && started == true)
        {
            StartCoroutine(StartingGame());
            
        }
        
    }

    private IEnumerator StartingGame()
    {
        
          sfx.Play();
          clicked = true;
          ani.SetTrigger("End");
          yield return new WaitForSeconds(2f);
          SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
        
        

    }

    private IEnumerator StartTransition()
    {
        ani.SetTrigger("Start");
        yield return new WaitForSeconds(2f);
        started = true;

    }

}
