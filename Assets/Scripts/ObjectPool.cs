using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{


    public static ObjectPool SharedInstance;

    public List<GameObject> ammoList;
    public GameObject ammo;

    public List<GameObject> EasyRoads1;
    public GameObject EasyRoadObject1;

    public List<GameObject> EasyRoads2;
    public GameObject EasyRoadObject2;

    public int amountToPool;


    private void Awake()
    {
        SharedInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        

        EasyRoads1 = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(EasyRoadObject1);
            tmp.SetActive(false);
            EasyRoads1.Add(tmp);
        }


        ammoList = new List<GameObject>();
        GameObject tmp2;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp2 = Instantiate(ammo);
            tmp2.SetActive(false);
            ammoList.Add(tmp2);
        }

        EasyRoads2 = new List<GameObject>();
        GameObject tmp3;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp3 = Instantiate(EasyRoadObject2);
            tmp3.SetActive(false);
            EasyRoads2.Add(tmp3);
        }

    }

    public GameObject GetEasyRoad1()
    {


        for (int i = 0; i < amountToPool; i++)
        {
            if (!EasyRoads1[i].activeInHierarchy)
            {
                if (EasyRoads1[i].transform.Find("pizza"))
                {
                    EasyRoads1[i].transform.Find("pizza").gameObject.SetActive(true);
                }
                
                return EasyRoads1[i];

            }
        }
        
        return null;
    }

    public GameObject GetEasyRoad2()
    {


        for (int i = 0; i < amountToPool; i++)
        {
            if (!EasyRoads2[i].activeInHierarchy)
            {
                if (EasyRoads2[i].transform.Find("pizza"))
                {
                    EasyRoads2[i].transform.Find("pizza").gameObject.SetActive(true);
                }

                return EasyRoads2[i];

            }
        }

        return null;
    }

    public GameObject GetAmmo()
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
}
