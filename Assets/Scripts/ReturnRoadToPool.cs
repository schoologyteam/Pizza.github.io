using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnRoadToPool : MonoBehaviour
{

    private Transform Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(Player.transform.position, transform.position) > 30 && transform.position.x < Player.transform.position.x)
        {


            foreach (Transform child in transform)
            {
                if (child.gameObject.CompareTag("Enemy") && Vector3.Distance(Player.transform.position, transform.position) > 30)
                {
                    child.gameObject.SetActive(true);
                    //Debug.Log("here3");

                }


            }

            foreach (Transform child in transform)
            {
                if (child.gameObject.CompareTag("Pizza") && Vector3.Distance(Player.transform.position, transform.position) > 30)
                {
                    child.gameObject.SetActive(true);
                    Debug.Log("here4");
                }


            }


            this.gameObject.SetActive(false);
        }
    }
}
