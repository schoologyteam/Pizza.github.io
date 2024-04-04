using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    
    public float playerSpeed;

    [SerializeField]
    private PlayerInputs PlayerInputActions;

    private InputAction Jump;

    private InputAction Shoot;

    private InputAction ChangeSize;

    private Rigidbody rb;

    public float jumpForce;

    private bool grounded;

    private float jumpCount;

    [SerializeField]
    private GameObject ammoSpawner;
    private float reloadTimer;

    [SerializeField]
    private float reloadTime;

    public int amoutOfPizzas;
    private int score;

    private Vector3 og_Gravity;

    private Vector3 fallingGravity;

    private Vector3 yStopper;

    [SerializeField]
    private Text pizzaUI;

    [SerializeField]
    private Text ScoreUI;

    public Animator playerAnimator;

    private Vector3 SetPlayerRotationBackToNormal;
    public GameObject pizzaGuy;

    private float ogPlayerSpeed;

    private GameObject sfxManager;

    private SFXManager SFXManager;

    private bool inBarrel;

    private bool useGravity;

    [SerializeField]
    private float barrelShotMul;

    [SerializeField]
    private ParticleSystem barrelShotEffect;

    [SerializeField]
    private Animator transitionAni;
    private bool hasStarted;

    private void Awake()
    {
        PlayerInputActions = new PlayerInputs();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jumpCount = 0;
        reloadTimer = reloadTime + 1;
        Physics.gravity = new Vector3(0, -9.8f, 0);
        og_Gravity = Physics.gravity;
        fallingGravity = new Vector3(Physics.gravity.x, Physics.gravity.y * 1.5f, Physics.gravity.z);
        yStopper = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        pizzaUI.text = amoutOfPizzas.ToString();
        ScoreUI.text = "Score: 0";
        SetPlayerRotationBackToNormal = new Vector3(0, 90, 0);
        ogPlayerSpeed = playerSpeed;

        sfxManager = GameObject.Find("SFXManager");
        SFXManager = sfxManager.GetComponent<SFXManager>();

        inBarrel = false;

        useGravity = true;

        hasStarted = false;

        StartCoroutine(StartDelay());

    }

    private void OnEnable()
    {
        Jump = PlayerInputActions.Player.Jump;
        Jump.Enable();
        Jump.performed += Jumped;

        Shoot = PlayerInputActions.Player.Shoot;
        Shoot.Enable();
        Shoot.performed += Shooting;

        ChangeSize = PlayerInputActions.Player.ChangeSize;
        ChangeSize.Enable();
        ChangeSize.performed += ChangingSize;

    }

    private void OnDisable()
    {
        Jump.Disable();

        Shoot.Disable();

        ChangeSize.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(playerSpeed * Time.deltaTime, 0, 0, Space.World);
        reloadTimer += Time.deltaTime;

        if(grounded == false && rb.velocity.y <= 0 && useGravity == true)
        {
            Physics.gravity = fallingGravity;
        }

        if(inBarrel == true)
        {
            transform.position = transform.parent.transform.position;
            rb.velocity = Vector3.zero;
        }

    }

    

    private void Jumped(InputAction.CallbackContext context)
    {
        
        if(jumpCount < 2 && grounded == true && inBarrel == false)
        {
            rb.AddForce(new Vector3(0, jumpForce * transform.localScale.y, 0));
            jumpCount++;
            playerAnimator.SetBool("HitGround", false);
            playerAnimator.SetTrigger("Jump");
            SFXManager.PlaySFX(5);

        }

        if(jumpCount < 2 && grounded == false && inBarrel == false)
        {
            rb.velocity = yStopper;
            rb.AddForce(new Vector3(0, jumpForce, 0));
            jumpCount++;
            playerAnimator.SetBool("HitGround", false);
            playerAnimator.SetTrigger("Jump");
            SFXManager.PlaySFX(5);

        }

        if(inBarrel == true)
        {

            StartCoroutine(ShotFromBarrel());
        }
        
    }

    private void Shooting(InputAction.CallbackContext context)
    {
        if(reloadTimer >= reloadTime && inBarrel == false)
        {

            StartCoroutine(Shot());
            
        }
        
        
    }

    private void ChangingSize(InputAction.CallbackContext context)
    {
        if(this.gameObject.transform.localScale.y == 1)
        {
            this.gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }

        else if(this.gameObject.transform.localScale.y < 1)
        {
            this.gameObject.transform.localScale = new Vector3(1, 1, 1);
        }

        SFXManager.PlaySFX(8);
    }

    public void UpdatePizzaUI()
    {
        pizzaUI.text = amoutOfPizzas.ToString();
    }

    private void UpdateScore()
    {
        score += (amoutOfPizzas * amoutOfPizzas) * 100;
        amoutOfPizzas = 0;
        UpdatePizzaUI();
        ScoreUI.text = "Score: " + score.ToString();
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        ScoreUI.text = "Score: " + score.ToString();
    }

    private IEnumerator Shot()
    {
        reloadTimer = 0;
        GameObject Ammo = ObjectPool.SharedInstance.GetAmmo();
        Ammo.transform.position = ammoSpawner.transform.position;
        Ammo.transform.localScale = transform.localScale/8;
        Ammo.SetActive(true);
        SFXManager.PlaySFX(6);
        yield return new WaitForSeconds(1.5f);
        Ammo.SetActive(false);
    }

    private IEnumerator DeathRoutine()
    {
        
        if(hasStarted == true)
        {
            hasStarted = false;
            playerAnimator.SetBool("Death", true);
            SFXManager.PlaySFX(7);
            playerSpeed = 0;
            rb.velocity = Vector3.zero;
            PlayerPrefs.SetInt("CurrentScore", score);

            yield return new WaitForSecondsRealtime(1.5f);
            transitionAni.SetTrigger("End");

            yield return new WaitForSecondsRealtime(2.5f);

            SceneManager.LoadScene("LeaderboardScene", LoadSceneMode.Single);
        }
        
        
        
    }

    private IEnumerator ShotFromBarrel()
    {

        //barrelShotEffect.transform.parent = null;
        barrelShotEffect.Play();

        inBarrel = false;
        rb.AddForce(transform.parent.GetChild(0).up * jumpForce * barrelShotMul);
        transform.parent = null;
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        pizzaGuy.SetActive(true);
        
        jumpCount = 2;
        SFXManager.PlaySFX(11);
        yield return new WaitForSeconds(0.25f);
        useGravity = true;
        Physics.gravity = fallingGravity;
        barrelShotEffect.transform.SetParent(this.gameObject.transform);
        
    }

    private IEnumerator StartDelay()
    {
        Physics.gravity = Vector3.zero;
        rb.useGravity = false;
        playerSpeed = 0;
        transitionAni.SetTrigger("Start");
        playerAnimator.SetTrigger("Jump");
        yield return new WaitForSeconds(1.25f);
        hasStarted = true;
        Physics.gravity = fallingGravity;
        rb.useGravity = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            grounded = true;
            jumpCount = 0;
            Physics.gravity = og_Gravity;
            playerAnimator.ResetTrigger("Jump");
            playerAnimator.SetBool("HitGround", true);

            pizzaGuy.transform.eulerAngles = SetPlayerRotationBackToNormal;
            pizzaGuy.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);

            if(playerSpeed < ogPlayerSpeed && hasStarted == true) 
            {
                playerSpeed = ogPlayerSpeed;
            }


        }

        if (collision.gameObject.tag == "Enemy")
        {

            StartCoroutine(DeathRoutine());
        }

        
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "customer")
        {
            UpdateScore();
        }

        if(other.tag == "Bounce")
        {
            rb.velocity = Vector3.zero;
            jumpCount = 2;
            playerSpeed = 0.33f;
            Physics.gravity = fallingGravity;
            rb.AddForce(new Vector3(0, jumpForce * 1.75f, 0));
            playerAnimator.SetTrigger("Jump");
        }

        if (other.tag == "R_Barrel")
        {

            transform.SetParent(other.transform);
            pizzaGuy.SetActive(false);
            this.gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            inBarrel = true;
            playerSpeed = 0;
            transform.position = other.transform.position;
            Physics.gravity = Vector3.zero;
            useGravity = false;
            grounded = false;
            barrelShotEffect.transform.position = other.transform.GetChild(0).transform.Find("Effect").transform.position;
            barrelShotEffect.transform.SetParent(other.transform.GetChild(0).transform.Find("Effect").transform);
            barrelShotEffect.transform.eulerAngles = Vector3.zero;
            //transform.position = transform.parent.position;
        }

        if (other.tag == "M_Barrel")
        {

            transform.SetParent(other.transform);
            pizzaGuy.SetActive(false);
            this.gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            inBarrel = true;
            playerSpeed = 0;
            transform.position = other.transform.position;
            Physics.gravity = Vector3.zero;
            useGravity = false;
            grounded = false;

            barrelShotEffect.transform.position = transform.parent.Find("Effect").transform.position;
            barrelShotEffect.transform.SetParent(transform.parent.Find("Effect").transform);

        }
    }


}
