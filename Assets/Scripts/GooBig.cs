using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooBig : MonoBehaviour
{

    private GameObject Player;

    public PlayerController PlayerController { get; private set; }  //Player Controller Script

    [SerializeField]
    private int points; //Score to add when destroyed by player

    [SerializeField]
    private int hp;

    private int maxHP;

    private Animator gooAnimator;

    private Vector3 xPlosionOffset;

    private GameObject sfxManager;   //Sound Effect Manager Object

    private SFXManager SFXManager; //Sound Effect Manager Script

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerController = Player.GetComponent<PlayerController>();

        gooAnimator = GetComponent<Animator>();

        xPlosionOffset = new Vector3(0, 0.5f, 0);

        sfxManager = GameObject.Find("SFXManager");
        SFXManager = sfxManager.GetComponent<SFXManager>();

        maxHP = hp;

    }

    private void OnTriggerEnter(Collider other)
    {


        if(other.tag == "Ammo")
        {


            hp--;

            if (hp > 1)
            {
                gooAnimator.SetTrigger("Hit");
                SFXManager.PlaySFX(2);
            }

            else if (hp < 1)
            {
                PlayerController.AddScore(points); //Adds score to player

                GameObject xPlosion = ObjectPool.SharedInstance.GetXplosion();
                xPlosion.transform.position = transform.position + xPlosionOffset;
                xPlosion.gameObject.SetActive(true);
                xPlosion.GetComponent<Xplosion>().StartXPlosion();

                SFXManager.PlaySFX(0);
                SFXManager.PlaySFX(1);
                hp = maxHP;
                gooAnimator.ResetTrigger("Hit");
                gameObject.SetActive(false);
            }
        }

    }
}
