using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    Vector3 movementInput = Vector3.zero;
    float movementSpeed = 0.07f;
    Vector3 rotationInput = Vector3.zero;
    float rotationSpeed = 1f;
    Vector3 headRotationInput = Vector3.zero;
    public float jumpStrenght = 5f;



    public AudioSource bgm;
    public float Health = 100;
    float timerVal = 0;
    public float sprintModifier = 0.1f;
    private bool isGrounded = false;
    bool mouseclick = false;

    public  GameObject playerCamera;
    public Transform head;


    private void OnCollisionStay(Collision collision)

    {
        isGrounded = true;
        //Debug.Log("Im grounded");// to test if player is grounded
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Monkey")// Allows player to be damaged once Enemy approaches player
        {
            Health -= 1;
            Debug.Log("player health:" + Health);
        }
    }

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


        void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Health > 0)
        {
 
            if (Input.GetKey(KeyCode.LeftShift))
            {
                // Set current speed to run if shift is down
                movementSpeed = sprintModifier;
            }
            else
            {
                movementSpeed = 0.07f;
            }

            Debug.DrawLine(head.transform.position, head.transform.position + (head.transform.forward * 5f));
            RaycastHit hitInfo;
            if (Physics.Raycast(head.transform.position,
                head.transform.forward, out hitInfo, 10f))
            {
                if (hitInfo.transform.tag == "Monkey" && mouseclick)
                {
                    Debug.Log("raycast hit: " + hitInfo.transform.gameObject.name);
                    hitInfo.transform.GetComponent<EnemyScript>().Hurt();
                }
            }
            mouseclick = false;

            Vector3 forwardDir = transform.forward;
            forwardDir *= movementInput.y;

            Vector3 rightDir = transform.right;
            rightDir *= movementInput.x;

            GetComponent<Rigidbody>().MovePosition(transform.position + (forwardDir + rightDir) * movementSpeed);
            //transform.position += (forwardDir + rightDir) * movementSpeed;


            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + rotationInput * rotationSpeed);

            var headRot = playerCamera.transform.rotation.eulerAngles
                + headRotationInput * rotationSpeed;

            //limitations for the player camera
            headRotationInput.x -= rotationInput.y; 
            headRotationInput.x= Mathf.Clamp(headRotationInput.x, -45f, 45f);
            

            playerCamera.transform.rotation = Quaternion.Euler(headRot);

            isGrounded = false;
        }
        else
        {

        }
    }
}
