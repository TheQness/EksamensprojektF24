using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance {get; private set; }
    public int coinsCollected {get; private set; }
    [SerializeField]private TMP_Text coinCountText;
    [SerializeField]private RawImage coinSprite;

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
        coinCountText = GetComponentInChildren<TMP_Text>();
        coinSprite = GetComponentInChildren<RawImage>();
        DontDestroyOnLoad(coinCountText);
        DontDestroyOnLoad(coinSprite);

        if (coinCountText == null || coinSprite == null)
        {
            Debug.LogError("Failed to find TMP_Text or RawImage component.");
        }
        
    }

    private void Start()
    {
        coinsCollected = 0;
    }

    private void OnDestroy() //This method is called automatically by Unity when the game object that this script is attached to is destroyed. 
    {
        if (Instance == this) //Instance == this ensures that the code only runs if the current GameManager being destroyed is indeed the one stored in the Instance
        {
            Instance = null; //This effectively removes the reference to the Singleton instance, allowing a new instance to be created in the future if necessary.
        }
    }
    public void AddCoin()
    {
        coinsCollected ++;
        if (coinsCollected == 100)
        {
            GameManager.Instance.AddLife();
            coinsCollected = 0;
        }
        DisplayCoins();
    }

    public void DisplayCoins()
    {
        coinCountText.text = coinsCollected.ToString();
    }
}
