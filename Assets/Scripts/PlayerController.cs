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

    public Leaderboard leaderboard;

    private float ogPlayerSpeed;

    private GameObject sfxManager;

    private SFXManager SFXManager;

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

        if(grounded == false && rb.velocity.y <= 0)
        {
            Physics.gravity = fallingGravity;
        }

    }

    

    private void Jumped(InputAction.CallbackContext context)
    {
        
        if(jumpCount < 2 && grounded == true)
        {
            rb.AddForce(new Vector3(0, jumpForce * transform.localScale.y, 0));
            jumpCount++;
            playerAnimator.SetBool("HitGround", false);
            playerAnimator.SetTrigger("Jump");
            SFXManager.PlaySFX(5);

        }

        if(jumpCount < 2 && grounded == false)
        {
            rb.velocity = yStopper;
            rb.AddForce(new Vector3(0, jumpForce, 0));
            jumpCount++;
            playerAnimator.SetBool("HitGround", false);
            playerAnimator.SetTrigger("Jump");
            SFXManager.PlaySFX(5);

        }
        
    }

    private void Shooting(InputAction.CallbackContext context)
    {
        if(reloadTimer >= reloadTime)
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

        else if(this.gameObject.transform.localScale.y == 0.5f)
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
        //Time.timeScale = 0;
        playerAnimator.SetBool("Death", true);
        SFXManager.PlaySFX(7);
        playerSpeed = 0;
        PlayerPrefs.SetInt("CurrentScore", score);
        yield return new WaitForSecondsRealtime(3);
        //yield return leaderboard.SubmitScore(score);
        //Time.timeScale = 1;
        SceneManager.LoadScene("LeaderboardScene", LoadSceneMode.Single);
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

            if(playerSpeed < ogPlayerSpeed) 
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
    }


}
