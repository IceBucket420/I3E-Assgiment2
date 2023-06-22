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


    bool hit = false;

    public  GameObject playerCamera;

    private void OnCollisionStay(Collision collision)

    {
        isGrounded = true;
        //Debug.Log("Im grounded");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "collectables")
        {
            //score += 3;
           // Debug.Log("Enter : " + collision.gameObject.name);
           // Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Coin")
        {
            //collision.gameObject.GetComponent<coinScript>().Collected();
        }

        if (collision.gameObject.tag == "Damage")
        {
            //collision.gameObject.GetComponent<playerDead>().Damaged();
            //hit = true;
        }
    }


    //private void OnTriggerEnter(Collider collider)
    //{
    //   if (collider.gameObject.tag == "triggerbox")
    //  {
    //       Debug.Log("Enter Trigger:")
    //   }
    //}

    void OnFire()
    {

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


        if (hit == false)
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

            Vector3 forwardDir = transform.forward;
            forwardDir *= movementInput.y;

            Vector3 rightDir = transform.right;
            rightDir *= movementInput.x;

            GetComponent<Rigidbody>().MovePosition(transform.position + (forwardDir + rightDir) * movementSpeed);
            //transform.position += (forwardDir + rightDir) * movementSpeed;


            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + rotationInput * rotationSpeed);

            var headRot = playerCamera.transform.rotation.eulerAngles
                + headRotationInput * rotationSpeed;

            headRotationInput.x -= rotationInput.y;
            headRotationInput.x= Mathf.Clamp(headRotationInput.x, -90f, 90f);
            

            playerCamera.transform.rotation = Quaternion.Euler(headRot);

            isGrounded = false;
        }
    }
}
