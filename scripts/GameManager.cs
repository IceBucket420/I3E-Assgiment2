using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject PlayerPrefab;
    private PlayerMovement activePlayer;
    public static int Score;
    public static GameManager instance;
    public GameObject QuitMenu;
    public GameObject MainMenuCanvas;
    public GameObject AudioCanvas;
    public GameObject DeathCanvas;
    public Animator transition;
    public float transitionTime = 1f;
    public AudioSource ButtonSound;


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

    public void OnStartButton()
    {
        ButtonSound.Play();
        SceneManager.LoadScene(2);
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

    public void OnQuitButton()
    {
        QuitMenu.gameObject.SetActive(true);
        ButtonSound.Play();
    }

    public void OnYesButtion()
    {
        ButtonSound.Play();
        Application.Quit();
    }

    public void OnNoButton()
    { 
        QuitMenu.gameObject.SetActive(false);
        ButtonSound.Play();
    }

    public void OnSettingsButton()
    {
        //StartCoroutine(LoadLevel(1));
        //SceneManager.LoadScene(1);
        ButtonSound.Play();
        AudioCanvas.gameObject.SetActive(true);

    }


    public void OnBackButton()
    {
        Debug.Log("back is hit");
        ButtonSound.Play();
        //StartCoroutine(LoadLevel(0));
        AudioCanvas.gameObject.SetActive(false);
    }

    public void OnMainMenuButton()
    {
       //ButtonSound.Play();
        SceneManager.LoadScene(0);
    }

    public void OnRestartButton(int i)
    {
        ButtonSound.Play();
        i = PlayerPrefab.GetComponent<PlayerMovement>().CurrentScene;
        //StartCoroutine(LoadLevel(i));
        SceneManager.LoadScene(i);
    }


    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }

    private void Start()
    {
        QuitMenu.gameObject.SetActive(false);
        AudioCanvas.gameObject.SetActive(false);
        DeathCanvas.SetActive(false);
    }

    public void IncreaseScore()
    {
        Score = +1;
    }

    void Update()
    {

    }
}
