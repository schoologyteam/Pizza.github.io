using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Pizza : MonoBehaviour
{


    private Vector3 ogScale;
    private Vector3 ScaleTo;

    [SerializeField]
    private float size;

    [SerializeField]
    private float length;

    private GameObject Player;

    public PlayerController PlayerController { get; private set; }

    private GameObject sfxManager;

    private SFXManager SFXManager;


    // Start is called before the first frame update
    void Start()
    {

        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerController = Player.GetComponent<PlayerController>();


        ogScale = transform.localScale;
        ScaleTo = ogScale * size;

        transform.DOScale(ScaleTo, length).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);

        sfxManager = GameObject.Find("SFXManager");
        SFXManager = sfxManager.GetComponent<SFXManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            SFXManager.PlaySFX(3);
            PlayerController.amoutOfPizzas++;
            PlayerController.UpdatePizzaUI();
            this.gameObject.SetActive(false);
        }
    }
}
