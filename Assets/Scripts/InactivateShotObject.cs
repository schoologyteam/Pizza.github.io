using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InactivateShotObject : MonoBehaviour
{

    private GameObject Player;

    public PlayerController PlayerController { get; private set; }

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerController = Player.GetComponent<PlayerController>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ammo")
        {
            PlayerController.AddScore(10);
            this.gameObject.SetActive(false);
        }
    }

    
}
