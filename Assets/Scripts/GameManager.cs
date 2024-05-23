using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set; } //Static GameManager, public getter, but private setter

    private UIManager uiManager;
    public int world {get; private set; }
    public int stage {get; private set; }

    private int _playerLives;

    public int PlayerLives
    {
        get {return _playerLives; }
        set 
        {
            _playerLives = value; 
            UIManager uiManager = GameObject.FindObjectOfType<UIManager>();
            uiManager.UpdateLifeUI();  
        }
    }

    private int _coinsCollected;
    public int CoinsCollected
    {
        get {return _coinsCollected; }
        set
        {
            _coinsCollected = value;
            if (CoinsCollected >= 100)
            {
                _coinsCollected = 0;
                AddLife();
            }
            UIManager uiManager = GameObject.FindObjectOfType<UIManager>();
            uiManager.UpdateCoinUI();
        }
    }

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
        PlayerLives = 3;
        CoinsCollected = 0;
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
        SubtractLife();
        CoinsCollected = 0;
        if (PlayerLives > 0)
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
        CoinsCollected ++;
    }

    public void AddLife()
    {
        PlayerLives++;
    }

    public void SubtractLife()
    {
        PlayerLives--;
    }
}
