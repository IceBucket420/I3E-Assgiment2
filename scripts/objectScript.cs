using UnityEngine;

public class objectScript : MonoBehaviour
{
    public Animator animator;
    public AudioSource CollectedSound;

    public void Collected()
    {
        if (gameObject.tag == "helmet")
        {
            FindObjectOfType<PlayerMovement>().WearingHelmet = true;
            //GetComponent<AudioSource>().Play();
            Destroy(gameObject);
        }
        if (gameObject.tag == "gun")
        {
            FindObjectOfType<PlayerMovement>().HoldingGun = true;
            //GetComponent<AudioSource>().Play();
            Destroy(gameObject);
        }
        if (gameObject.tag == "core")
        {
            animator.SetTrigger("isCollected");
            FindObjectOfType<PlayerMovement>().coreCollected = true;
            //GetComponent<AudioSource>().Play();
        }

    }

    public void DestroyCore()
    {
        Destroy(gameObject);
        CollectedSound.Play();
    }


    public void DestroyProjectiles()
    {
        if (gameObject.tag == "projectiles")
        {
            Destroy(gameObject);
        }
        if (gameObject.tag == "projectiles2")
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            Debug.Log("Ground collision");
            //gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
   
}
