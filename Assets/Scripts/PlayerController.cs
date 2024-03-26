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

    [SerializeField]
    private float jumpForce;

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
        og_Gravity = Physics.gravity;
        fallingGravity = new Vector3(Physics.gravity.x, Physics.gravity.y * 1.5f, Physics.gravity.z);
        yStopper = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        pizzaUI.text = amoutOfPizzas.ToString();
        ScoreUI.text = "Score: 0";
        SetPlayerRotationBackToNormal = new Vector3(0, 90, 0);

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
        transform.Translate(playerSpeed/100, 0, 0, Space.World);
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
            rb.AddForce(new Vector3(0, jumpForce, 0));
            jumpCount++;
            playerAnimator.SetBool("HitGround", false);
            playerAnimator.SetTrigger("Jump");
            //playerAnimator.ResetTrigger("Jump");
            
        }

        if(jumpCount < 2 && grounded == false)
        {
            rb.velocity = yStopper;
            rb.AddForce(new Vector3(0, jumpForce, 0));
            jumpCount++;
            playerAnimator.SetBool("HitGround", false);
            playerAnimator.SetTrigger("Jump");
            
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
    }

    public void UpdatePizzaUI()
    {
        pizzaUI.text = amoutOfPizzas.ToString();
    }

    private void UpdateScore()
    {
        score += amoutOfPizzas * 100;
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
        Ammo.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        Ammo.SetActive(false);
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


        }

        if (collision.gameObject.tag == "Enemy")
        {
            SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
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
    }


}
