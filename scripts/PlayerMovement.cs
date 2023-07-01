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

    Vector3 movementInput = Vector3.zero;
    float movementSpeed = 0.07f;
    Vector3 rotationInput = Vector3.zero;
    float rotationSpeed = 1f;
    Vector3 headRotationInput = Vector3.zero;
    public float jumpStrenght = 5f;


    public int maxHealth = 100;
    public int currentHealth;
    public float sprintModifier = 0.1f;
    private bool isGrounded = false;
    bool mouseclick = false;
    public int CurrentScene;

    //Item collection
    public bool WearingHelmet = false;
    public bool HoldingGun = false;
    public bool Ready = false;
    public bool coreCollected = false;
    public bool canCollect = false;

    public GameObject playerCamera;
    public Transform head;
    public TextMeshProUGUI HealthDisplay;
    public GameObject DeathMenu;
    public GameObject playerPanel;
    public AudioSource walkingSound;
    public AudioSource DeathSound;
    public GameObject Core;
    public Animator transition;
    public float transitionTime = 1f;


    public HealthBar healthBar;
    private void OnCollisionStay(Collision collision)

    {
        isGrounded = true;
        //Debug.Log("Im grounded");// to test if player is grounded
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Monkey")// Allows player to be damaged once Enemy approaches player
        {
            currentHealth -= 3;
            Debug.Log("player health:" + currentHealth);
            HealthDisplay.text = currentHealth.ToString();
            healthBar.SetHealth(currentHealth);
        }

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

    void OnFire()
    {
        mouseclick = true;
    }
    void OnLook(InputValue value)
    {
        rotationInput.y = value.Get<Vector2>().x;
        headRotationInput.x = -value.Get<Vector2>().y;
    }
    // Start is called before the first frame update
    void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();

    }
    void OnJump()  //space to jump
    {
        if (isGrounded == true)
        {
            GetComponent<Rigidbody>().AddForce
            (Vector3.up * jumpStrenght, ForceMode.Impulse); //Lets player jump
        }
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }


    private void Awake()
    {

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        DeathMenu.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        HealthDisplay.text = currentHealth.ToString();
        // Update is called once per frame
    }
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

        //void PlayerDied()
        //{
        //    GameManager.instance.GameOver();
        //    Destroy(gameObject);
        //}
    }
}


