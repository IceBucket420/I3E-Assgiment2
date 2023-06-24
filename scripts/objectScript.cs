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
