using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

    private GameObject PlayerFeet;

    private BoxCollider col;

    // Start is called before the first frame update
    void Start()
    {
        PlayerFeet = GameObject.FindGameObjectWithTag("Player").transform.Find("Feet").gameObject;
        col = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerFeet.transform.position.y > this.transform.position.y)
        {
            col.enabled = true;
        }

        else
        {
            col.enabled = false;
        }
    }
}
