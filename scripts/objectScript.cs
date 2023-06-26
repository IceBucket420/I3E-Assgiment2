using UnityEngine;

public class objectScript : MonoBehaviour
{

    public void Collected()
    {
        if (gameObject.tag == "helmet")
        {
            GetComponent<AudioSource>().Play();
            Destroy(gameObject);
        }
        if (gameObject.tag == "gun")
        {
            GetComponent<AudioSource>().Play();
            Destroy(gameObject);
        }

    }

    public void DestroyProjectiles()
    {
        if (gameObject.tag == "projectiles")
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
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
