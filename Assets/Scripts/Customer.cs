using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{

    private Animator customerAnimator;

    private GameObject Player;

    public PlayerController PlayerController { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        customerAnimator = GetComponent<Animator>();

        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerController = Player.GetComponent<PlayerController>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player" && PlayerController.amoutOfPizzas > 0)
        {
            customerAnimator.SetTrigger("celebrate");
            Debug.Log("Here");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && PlayerController.amoutOfPizzas > 0)
        {
            customerAnimator.SetTrigger("celebrate");
            Debug.Log("Here2");
        }
    }


}
