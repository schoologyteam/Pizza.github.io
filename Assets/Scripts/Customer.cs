using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{

    private Animator customerAnimator;

    private GameObject Player;

    public PlayerController PlayerController { get; private set; }

    private GameObject sfxManager;

    private SFXManager SFXManager;

    // Start is called before the first frame update
    void Start()
    {
        customerAnimator = GetComponent<Animator>();

        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerController = Player.GetComponent<PlayerController>();

        sfxManager = GameObject.Find("SFXManager");
        SFXManager = sfxManager.GetComponent<SFXManager>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && PlayerController.amoutOfPizzas > 0)
        {
            SFXManager.PlaySFX(4);
            customerAnimator.SetTrigger("celebrate");
        }
    }


}
