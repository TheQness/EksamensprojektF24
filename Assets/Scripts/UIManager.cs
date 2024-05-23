using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    private TMP_Text coinText;
    private TMP_Text lifeText;

    private void Start()
    {
        coinText = GameObject.FindGameObjectWithTag("CoinText").GetComponent<TMP_Text>();
        lifeText = GameObject.FindGameObjectWithTag("LifeText").GetComponent<TMP_Text>();
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
        UpdateCoinUI();
        UpdateLifeUI();
    }

    public void UpdateCoinUI()
    {
        coinText.text = GameManager.Instance.CoinsCollected.ToString();  
    }

    public void UpdateLifeUI()
    {
        lifeText.text = GameManager.Instance.PlayerLives.ToString();
    }
}
