using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{
    public Animator animator;
    public Animator animator2;
    public AudioSource doorSound;

    private void OnTriggerEnter(Collider other)
    {
        animator.SetBool("isOpen", true);
        animator2.SetBool("isOpen", true);
        doorSound.Play();
    }

    private void OnTriggerExit(Collider other)
    {
        animator.SetBool("isOpen", false);
        animator2.SetBool("isOpen", false);
        doorSound.Play();
    }
}
