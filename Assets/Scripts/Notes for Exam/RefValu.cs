using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefValu : MonoBehaviour
{
    private int playerHealth = 100;
    private int attackDamage = 50;

    private string fruitColor; // not initialized before passing into the method
    private string fruitName; // not initialized before passing into the method

    /// <summary>
    /// Primitive Data Types 
    /// Primitive data types are basic data types provided by the programming language. Predefined.
    /// They represent simple, atomic values and are not composed of other data types. 
    /// Examples include integers (int), floating-point numbers (float), characters (char), and booleans (bool). 
    /// Primitive data types are typically directly supported by the language and have specific size and behavior defined by the language specification.
    /// Often value types
    
    /// Non-Primitive Data Types: (Object types):
    /// Non-primitive data types, also known as composite or structured data types, are composed of multiple primitive or non-primitive data types. User Defined 
    /// Examples include arrays, strings, classes, structures, and enums. Non-primitive data types are created using primitive data types and may have complex internal structures and behaviors.
    /// Often reference types, structs are value type
    
    ///Reference Types: 
    /// Reference types store references (or pointers) to their data in memory. 
    /// When you assign a reference type variable to another variable, you are copying the reference, not the actual data. 
    /// Multiple variables can refer to the same underlying data. Reference types include classes, interfaces, arrays, and delegates. 
    ///Modifications made to the data through one reference affect all references to the same data.
    
    ///Value Types: 
    /// Value types store their actual data directly in memory. 
    /// When you assign a value type variable to another variable, a copy of the data is made. 
    /// Each variable has its own independent copy of the data. Examples of value types include primitive data types (integers, floats etc.) and structs in languages like C#. 
    /// Modifications made to one variable do not affect other variables containing the same data.

    /// 

    
    /// </summary>

    private void PassByValue()
    {
        Weapon bow = new Weapon("Bow", 100); //New instance of Weapon struct is created.,
        Weapon axe = bow; //Another instanc eof the Weapon struct is created and set to bow.

        //When printing our their stats, it whill print the same out.
        bow.PrintWeaponStats();
        axe.PrintWeaponStats();

        //If I then change the name of the axe weapon to Axe and damage to 400
        axe.weaponName = "Axe";
        axe.weaponDamage = 400;

        //When printing out again, they will not print out the same stats, demonstrationg that they do NOT reference the same object in the memory.
        // When axe was set to be the same as bow, a copy of  the data is made.
        bow.PrintWeaponStats();
        axe.PrintWeaponStats();
    }

    private void PassByReference()
    {
        Villager villager1 = new Villager(); // New instance of the Villager Class is created.
        Villager villager2 = villager1; // Villager1 assigned to another new instance of the villager class

        // When printing out their names, they will print our the same names.
        villager1.printVillagerInfo(); 
        villager2.printVillagerInfo();

        // If i then chnage the name for villager2 to "Benny the Bold"
        villager2.villagerName = "Benny The Bold";

        // When printing out their names again, they will again print out the same, even though i manually only modified villager 2.
        // This is because they reference the same objects in the memory. THey have the same memory allocation. 
        villager1.printVillagerInfo();
        villager2.printVillagerInfo();
    }

    /// <summary>
    /// By default, all arguments are passed by value, meaning that a variable passed into a method will not be affected by any changes that are made to its value inside the method body
    /// Protects from making unwanted changes to existing variables when using them as method parameters
    /// </summary>

    private void calculateAttackDamage(int health, int damage)
    {
        health -= damage;
        Debug.Log($"Your health will be: {health} after this attack. Do you wanna take {damage} in damage or avoid attack?");
    }

    private void ParameterPassedByValue()
    {
        Debug.Log(playerHealth); //debugs 100
        calculateAttackDamage(playerHealth, attackDamage); //debugs: Your health will be: 50 after this attack. Do you wanna take 50 in damage or avoid attack?
        Debug.Log(playerHealth); //still debugs 100, playerHealth was not modified, as it was passed by value
    }

    /// <summary>
    /// There are situations where you’ll want to pass in a method argument by reference so that it can be updated and have that change reflected in the original variable. 
    /// Prefixing a parameter declaration with either the ref or out keyword will mark the argument as a reference
    /// • Arguments have to be initialized before being passed into a method
    /// • You don’t need to initialize or assign the reference parameter value before ending the method
    /// • Properties with get or set accessors can’t be used as ref or out arguments
    /// </summary>

    private void takeDamage(ref int health, int damage) //using the ref keyword to pass health by reference
    {
        health -= damage; //now the original variable of playerHealth will decrease with the attackDamage.
    }

    private void RefKeyword()
    {
        Debug.Log(playerHealth); //debugs 100
        takeDamage(ref playerHealth, attackDamage);
        Debug.Log(playerHealth); //debugs 50, as 50 was subtracted from playerhealth, which was passed as reference instead of value.
    }

    /// <summary>
    /// Out keyword also makes the paramater be passed as a reference
    /// • Arguments do not need to be initialized before being passed into a method (more flexible)
    /// • The referenced parameter value does need to be initialized or assigned in the calling method before it’s returned
    /// • An out parameter is primarily used for returning data from a method. It's typically used when a method needs to return multiple values.
    /// • makes sures parameters are assigned before use
    /// </summary>

    private void GetFoodInfo(out string name, out string color)
    {
        name = "apple";
        color = "red";
    }
    
    private void OutKeyword()
    {
        Debug.Log($"Fruit: {fruitName}. Color: {fruitColor}");
        GetFoodInfo(out fruitName, out fruitColor);
        Debug.Log($"Fruit: {fruitName}. Color: {fruitColor}");
    }
}
