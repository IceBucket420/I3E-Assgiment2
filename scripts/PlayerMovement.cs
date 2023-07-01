/*
 * Author: Pang Le Xin 
 * Date: 20/06/2023
 * Description: Controls the player actions, inputs, raycast and player movement. Keep tracks of the player health as well
 */


using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;


public class PlayerMovement : MonoBehaviour
{
    /// <summary>
    /// movement Input 
    /// </summary>
    Vector3 movementInput = Vector3.zero;
    /// <summary>
    /// Movement Speed
    /// </summary>
    float movementSpeed = 0.07f;
    /// <summary>
    /// Rotation input
    /// </summary>
    Vector3 rotationInput = Vector3.zero;
    /// <summary>
    /// Rotation Speed
    /// </summary>
    float rotationSpeed = 1f;
    /// <summary>
    /// Roatation of camera Input
    /// </summary>
    Vector3 headRotationInput = Vector3.zero;
    /// <summary>
    /// Jump Streght
    /// </summary>
    public float jumpStrenght = 5f;

    /// <summary>
    /// Maximum Health of player
    /// </summary>
    public int maxHealth = 100;
    /// <summary>
    /// current health of player
    /// </summary>
    public int currentHealth;
    /// <summary>
    /// Sprint speed
    /// </summary>
    public float sprintModifier = 0.09f;
    /// <summary>
    /// bool that check if player is on ground
    /// </summary>
    private bool isGrounded = false;
    /// <summary>
    /// bool checks for mouse clicks
    /// </summary>
    bool mouseclick = false;
    /// <summary>
    /// Current scene interger
    /// </summary>
    public int CurrentScene;

    //Item collection

    /// <summary>
    /// Check if player is Wearing Helmet
    /// </summary>
    public bool WearingHelmet = false;
    /// <summary>
    /// Check if player is holding gun
    /// </summary>
    public bool HoldingGun = false;
    /// <summary>
    /// Check id player is ready with the helmet and gun
    /// </summary>
    public bool Ready = false;
    /// <summary>
    /// Check if player collected core
    /// </summary>
    public bool coreCollected = false;
    /// <summary>
    /// Check if player can collect the core
    /// </summary>
    public bool canCollect = false;

    /// <summary>
    /// GameObject that find player camera
    /// </summary>
    public GameObject playerCamera;
    /// <summary>
    /// Head of the player aka camera
    /// </summary>
    public Transform head;
    /// <summary>
    /// TextMesh of player health
    /// </summary>
    public TextMeshProUGUI HealthDisplay;
    /// <summary>
    /// Game object Death Menu canvas for player
    /// </summary>
    public GameObject DeathMenu;
    /// <summary>
    /// Game object Player panel canvas for player
    /// </summary>
    public GameObject playerPanel;
    /// <summary>
    /// Audiosource of player walking sound
    /// </summary>
    public AudioSource walkingSound;
    /// <summary>
    /// Audiosource of player Death Sound
    /// </summary>
    public AudioSource DeathSound;
    /// <summary>
    /// Game object of the crystal core
    /// </summary>
    public GameObject Core;
    /// <summary>
    /// Animator of the fade transitiom
    /// </summary>
    public Animator transition;
    /// <summary>
    /// float of transtion time 
    /// </summary>
    public float transitionTime = 1f;

    /// <summary>
    /// Reference script Healthbar
    /// </summary>
    public HealthBar healthBar;

    /// <summary>
    /// Check if player is on the ground
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionStay(Collision collision)

    {
        isGrounded = true;
        //Debug.Log("Im grounded");// to test if player is grounded
    }

    /// <summary>
    /// If player collided with enemies or projectiles player loses health
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "boss")// Allows player to be damaged once Enemy approaches player
        {
            currentHealth -= 5;
            Debug.Log("player health:" + currentHealth);
            HealthDisplay.text = currentHealth.ToString();
            healthBar.SetHealth(currentHealth);
        }


        if (collision.gameObject.tag == "projectiles")
        {

            Debug.Log("Ouch");
            currentHealth -= 2;
            HealthDisplay.text = currentHealth.ToString();
            collision.gameObject.GetComponent<objectScript>().DestroyProjectiles();
            healthBar.SetHealth(currentHealth);
        }

        if (collision.gameObject.tag == "projectiles2")
        {

            Debug.Log("Ouch");
            currentHealth -= 5;
            HealthDisplay.text = currentHealth.ToString();
            collision.gameObject.GetComponent<objectScript>().DestroyProjectiles();
            healthBar.SetHealth(currentHealth);
        }

        if (collision.gameObject.tag == "die")
        {
            Destroy(gameObject);
        }
    }

    //public void OnTriggerEnter(Collider collision)
    //{
    //    if (collision.gameObject.tag == "Teleporter 1")
    //    {
    //        Debug.Log("Teleport to scene");
    //        CurrentScene = SceneManager.GetActiveScene().buildIndex;
    //        SceneManager.LoadScene(CurrentScene + 1);

    //    }
    //    else
    //    {
    //        Debug.Log("Im not ready");
    //    }
    //}

    /// <summary>
    /// player input actions that tracks player mouseclick
    /// </summary>
    void OnFire()
    {
        mouseclick = true;
    }
    /// <summary>
    /// player input actions that tracks player rotation 
    /// </summary>
    void OnLook(InputValue value)
    {
        rotationInput.y = value.Get<Vector2>().x;
        headRotationInput.x = -value.Get<Vector2>().y;
    }
    /// <summary>
    /// player input actions that tracks player movement
    /// </summary>
    void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();

    }
    /// <summary>
    /// player input actions that tracks player spacebar
    /// </summary>
    void OnJump()  //space to jump
    {
        if (isGrounded == true)
        {
            GetComponent<Rigidbody>().AddForce
            (Vector3.up * jumpStrenght, ForceMode.Impulse); //Lets player jump
        }
    }

    /// <summary>
    /// Give time for the transition of the scenes
    /// </summary>
    /// <param name="levelIndex"></param>
    /// <returns></returns>
    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }

    /// <summary>
    /// Awake function
    /// </summary>
    private void Awake()
    {

        DontDestroyOnLoad(gameObject);
    }
    /// <summary>
    /// Start function
    /// </summary>
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        DeathMenu.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        HealthDisplay.text = currentHealth.ToString();
        // Update is called once per frame
    }

    /// <summary>
    /// update function
    /// </summary>
    void Update()
    {
        if (currentHealth > 0)
        {
            CurrentScene = SceneManager.GetActiveScene().buildIndex;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                movementSpeed = sprintModifier;
            }
            else
            {
                movementSpeed = 0.07f;
            }


            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                walkingSound.enabled = true;
            }

            else
            {
                walkingSound.enabled = false;
            }

            Debug.DrawLine(head.transform.position, head.transform.position + (head.transform.forward * 5f));
            RaycastHit hitInfo;
            if (Physics.Raycast(head.transform.position,
                head.transform.forward, out hitInfo, 15f))
            {
                // Player hits enemies with raycast and enemies take damage
                if (hitInfo.transform.tag == "Ranger" && mouseclick)
                {
                    Debug.Log("raycast hit: " + hitInfo.transform.gameObject.name);
                    hitInfo.transform.GetComponent<EnemyAI>().Hurt();// Gets the enemyscript, and calls the functions with reduced the health of enemies
                }

                if (hitInfo.transform.tag == "helmet" && mouseclick)
                {
                    Debug.Log("raycast hit: " + hitInfo.transform.gameObject.name);
                    hitInfo.transform.GetComponent<objectScript>().Collected(); // calls destroy function
                }

                if (hitInfo.transform.tag == "gun" && mouseclick)
                {
                    Debug.Log("raycast hit: " + hitInfo.transform.gameObject.name);
                    hitInfo.transform.GetComponent<objectScript>().Collected(); // Gets the enemyscript, and calls the functions with reduced the health of enemies
                }
                if (hitInfo.transform.tag == "door" && mouseclick && Ready == true)
                {
                    Debug.Log("raycast hit: " + hitInfo.transform.gameObject.name);
                    Debug.Log("Teleport to scene");
                    CurrentScene = SceneManager.GetActiveScene().buildIndex;
                    StartCoroutine(LoadLevel(CurrentScene + 1));
                }

                if (hitInfo.transform.tag == "Teleport" && mouseclick)
                {
                    Debug.Log("raycast hit: " + hitInfo.transform.gameObject.name);
                    Debug.Log("Teleport to scene");
                    CurrentScene = SceneManager.GetActiveScene().buildIndex;
                    StartCoroutine(LoadLevel(CurrentScene + 1));
                }

                if (hitInfo.transform.tag == "Teleporter 1" && mouseclick && coreCollected == true)
                {
                    Debug.Log("raycast hit: " + hitInfo.transform.gameObject.name);
                    Debug.Log("Teleport to scene");
                    StartCoroutine(LoadLevel(CurrentScene+1));
                }

                if (hitInfo.transform.tag == "boss" && mouseclick)
                {
                    Debug.Log("raycast hit: " + hitInfo.transform.gameObject.name);
                    hitInfo.transform.GetComponent<EnemyAI>().Hurt(); // Gets the enemyscript, and calls the functions with reduced the health of enemies
                }

                if (hitInfo.transform.tag == "core" && mouseclick && canCollect == true)
                {
                    Debug.Log("raycast hit: " + hitInfo.transform.gameObject.name);
                    hitInfo.transform.GetComponent<objectScript>().Collected(); // Gets the enemyscript, and calls the functions with reduced the health of enemies
                }
                if (hitInfo.transform.tag == "reactor" && mouseclick)
                {
                    Debug.Log("raycast hit: " + hitInfo.transform.gameObject.name);
                    Debug.Log("Teleport to scene");
                    StartCoroutine(LoadLevel(CurrentScene + 1));
                   // Destroy(gameObject);
                }

            }


            if (HoldingGun == true && WearingHelmet == true)
            {
                Ready = true;
                //Debug.Log("Im ready");
            }

            mouseclick = false; // detect if player clicked


            // Player movement 
            Vector3 forwardDir = transform.forward;
            forwardDir *= movementInput.y;

            Vector3 rightDir = transform.right;
            rightDir *= movementInput.x;

            GetComponent<Rigidbody>().MovePosition(transform.position + (forwardDir + rightDir) * movementSpeed);
            //transform.position += (forwardDir + rightDir) * movementSpeed;




            // FOr player to look left and right
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + rotationInput * rotationSpeed);


            // For Player to look up and down
            var headRot = playerCamera.transform.rotation.eulerAngles
                + headRotationInput * rotationSpeed;

            //limitations for the player camera
            headRotationInput.x -= rotationInput.y;
            headRotationInput.x = Mathf.Clamp(headRotationInput.x, -45f, 45f);


            playerCamera.transform.rotation = Quaternion.Euler(headRot);

            isGrounded = false;

            
        }

        else
        {
            walkingSound.enabled = false;
            //DeathSound.enabled = true;
            Debug.Log("Im Dead");
            //Destroy(gameObject);
            //PlayerDied();
            CurrentScene = SceneManager.GetActiveScene().buildIndex;
            //SceneManager.LoadScene(6);
            //currentHealth = 50;
            DeathMenu.SetActive(true);
            //FindObjectOfType<EnemyAI>().DestroyEnemies();
            playerPanel.SetActive(false);
        }

    }
}


