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


    // Start is called before the first frame update
    void Start()
    {

        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerController = Player.GetComponent<PlayerController>();


        ogScale = transform.localScale;
        ScaleTo = ogScale * size;

        transform.DOScale(ScaleTo, length).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            this.gameObject.SetActive(false);
            PlayerController.amoutOfPizzas++;
            PlayerController.UpdatePizzaUI();
        }
    }
}
