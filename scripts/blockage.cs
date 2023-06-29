using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockage : MonoBehaviour
{

    public Animator animator;
    public AudioSource crashSound;
    private void OnTriggerEnter(Collider other)
    {
        animator.SetBool("isPresent", true);
        crashSound.Play();
    }
}
