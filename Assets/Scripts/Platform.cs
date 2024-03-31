using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

    private GameObject PlayerFeet;

    private Rigidbody player_rb;

    private BoxCollider col;

    // Start is called before the first frame update
    void Start()
    {
        PlayerFeet = GameObject.FindGameObjectWithTag("Player").transform.Find("Feet").gameObject;
        player_rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        col = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerFeet.transform.position.y > this.transform.position.y && player_rb.velocity.y < 0)
        {
            col.enabled = true;
        }

        else
        {
            col.enabled = false;
        }
    }
}
