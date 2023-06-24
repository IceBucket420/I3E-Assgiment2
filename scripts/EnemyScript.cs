using UnityEngine;


public class EnemyScript : MonoBehaviour
{
    public Transform visionPoint;
    private PlayerMovement player;

    public GameObject Monkey;

    public Transform Player;

    //set the vision and movement speed  of the enemy 
    public float visionAngle = 30f;
    public float visionDistance = 10f;
    public float moveSpeed = 2f;
    public float chaseDistance = 3f;

    private Vector3? lastKnownPlayerPosition;

    //set health of enemies
    public float MonkeyHealth = 3;
    public float PlayerAttack = 2; // the damage the player deals to the enemy


    // Start is called before the first frame update
    void Start()
    {
        // lets enemy identify the player
        player = GameObject.FindObjectOfType<PlayerMovement>();
    }

    private void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookAt = Player.position;
        lookAt.y = transform.position.y;
        transform.LookAt(lookAt); // allows enemy to roatate and face player


        //Let the enemy move towards the player
        Monkey.transform.position = Vector3
                .MoveTowards(transform.position, Player.position, moveSpeed * Time.deltaTime);

    }
    void Look()
    {
        Vector3 deltaToPlayer = player.transform.position - visionPoint.position;
        Vector3 directionToPlayer = deltaToPlayer.normalized;

        float dot = Vector3.Dot(transform.forward, directionToPlayer);

        if (dot < 0)
        {
            return;
        }

        float distanceToPlayer = directionToPlayer.magnitude;

        if (distanceToPlayer > visionDistance)
        {
            return;
        }

        float angle = Vector3.Angle(transform.forward, directionToPlayer);

        if (angle > visionAngle)
        {
            return;
        }



        RaycastHit hit;
        if (Physics.Raycast(transform.position, directionToPlayer, out hit, visionDistance))
        {
            if (hit.collider.gameObject == player.gameObject)
            {
                lastKnownPlayerPosition = player.transform.position;
            }
        }
    }

    public void Hurt()
    {
        //enemy gets hurt when this function is called
        MonkeyHealth -= PlayerAttack;
        Debug.Log(" monkey health: " + MonkeyHealth);

        if (MonkeyHealth <= 0)
        {
            Destroy(this.gameObject); // destroy enemy when player kills it
        }
    }
}
