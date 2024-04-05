using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Pizza : MonoBehaviour
{


    private Vector3 ogScale; //Original scale of the object
    private Vector3 ScaleTo; //End value where the object scales to

    [SerializeField]
    private float size;  //Size to scale to

    [SerializeField]
    private float length; //How long the Yoyo effect takes

    private GameObject Player;

    public PlayerController PlayerController { get; private set; }  //Player Controller Script

    private GameObject sfxManager;  //Sound Effect Manager Object

    private SFXManager SFXManager;  //Sound Effect Manager Script


    // Start is called before the first frame update
    void Start()
    {

        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerController = Player.GetComponent<PlayerController>();


        ogScale = transform.localScale;
        ScaleTo = ogScale * size;

        //DoTween Scaling starts
        transform.DOScale(ScaleTo, length).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);

        sfxManager = GameObject.Find("SFXManager");
        SFXManager = sfxManager.GetComponent<SFXManager>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            SFXManager.PlaySFX(3);
            PlayerController.amoutOfPizzas++; //Adds Pizza to player
            PlayerController.UpdatePizzaUI(); //Updates Pizzas amount in the UI
            gameObject.SetActive(false);
        }
    }
}
