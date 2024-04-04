using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooNinja : MonoBehaviour
{

    private GameObject Player;

    [SerializeField]
    private int maxDistance;

    [SerializeField]
    private int points;

    public PlayerController PlayerController { get; private set; }

    private Vector3 xPlosionOffset;

    private GameObject sfxManager;

    private SFXManager SFXManager;

    [SerializeField]
    private GameObject mesh;

    [SerializeField]
    private ParticleSystem effect;

    private BoxCollider col;
    private bool hitIsActive;

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

    private RaycastHit Hit()
    {

        Ray ray = new Ray(transform.position + offset, transform.forward );

        RaycastHit hit;



        Vector3 endPos = transform.position + (maxDistance * transform.forward) + offset;

        Debug.DrawLine(transform.position + offset, endPos, Color.cyan);

        if (Physics.Raycast(ray, out hit, maxDistance))
        {

            endPos = hit.point;

            //Debug.DrawLine(transform.position, endPos, Color.cyan);

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

    private IEnumerator ActivateMesh()
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

            //hitIsActive = false;

            gameObject.SetActive(false);
        }
    }
}
