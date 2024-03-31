using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooBig : MonoBehaviour
{

    private GameObject Player;

    public PlayerController PlayerController { get; private set; }

    [SerializeField]
    private int points;

    [SerializeField]
    private int hp;

    private Animator gooAnimator;

    private Vector3 xPlosionOffset;

    private GameObject sfxManager;

    private SFXManager SFXManager;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerController = Player.GetComponent<PlayerController>();

        gooAnimator = GetComponent<Animator>();

        xPlosionOffset = new Vector3(0, 0.5f, 0);

        sfxManager = GameObject.Find("SFXManager");
        SFXManager = sfxManager.GetComponent<SFXManager>();
    }

    private void OnTriggerEnter(Collider other)
    {


        if(other.tag == "Ammo" && hp > 0)
        {
            hp--;
            gooAnimator.SetTrigger("Hit");
            SFXManager.PlaySFX(2);

            if (hp <= 0)
            {
                PlayerController.AddScore(points);

                GameObject xPlosion = ObjectPool.SharedInstance.GetXplosion();
                xPlosion.transform.position = transform.position + xPlosionOffset;
                xPlosion.gameObject.SetActive(true);
                xPlosion.GetComponent<Xplosion>().StartXPlosion();

                SFXManager.PlaySFX(0);
                SFXManager.PlaySFX(1);

                this.gameObject.SetActive(false);
            }
        }

    }
}
