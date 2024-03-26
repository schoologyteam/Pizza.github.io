using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{


    private Transform Player;

    private float speed;

    [SerializeField]
    private GameObject TestRoad;

    private Vector3 startPoint;

    private bool isSpawning;

    private int rand;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        speed = Player.GetComponent<PlayerController>().playerSpeed / 100;
        startPoint = this.gameObject.transform.position;
        isSpawning = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed, 0, 0, Space.World);

        if(Vector3.Distance(startPoint, transform.position) > 20 && isSpawning == false)
        {
            SpawnRoad();
        }
    }


    private void SpawnRoad()
    {

        rand = Random.Range(1, 3);

        if(rand == 1)
        {
            isSpawning = true;
            GameObject Road = ObjectPool.SharedInstance.GetEasyRoad1();
            Road.transform.position = transform.position;
            Road.SetActive(true);
            startPoint = this.transform.position;
            isSpawning = false;
        }

        else
        {
            isSpawning = true;
            GameObject Road = ObjectPool.SharedInstance.GetEasyRoad2();
            Road.transform.position = transform.position;
            Road.SetActive(true);
            startPoint = this.transform.position;
            isSpawning = false;
        }

        

    }
}
