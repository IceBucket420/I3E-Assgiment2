/*
 * Author: Pang Le Xin 
 * Date: 1/07/2023
 * Description: button function for the end menu
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonScript : MonoBehaviour
{
    /// <summary>
    /// the function lets button load the main menu scene
    /// </summary>
    public void OnMainMenuButton()
    {
        SceneManager.LoadScene(0);
    }
    
}
