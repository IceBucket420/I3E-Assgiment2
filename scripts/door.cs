/*
 * Author: Pang Le Xin 
 * Date: 29/06/2023
 * Description: Controls the door animation in the spaceship
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class door : MonoBehaviour
{
    /// <summary>
    /// Animator for the door animator
    /// </summary>
    public Animator animator;
    /// <summary>
    /// Animator for the door animator 
    /// </summary>
    public Animator animator2;
    /// <summary>
    /// audio source for the door
    /// </summary>
    public AudioSource doorSound;

    /// <summary>
    /// When player enter trigger enter area door open
    /// </summary>
    /// <param name="other"></param>
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

    /// <summary>
    /// When player exit trigger area the door close
    /// </summary>
    /// <param name="other"></param>
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
