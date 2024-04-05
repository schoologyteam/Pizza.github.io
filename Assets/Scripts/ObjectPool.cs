using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{


    public static ObjectPool SharedInstance;

    public List<GameObject> ammoList; //List of ammo.
    public GameObject ammo;

    public List<GameObject> xPlosionList;  //List of enemy explosions.
    public GameObject xPlosion;

    public List<GameObject> AllRoads;   //List with all the spawnable roads.

    public int amountToPool;

    private int randomRoad;  //int to randomize roads.

    public float gameTimer;  //checks how long the game has been played


    private void Awake()
    {
        SharedInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {


        ammoList = new List<GameObject>();
        GameObject tmpAmmo;
        for (int i = 0; i < amountToPool; i++)
        {
            tmpAmmo = Instantiate(ammo);
            tmpAmmo.SetActive(false);
            ammoList.Add(tmpAmmo);
        }

        xPlosionList = new List<GameObject>();
        GameObject tmpXplosion;
        for (int i = 0; i < amountToPool; i++)
        {
            tmpXplosion = Instantiate(xPlosion);
            tmpXplosion.SetActive(false);
            xPlosionList.Add(tmpXplosion);
        }

        gameTimer = 0;

    }

    private void Update()
    {
        gameTimer += Time.deltaTime;
    }   

    public GameObject GetRoad()  //Method to get randomized road GameObject.
    {
        

        for (int i = 0; i < AllRoads.Count-1; i++) //Tries to randomize an inactive road;
        {



            if(gameTimer < 90)
            {
                randomRoad = Random.Range(0, 6);

            }

            else if(gameTimer >= 90 && gameTimer < 180)
            {
                randomRoad = Random.Range(0, 12);

            }

            else if(gameTimer >= 180)
            {
                randomRoad = Random.Range(0, AllRoads.Count);

            }

            

            if (!AllRoads[randomRoad].activeInHierarchy)
            {

                return AllRoads[randomRoad];
            }


        }




        for(int i = 0; i < AllRoads.Count-1; i++) //If randomized fails checks for first inactive road that it can find.
        {
            if (!AllRoads[i].activeInHierarchy)
            {

                return AllRoads[i];
            }
        }

        return AllRoads[0];
        

        
    }

    public GameObject GetAmmo()  //Method to get ammo GameObject.
    {


        for (int i = 0; i < amountToPool; i++)
        {
            if (!ammoList[i].activeInHierarchy)
            {


                return ammoList[i];

            }
        }

        return null;
    }

    public GameObject GetXplosion()  //Method to get Xplosion GameObject.
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!xPlosionList[i].activeInHierarchy)
            {


                return xPlosionList[i];

            }
        }

        return null;
    }
}
