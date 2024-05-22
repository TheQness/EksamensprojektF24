using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set; } //Static GameManager, public getter, but private setter

    public int world {get; private set; }
    public int stage {get; private set; }
    public int playerLives {get; private set; }
    public int coinsCollected {get; private set; }

    private void Awake()
    {
        if (Instance != null) //// If 'Instance' is not null, it means there is already an instance of 'GameManager' in existence.
        {
            DestroyImmediate(gameObject); // If another instance of 'GameManager' exists, destroy the current gameObject immediately to ensure there is only one instance of 'GameManager' (Singleton pattern).
        }
        else
        {
            Instance = this; // If 'Instance' is null, set it to this instance of 'GameManager'.
            DontDestroyOnLoad(gameObject); //dont destroy this gameobjects, when a new scene is loaded
        }
    }

    private void OnDestroy() //This method is called automatically by Unity when the game object that this script is attached to is destroyed. 
    {
        if (Instance == this) //Instance == this ensures that the code only runs if the current GameManager being destroyed is indeed the one stored in the Instance
        {
            Instance = null; //This effectively removes the reference to the Singleton instance, allowing a new instance to be created in the future if necessary.
        }
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
        StartNewGame();
    }

    private void StartNewGame()
    {
        playerLives = 3;
        coinsCollected = 0;
        LoadLevel(1, 1);
    }


    public void LoadLevel(int world, int stage)
    {
        this.world = world;
        this.stage = stage;
        SceneManager.LoadScene($"{world}-{stage}");
    }

    public void NextLevel()
    {
        if (world == 1 && stage == 3)
        {
            LoadLevel(2, 1);
        }
        else 
        {
            LoadLevel(world, stage + 1);
        }
    }

    public void ResetLevel(float delay)
    {
        Invoke(nameof(ResetLevel), 3f);
    }

    public void ResetLevel() //public to call this from other scripts
    {
        playerLives --;
        if (playerLives > 0)
        {
            LoadLevel(world, stage);
        }
        else 
        {
            GameOver();
        }
    }
    
    private void GameOver()
    {
        Invoke(nameof(StartNewGame), 2f);
    }

    public void AddCoin()
    {
        coinsCollected ++;
        if (coinsCollected == 100)
        {
            AddLife();
            coinsCollected = 0;
        }
    }

    public void AddLife()
    {
        playerLives++;
    }
}
