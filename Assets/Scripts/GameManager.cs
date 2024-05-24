using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set; } // Singleton instance of GameManager.  public getter, but private setter

    private UIManager uiManager; //Reference to uiManager

    // Private field and public property for the world number
    private int _world;
    public int World
    {
        get {return _world; } //Getter for World
        private set {_world = value; }// Private setter for World
    }

    // Private field and public property for the stage number
    private int _stage;
    public int Stage
    {
        get { return _stage; } //Getter for Stage
        private set 
        { 
            _stage = value; // Set _stage to the provided value
            if (_stage > 3) // If _stage exceeds 3, reset to 1 and increment the world
            {
                _stage = 1;
                World++;
            }
        }
    }

     // Private field and public property for player lives
    private int _playerLives;
    public int PlayerLives
    {
        get {return _playerLives; } // Getter for PlayerLives
        set 
        {
            _playerLives = value; //Set _playerLives to the provided value
            UIManager uiManager = GameObject.FindObjectOfType<UIManager>(); // Find UIManager in the scene
            uiManager.UpdateLifeUI();  // Update the UI to reflect the new player lives
        }
    }

    // Private field and public property for coins collected
    private int _coinsCollected;
    public int CoinsCollected
    {
        get {return _coinsCollected; }// Getter for CoinsCollected
        set
        {
            _coinsCollected = value; // Set _coinsCollected to the provided value
            if (_coinsCollected >= 100) // If collected coins reach 100, reset to 0 and Add an extra life
            {
                _coinsCollected = 0;
                AddLife();
            }
            UIManager uiManager = GameObject.FindObjectOfType<UIManager>(); // Find UIManager in the scene
            uiManager.UpdateCoinUI(); // Update the UI to reflect the new coin count
        }
    }

    private void Awake()
    {
        if (Instance != null) // If an instance of GameManager already exists
        {
            DestroyImmediate(gameObject); // Destroy this instance to enforce the singleton pattern
        }
        else
        {
            Instance = this; // Set this instance as the singleton instance
            DontDestroyOnLoad(gameObject); // Prevent this gameObject from being destroyed when loading a new scene
        }
    }

    private void OnDestroy() // Method called when the gameObject is being destroyed 
    {
        if (Instance == this) //Instance == this ensures that the code only runs if the current GameManager being destroyed is indeed the one stored in the Instance
        {
            Instance = null; //This effectively removes the reference to the Singleton instance, allowing a new instance to be created in the future if necessary.
        }
    }

    private void Start()
    {
        Application.targetFrameRate = 60;  // Set the target frame rate to 60 FPS
        StartNewGame(); // Start a new game
    }

    private void StartNewGame() // Method to start a new game
    {
        PlayerLives = 3; // Set player lives to 3
        CoinsCollected = 0; // Set collected coins to 0
        LoadLevel(1, 1); // Load the first level
    }


    public void LoadLevel(int world, int stage)
    {
        this.World = world; // Set the world number
        this.Stage = stage; // Set the stage number
        SceneManager.LoadScene($"{World}-{Stage}"); // Load the scene based on the world and stage number
    }

    public void NextLevel()
    {
        Stage++; // Increment the Stage
        LoadLevel(World, Stage); // Load the next level
    }

    public void ResetLevel(float delay)
    {
        Invoke(nameof(ResetLevel), 3f); // Invoke the ResetLevel method after the specified delay
    }

    public void ResetLevel() //Method to reset the current Level
    {
        SubtractLife(); // Subtract one life
        CoinsCollected = 0;  //Reset collected coins to 0
        if (PlayerLives > 0) // If player still has lives left
        {
            LoadLevel(World, Stage); // Reload the current level
        }
        else 
        {
            GameOver(); // Trigger game over
        }
    }
    
    private void GameOver()
    {
        Invoke(nameof(StartNewGame), 2f); // Start a new game after a 2 second delay
    }

    
    public void AddCoin()
    {
        CoinsCollected ++; // Increment collected coins by 1
    }

    public void AddCoin(int coins)// Method to add a specific number of coins, method overloading
    {
        CoinsCollected += coins;
    }

    public void AddLife()
    {
        PlayerLives++; // Increment player lives by 1
    }

    public void SubtractLife()
    {
        PlayerLives--; // Decrement player lives by 1
    }
}
