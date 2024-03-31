using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource sfx;
    public void ExitGame()
    {
        sfx.Play();
        Application.Quit();
    }

    public void StartGame()
    {
        sfx.Play();
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }

}
