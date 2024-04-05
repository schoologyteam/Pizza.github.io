using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InactivateShotObject : MonoBehaviour
{

    private GameObject Player;

    public PlayerController PlayerController { get; private set; }    //Player Controller Script

    [SerializeField]
    private int points;  //Score to add when destroyed by player

    private Vector3 xPlosionOffset;

    private GameObject sfxManager;

    private SFXManager SFXManager;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerController = Player.GetComponent<PlayerController>();

        xPlosionOffset = new Vector3(0,0.5f,0);

        sfxManager = GameObject.Find("SFXManager");
        SFXManager = sfxManager.GetComponent<SFXManager>();

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ammo")
        {
            PlayerController.AddScore(points);

            GameObject xPlosion = ObjectPool.SharedInstance.GetXplosion();
            xPlosion.transform.position = transform.position + xPlosionOffset;
            xPlosion.gameObject.SetActive(true);
            xPlosion.GetComponent<Xplosion>().StartXPlosion();
            SFXManager.PlaySFX(0);
            gameObject.SetActive(false);



        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GameObject xPlosion = ObjectPool.SharedInstance.GetXplosion();
            xPlosion.transform.position = transform.position + xPlosionOffset;
            xPlosion.gameObject.SetActive(true);
            xPlosion.GetComponent<Xplosion>().StartXPlosion();
            SFXManager.PlaySFX(0);
            gameObject.SetActive(false);

            //StartCoroutine(Activator());
        }
    }


}
