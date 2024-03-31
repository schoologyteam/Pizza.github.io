using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{

    
    public List<AudioSource> sfxList = new List<AudioSource>();


    public void PlaySFX(int sfxToPlay)
    {

        Debug.Log(sfxToPlay);
        Debug.Log(sfxList.Count-1);

        if (sfxList.Count == 0)
        {
            Debug.Log("No Audio Found!!!");
        }

        else if(sfxToPlay < 0)
        {
            sfxList[0].Play();
            Debug.Log("1");

        }

        else if(sfxToPlay == sfxList.Count)
        {
            sfxList[sfxList.Count].Play();
            Debug.Log("2");

        }
        else
        {
            sfxList[sfxToPlay].Play();
            Debug.Log("3");

        }

    }
}
