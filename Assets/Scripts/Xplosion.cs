using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Xplosion : MonoBehaviour
{

    [SerializeField]
    private ParticleSystem particles;

    private void Start()
    {
        //particles = GetComponent<ParticleSystem>();
    }

    public void StartXPlosion()
    {
       
            StartCoroutine(Xploding());
       
    }


    public IEnumerator Xploding()
    {
        particles.Play();
        yield return new WaitForSeconds(particles.main.duration);
        particles.Stop();
        transform.gameObject.SetActive(false);

    }

}
