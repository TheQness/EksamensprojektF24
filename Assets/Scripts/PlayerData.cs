using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    private int _coinsCollected;
    private int _playerLives;

    public int CoinsCollected
    {
        get {return _coinsCollected; }
        set
        {
            if (_coinsCollected >= 100)
            {
                _coinsCollected = 0;
                _playerLives ++;
            }
            else
            {
                _coinsCollected = value;
            }

        }
        
    }

    public int PlayerLives
    {
        get {return _playerLives; }
        set {_playerLives = value; }
    }

}
