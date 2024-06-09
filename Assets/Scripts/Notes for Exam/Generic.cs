using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generic<T> where T : Collectable //constraints type parameter
{
   // Generic programming allows us to create reusable classes, methods, and variables using a placeholder, rather than a concrete type.
   //When a generic class instance is created at compile time or a method is used, a concrete type will be assigned, but the code itself treats it as a generic type. 
   //We’ve already seen this in action with the List type, which is a generic type. We can access all its addition, removal, and modification functions regardless of whether it’s storing integers, strings, or individual characters.

    public List<T> inventory = new List<T>(); //Adds an inventory List<T> of type T to store whatever item types we initialize the generic class with

    public void AddItem(T newItem) //Declares a method for adding newItems of type T to the inventory
    {
        inventory.Add(newItem);
    }
    //We can’t use type T again because it’s already been defined in the class definition, just like we couldn’t declare multiple variables with the same name in the same class. 
    
    public int GetStockCount<U>() where U : T //Declares a method that returns an int value for how many matching items of type U we find in the inventory
    {
        var stock = 0; //Creates a variable to hold the number of matching stock items we find and eventually return from the inventory
        foreach (var item in inventory)
        {
            if (item is U) //Uses a foreach loop to go through the inventory list 
            {
                stock++; // increase the stock value every time a match is found
            }
        }
    return stock; //Returns the number of matching stock items
    }
}
