using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    private TMP_Text coinText; // Reference to the TMP text component displaying the coin count
    private TMP_Text lifeText; // Reference to the TMP text component displaying the player's remaining lives

    private void Start()
    {
        coinText = GameObject.FindGameObjectWithTag("CoinText").GetComponent<TMP_Text>();  // Find and assign the TMP text component for displaying the coin count
        lifeText = GameObject.FindGameObjectWithTag("LifeText").GetComponent<TMP_Text>(); // Find and assign the TMP text component for displaying the player's remaining lives
        if (coinText == null)
        {
            Debug.LogError("TMP_Text component not found in children.");
            return;
        }
        if (coinText == null)
        {
            Debug.LogError("TMP_Text component not found in children.");
            return;
        }
        // Update the UI elements to reflect the initial values
        UpdateCoinUI();
        UpdateLifeUI();
    }

    public void UpdateCoinUI()
    {
        coinText.text = GameManager.Instance.CoinsCollected.ToString();  // Update the text of the coin count UI element with the current number of coins collected 
    }

    public void UpdateLifeUI()
    {
        lifeText.text = GameManager.Instance.PlayerLives.ToString(); // Update the text of the player lives UI element with the current number of lives remaining
    }
}
