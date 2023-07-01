
/*
 * Author: Pang Le Xin 
 * Date: 30/06/2023
 * Description: triigers the animation of the trigger event that blocks player
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockage : MonoBehaviour
{
    /// <summary>
    /// Animator of blockage animator
    /// </summary>
    public Animator animator;

    /// <summary>
    /// Audio source of the crash sound
    /// </summary>
    public AudioSource crashSound;

    /// <summary>
    /// On the trigger enter calls the animation
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        animator.SetBool("isPresent", true);
        crashSound.Play();
    }
}
