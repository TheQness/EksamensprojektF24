using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enums : MonoBehaviour
{
    public int enemyLives;

    public enum Size // Enum representing the size of the enemy.
    {
        Small = 2,
        Medium = 4, 
        Large = 6
    }
    public Size size;// The size of the enemy, gets set in inspector

    /// <summary>
    ///  an enumeration type is a set, or collection, of named constants that belong to the same variable. 
    /// These are useful when you want a collection of different values, but with the added benefit of them all being of the same parent type.
    /// They can store underlying types!
    /// underlying types for enumerations are limited to byte, sbyte, short, ushort, int, uint, long, and ulong. 
    /// These are called integral types, which are used to specify the size of numeric values that a variable can store.
    /// There’s no rule that says underlying values need to start at 0; in fact, all you have to do is specify the first value, and then C# increments the rest of the values for you.
    /// if we wanted the PlayerAction enum to hold non-sequential values, we could explicitly add them in
    /// </summary>

    private void SetLives()
    {
        if (size == Size.Small || size == Size.Medium || size == Size.Large)
        {
            enemyLives = (int)size;
            Debug.Log(enemyLives);
        }
        else
        {
            Debug.Log("Size not found");
        }
        
    }

    void Start()
    {
        SetLives();
    }
}
