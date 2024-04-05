using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{


    private Transform Player;

    private Vector3 startPoint;

    private Vector3 ToAdd;  //The Amount added in x-axis when spawning new road.

    private bool isSpawning;  //Bool to check if spawning.
    

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        startPoint = this.gameObject.transform.position;
        ToAdd = new Vector3(20, 0, 0);
        isSpawning = false;
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = new Vector3(Player.position.x + 30, transform.position.y, transform.position.z);

        if(Vector3.Distance(startPoint, transform.position) > 20 && isSpawning == false)
        {
            SpawnRoad();
        }


    }


    private void SpawnRoad() //Method to spawn road.
    {
        isSpawning = true;
        GameObject Road = ObjectPool.SharedInstance.GetRoad();
        Road.transform.position = startPoint + ToAdd;
        Road.SetActive(true);
        startPoint = startPoint + ToAdd;
        isSpawning = false;


    }
}
