/*
 * Author: Pang Le Xin 
 * Date: 27/06/2023
 * Description: Script for object functions
 */

using UnityEngine;

public class objectScript : MonoBehaviour
{
    /// <summary>
    /// Animator for the crystal animation
    /// </summary>
    public Animator animator;
    /// <summary>
    /// AudioSource for the sound of the crystal collection
    /// </summary>
    public AudioSource CollectedSound;

    /// <summary>
    ///  For destroying and a function that would check if the item is collected
    /// </summary>
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
    /// <summary>
    /// For the animation event for the crystal core
    /// </summary>
    public void DestroyCore() 
    {
        Destroy(gameObject);
        CollectedSound.Play();
    }

    /// <summary>
    /// Destroy the projectiles shot by enemies, destroy them from the scene
    /// </summary>
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
    /// <summary>
    /// When projectiles hit the ground instead of the player, it would still be destroyed.
    /// </summary>
    /// <param name="collision"></param>
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
