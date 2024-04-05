using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooNinja : MonoBehaviour
{

    private GameObject Player;

    [SerializeField]
    private int maxDistance;  //Max Distance where Enemy can "See"

    [SerializeField]
    private int points;    //Score to add when destroyed by player

    public PlayerController PlayerController { get; private set; }   //Player Controller Script

    private Vector3 xPlosionOffset;

    private GameObject sfxManager;   //Sound Effect Manager Object

    private SFXManager SFXManager;   //Sound Effect Manager Script

    [SerializeField]
    private GameObject mesh;  //Object that holds the enemies meshes

    [SerializeField]
    private ParticleSystem effect;  //Particle effect when Enemy appears

    private BoxCollider col;
    private bool hitIsActive;  //Bool to check if RayCast Hits Player

    [SerializeField]
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerController = Player.GetComponent<PlayerController>();

        xPlosionOffset = new Vector3(0, 0.5f, 0);

        sfxManager = GameObject.Find("SFXManager");
        SFXManager = sfxManager.GetComponent<SFXManager>();

        mesh.SetActive(false);

        col = GetComponent<BoxCollider>();
        col.enabled = false;

        hitIsActive = false;

        
    }

    // Update is called once per frame
    void Update()
    {
        Hit();
    }

    private RaycastHit Hit()   //Checks if player is seen.
    {

        Ray ray = new Ray(transform.position + offset, transform.forward );

        RaycastHit hit;



        Vector3 endPos = transform.position + (maxDistance * transform.forward) + offset;

        Debug.DrawLine(transform.position + offset, endPos, Color.cyan);

        if (Physics.Raycast(ray, out hit, maxDistance))
        {

            endPos = hit.point;

            if (Physics.Raycast(ray, out hit, maxDistance))
            {
                endPos = hit.point;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.tag == "Player" && hitIsActive == false)
                    {
                        StartCoroutine(ActivateMesh());
                    }
                }
            }


        }

        return hit;
    }

    private IEnumerator ActivateMesh() //Couroutine to make Enemy appear
    {
        hitIsActive = true;
        SFXManager.PlaySFX(10);
        effect.Play();
        yield return new WaitForSeconds(0.5f);
        mesh.SetActive(true);
        col.enabled = true;
        yield return new WaitForSeconds(5f);
        col.enabled = false;
        if (mesh.activeInHierarchy == true)
        {
            mesh.SetActive(false);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ammo")
        {
            PlayerController.AddScore(points);

            col.enabled = false;
            if (mesh.activeInHierarchy == true)
            {
                mesh.SetActive(false);
            }

            GameObject xPlosion = ObjectPool.SharedInstance.GetXplosion();
            xPlosion.transform.position = transform.position + xPlosionOffset;
            xPlosion.gameObject.SetActive(true);
            xPlosion.GetComponent<Xplosion>().StartXPlosion();

            SFXManager.PlaySFX(0);
            SFXManager.PlaySFX(1);

            gameObject.SetActive(false);
        }
    }
}
