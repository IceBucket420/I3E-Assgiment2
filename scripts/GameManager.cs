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
    public int MainMenu;
    public int audioSettings;


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
        GetComponent<AudioSource>().Play();
        DontDestroyOnLoad(QuitMenu);
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
        GetComponent<AudioSource>().Play();
        QuitMenu.gameObject.SetActive(true);
    }

    public void OnYesButtion()
    {
        GetComponent<AudioSource>().Play();
        Application.Quit();
    }

    public void OnNoButton()
    {
        GetComponent<AudioSource>().Play();
        QuitMenu.gameObject.SetActive(false);
    }

    public void OnSettingsButton()
    {
        SceneManager.LoadScene(2);
        GetComponent<AudioSource>().Play();
        DontDestroyOnLoad(QuitMenu);
        DontDestroyOnLoad(MainMenuCanvas);
        MainMenuCanvas.gameObject.SetActive(false);
    }


    public void OnBackButton()
    {
        SceneManager.LoadScene(3);
        GetComponent<AudioSource>().Play();
    }

    public void OnMainMenuButton()
    {
        SceneManager.LoadScene(3);
        GetComponent<AudioSource>().Play();
    }

    public void OnRestartButton(int i)
    {
        i = PlayerPrefab.GetComponent<PlayerMovement>().CurrentScene;
        GetComponent<AudioSource>().Play();
        SceneManager.LoadScene(i);
    }

    private void Start()
    {
        GetComponent<AudioSource>().Play();
        QuitMenu.gameObject.SetActive(false);

    }
    void Update()
    {

    }
}
