using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject PlayerPrefab;
    private PlayerMovement activePlayer;
    public static int Score;
    public static GameManager instance;
    public GameObject QuitMenu;
    public GameObject MainMenuCanvas;
    public int Level2;
    public AudioSource bgm;

    // Start is called before the first frame update
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
    private void SpawnPlayerOnLoad(Scene prev, Scene next)
    {
        Debug.Log("Entering Scene is:" + next.buildIndex);
  
        playerSpawnSpot playerSpot = FindObjectOfType<playerSpawnSpot>();
        if (activePlayer == null)
        {
            GameObject player = Instantiate(PlayerPrefab, playerSpot.transform.position, playerSpot.transform.rotation);
            activePlayer = player.GetComponent<PlayerMovement>();
        }
        else
        {
            activePlayer.transform.position = playerSpot.transform.position;
            activePlayer.transform.rotation = playerSpot.transform.rotation;
        }
    }

    public void OnStartButton()
    {
        SceneManager.LoadScene(1);
        DontDestroyOnLoad(QuitMenu);
        MainMenuCanvas.gameObject.SetActive(false);
    }

    public void OnQuitButton()
    {
        QuitMenu.gameObject.SetActive(true);
    }

    public void OnYesButtion()
    {
        Application.Quit();
    }

    public void OnNoButton()
    {
        QuitMenu.gameObject.SetActive(false);
    }

    private void Start()
    {
        QuitMenu.gameObject.SetActive(false);
    }
    void Update()
    {
        
    }
}
