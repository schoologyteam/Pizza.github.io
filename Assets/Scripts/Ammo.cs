using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{

    [SerializeField]
    private float ammoSpeed;


    // Update is called once per frame
    void Update()
    {
        
        transform.Translate(ammoSpeed / 100, 0, 0, Space.World);

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            gameObject.SetActive(false);
        }

        if (other.tag == "Ground")
        {
            gameObject.SetActive(false);
        }

    }
}
