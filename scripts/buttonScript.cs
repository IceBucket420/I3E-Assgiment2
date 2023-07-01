using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonScript : MonoBehaviour
{
    public void OnMainMenuButton()
    {
        SceneManager.LoadScene(0);
    }
    
}
