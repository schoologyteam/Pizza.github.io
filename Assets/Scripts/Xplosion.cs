using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Xplosion : MonoBehaviour
{

    [SerializeField]
    private ParticleSystem particles;

    public void StartXPlosion()
    {
       
            StartCoroutine(Xploding());
       
    }


    public IEnumerator Xploding()  //Couroutine for enemy explosion effect.
    {
        particles.Play();
        yield return new WaitForSeconds(particles.main.duration);
        particles.Stop();
        transform.gameObject.SetActive(false);

    }

}
