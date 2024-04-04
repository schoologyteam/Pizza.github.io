using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{

    
    public List<AudioSource> sfxList = new List<AudioSource>();


    public void PlaySFX(int sfxToPlay)
    {

        //Debug.Log(sfxToPlay);
        //Debug.Log(sfxList.Count-1);

        if (sfxList.Count == 0)
        {
            Debug.Log("No Audio Found!!!");
        }

        else if(sfxToPlay < 0)
        {
            sfxList[0].Play();

        }

        else if(sfxToPlay == sfxList.Count)
        {
            sfxList[sfxList.Count].Play();

        }
        else
        {
            sfxList[sfxToPlay].Play();

        }

    }
}
