using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{


    public static ObjectPool SharedInstance;

    public List<GameObject> ammoList;
    public GameObject ammo;

    public List<GameObject> xPlosionList;
    public GameObject xPlosion;

    public List<GameObject> EasyRoads1;
    public GameObject EasyRoadObject1;

    public List<GameObject> EasyRoads2;
    public GameObject EasyRoadObject2;

    public List<GameObject> EasyRoads3;
    public GameObject EasyRoadObject3;



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

        EasyRoads3 = new List<GameObject>();
        GameObject tmp4;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp4 = Instantiate(EasyRoadObject3);
            tmp4.SetActive(false);
            EasyRoads3.Add(tmp4);
        }

        xPlosionList = new List<GameObject>();
        GameObject tmp5;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp5 = Instantiate(xPlosion);
            tmp5.SetActive(false);
            xPlosionList.Add(tmp5);
        }

    }

    public GameObject GetEasyRoad1()
    {


        for (int i = 0; i < amountToPool; i++)
        {
            if (!EasyRoads1[i].activeInHierarchy)
            {
                /*if (EasyRoads1[i].transform.Find("pizza"))
                {
                    EasyRoads1[i].transform.Find("pizza").gameObject.SetActive(true);
                }*/

                foreach (Transform child in EasyRoads1[i].transform)
                {
                    if (child.gameObject.CompareTag("Enemy"))
                    {
                        child.gameObject.SetActive(true);
                    }


                }

                foreach (Transform child in EasyRoads1[i].transform)
                {
                    if (child.gameObject.CompareTag("Pizza"))
                    {
                        child.gameObject.SetActive(true);
                    }


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
                foreach (Transform child in EasyRoads2[i].transform)
                {
                    if (child.gameObject.CompareTag("Enemy"))
                    {
                        child.gameObject.SetActive(true);
                    }


                }

                foreach (Transform child in EasyRoads2[i].transform)
                {
                    if (child.gameObject.CompareTag("Pizza"))
                    {
                        child.gameObject.SetActive(true);
                    }


                }

                return EasyRoads2[i];

            }
        }

        return null;
    }


    public GameObject GetEasyRoad3()
    {


        for (int i = 0; i < amountToPool; i++)
        {
            if (!EasyRoads3[i].activeInHierarchy)
            {
                foreach (Transform child in EasyRoads3[i].transform)
                {
                    if (child.gameObject.CompareTag("Enemy"))
                    {
                        child.gameObject.SetActive(true);

                        
                    }


                }

                foreach (Transform child in EasyRoads3[i].transform)
                {
                    if (child.gameObject.CompareTag("Pizza"))
                    {
                        child.gameObject.SetActive(true);
                    }


                }

                return EasyRoads3[i];

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

    public GameObject GetXplosion()
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
