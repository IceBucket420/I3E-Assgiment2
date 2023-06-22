using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Transform visionPoint;
    private PlayerMovement player;

    public Transform Player;

    //set the vision and movement speed  of the enemy 
    public float visionAngle = 30f;
    public float visionDistance = 10f;
    public float moveSpeed = 2f;
    public float chaseDistance = 3f;

    private Vector3? lastKnownPlayerPosition;

    //set health of enemies
    public float MonkeyHealth = 3;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookAt = Player.position;
        lookAt.y = transform.position.y;
        transform.LookAt(lookAt);
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
        MonkeyHealth -= 1;
        if (MonkeyHealth == 0)
        {
            Destroy(this.gameObject);
        }
    }
}
