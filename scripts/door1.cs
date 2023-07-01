/*
 * Author: Pang Le Xin 
 * Date: 26/06/2023
 * Description: Activate the animation for the door in the spaceship
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class door1 : MonoBehaviour
{
    /// <summary>
    ///  The animator to trigger the animation
    /// </summary>
    public Animator animator;
    /// <summary>
    ///  The animator to trigger the animation
    /// </summary>
    public Animator animator2;

    /// <summary>
    /// door audio
    /// </summary>
    public AudioSource doorSound;

    /// <summary>
    /// On trigger enter the door would open and door sound would be played
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        animator.SetBool("isOpen", true);
        animator2.SetBool("isOpen", true);
        doorSound.Play();
    }
     
    /// <summary>
    /// On trigger exit, door would close and door sound would play again
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {

        animator.SetBool("isOpen", false);
        animator2.SetBool("isOpen", false);
        doorSound.Play();
    }
}
