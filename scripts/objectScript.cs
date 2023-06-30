using UnityEngine;

public class objectScript : MonoBehaviour
{

    public void Collected()
    {
        if (gameObject.tag == "helmet")
        {
            //GetComponent<AudioSource>().Play();
            Destroy(gameObject);
        }
        if (gameObject.tag == "gun")
        {
            //GetComponent<AudioSource>().Play();
            Destroy(gameObject);
        }

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
