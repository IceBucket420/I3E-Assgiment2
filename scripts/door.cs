using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class door : MonoBehaviour
{
    public Animator animator;
    public Animator animator2;
    public AudioSource doorSound;

    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.GetComponent<PlayerMovement>().Ready == true)
        {
            animator.SetBool("isOpen", true);
            animator2.SetBool("isOpen", true);
            doorSound.Play();
        }
        else
        {
            Debug.Log("You cannot exit. Grab your helmet and gun.");
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.GetComponent<PlayerMovement>().Ready == true)
        {
            animator.SetBool("isOpen", false);
            animator2.SetBool("isOpen", false);
            doorSound.Play();
        }
    }
}
