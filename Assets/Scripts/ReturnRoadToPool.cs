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
            this.gameObject.SetActive(false);
        }
    }
}
