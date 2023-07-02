/*
 * Author: Pang Le Xin 
 * Date: 23/06/2023
 * Description: Controls the spawning of the player and manages the UI 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Gameobject of player
    /// </summary>
    public GameObject PlayerPrefab;
    /// <summary>
    /// Playermovement script reference
    /// </summary>
    private PlayerMovement activePlayer;
    /// <summary>
    /// Set instance for game manager
    /// </summary>
    public static GameManager instance;
    /// <summary>
    /// GameObject of the Quit menu
    /// </summary>
    public GameObject QuitMenu;
    /// <summary>
    /// GameObject of the Main menu menu
    /// </summary>
    public GameObject MainMenuCanvas;
    /// <summary>
    /// GameObject of the Audio menu
    /// </summary>
    public GameObject AudioCanvas;
    /// <summary>
    ///  GameObject of the How to play menu
    /// </summary>
    public GameObject HowToPlayCanvas;
    /// <summary>
    ///  GameObject of the credits menu
    /// </summary>
    public GameObject CreditsCanvas;
    /// <summary>
    /// Animator for the fade transition 
    /// </summary>
    public Animator transition;
    /// <summary>
    /// float transition time 
    /// </summary>
    public float transitionTime = 1f;
    /// <summary>
    /// Audio source of the button sound
    /// </summary>
    public AudioSource ButtonSound;


    /// <summary>
    /// Spawn the player on the load
    /// </summary>
    /// <param name="prev"></param>
    /// <param name="next"></param>
    private void SpawnPlayerOnLoad(Scene prev, Scene next)
    {
        Debug.Log("Entering Scene is:" + next.buildIndex);

        playerSpawnSpot playerSpot = FindObjectOfType<playerSpawnSpot>();
        if (activePlayer == null)
        {
            GameObject player = Instantiate(PlayerPrefab, playerSpot.transform.position, playerSpot.transform.rotation);
            activePlayer = player.GetComponent<PlayerMovement>();
            Debug.Log("Player is spawned");
        }
        else
        {
            activePlayer.transform.position = playerSpot.transform.position;
            activePlayer.transform.rotation = playerSpot.transform.rotation;
        }
    }

    /// <summary>
    /// Awake function
    /// </summary>
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            SceneManager.activeSceneChanged += SpawnPlayerOnLoad;

            instance = this;
        }
    }
    /// <summary>
    /// Start function for the main menu start button
    /// </summary>
    public void OnStartButton()
    {
        ButtonSound.Play();
        SceneManager.LoadScene(1);
        //StartCoroutine(LoadLevel(2));
        //DontDestroyOnLoad(QuitMenu);
        MainMenuCanvas.gameObject.SetActive(false);
        if (instance != null && instance != this)
           {
              Destroy(gameObject);
           }
        else
           {
             DontDestroyOnLoad(gameObject);
             SceneManager.activeSceneChanged += SpawnPlayerOnLoad;

              instance = this;
           }
        
    }
    /// <summary>
    /// function for the Main menu Quit button
    /// </summary>
    public void OnQuitButton()
    {
        QuitMenu.gameObject.SetActive(true);
        ButtonSound.Play();
    }
    /// <summary>
    /// function for the Quit Menu yes button
    /// </summary>
    public void OnYesButtion()
    {
        ButtonSound.Play();
        Application.Quit();
    }
    /// <summary>
    /// function for the Quit Menu no button
    /// </summary>
    public void OnNoButton()
    { 
        QuitMenu.gameObject.SetActive(false);
        ButtonSound.Play();
    }
    /// <summary>
    /// function for the settings button of main menu
    /// </summary>
    public void OnSettingsButton()
    {
        //StartCoroutine(LoadLevel(1));
        //SceneManager.LoadScene(1);
        ButtonSound.Play();
        AudioCanvas.gameObject.SetActive(true);

    }
    /// <summary>
    /// function for the back button in the settings menu
    /// </summary>
    public void OnBackButton()
    {
        Debug.Log("back is hit");
        ButtonSound.Play();
        //StartCoroutine(LoadLevel(0));
        AudioCanvas.gameObject.SetActive(false);
    }

    /// <summary>
    /// function for the credits button in the start menu
    /// </summary>
    public void OnCreditsButton()
    {
        Debug.Log("back is hit");
        ButtonSound.Play();
        //StartCoroutine(LoadLevel(0));
        CreditsCanvas.gameObject.SetActive(true);
    }
    /// <summary>
    /// function for the How to Play button in the start menu
    /// </summary>
    public void OnHowToPlayButton()
    {
        Debug.Log("back is hit");
        ButtonSound.Play();
        //StartCoroutine(LoadLevel(0));
        HowToPlayCanvas.gameObject.SetActive(true);
    }
    /// <summary>
    /// function for the back button in the How to play menu
    /// </summary>
    public void OnBackButton2()
    {
        Debug.Log("back is hit");
        ButtonSound.Play();
        //StartCoroutine(LoadLevel(0));
        HowToPlayCanvas.gameObject.SetActive(false);
    }
    /// <summary>
    /// function for the back button in the credits menu
    /// </summary>
    public void OnBackButton3()
    {
        Debug.Log("back is hit");
        ButtonSound.Play();
        //StartCoroutine(LoadLevel(0));
        CreditsCanvas.gameObject.SetActive(false);
    }

    /// <summary>
    /// function for the main menu button in the death menu
    /// </summary>
    public void OnMainMenuButton()
    {
       //ButtonSound.Play();
        SceneManager.LoadScene(0);
    }
    /// <summary>
    /// function for the restart button in the death menu
    /// </summary>
    public void OnRestartButton(int i)
    {
        ButtonSound.Play();
        i = PlayerPrefab.GetComponent<PlayerMovement>().CurrentScene;
        //StartCoroutine(LoadLevel(i));
        SceneManager.LoadScene(i);
    }

    /// <summary>
    /// give time for the transition
    /// </summary>
    /// <param name="levelIndex"></param>
    /// <returns></returns>
    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }

    /// <summary>
    /// Start function
    /// </summary>
    private void Start()
    {
        CreditsCanvas.gameObject.SetActive(false);
        HowToPlayCanvas.gameObject.SetActive(false);
        QuitMenu.gameObject.SetActive(false);
        AudioCanvas.gameObject.SetActive(false);
    }

}
