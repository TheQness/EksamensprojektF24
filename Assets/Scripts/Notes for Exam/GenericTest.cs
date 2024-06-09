using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var itemShop = new Generic<Collectable>(); //Creates a new instance of Shop<string> in GameBehavior and specifies string values as the generic type
        Debug.Log("Items for sale: " + itemShop.GetStockCount<Potion>()); //Prints out a debug message with the inventory count: debugs 0
        itemShop.AddItem(new Potion()); //adds items
        itemShop.AddItem(new Antidote()); //adds items
        Debug.Log("Items for sale: " + itemShop.GetStockCount<Potion>());
        debug(myName); // call to the debug delegate instance. This results in Print(myName) being executed, logging myName to the console.
        LogWithDelegate(debug); //Calls LogWithDelegate() and passes in our debug variable as its type parameter.  Inside LogWithDelegate, the debug delegate is invoked, which calls the Print method with the specified string.
    }

    private string myName = "Sofie";

    //Delegate declaretion 
    public delegate void DebugDelegate(string newText); //Declares a public delegate type named DebugDelegate to hold a method that takes in a string parameter and returns void

    //Delegate instance created 
    public DebugDelegate debug = Print; //Creates a new DebugDelegate instance named debug and assigns it a method with a matching signature named Print(). This means that debug can be used to call the Print method

    public static void Print(string newText)
    {
        Debug.Log(newText);
    }

    //Using Delegate is parameter
    public void LogWithDelegate(DebugDelegate del) //Declares a new method that takes in a parameter of the DebugDelegate type
    {
        del("Delegating the debug task..."); //Calls the delegate parameter’s function and passes in a string literal to be printed out:
    }
}

public class Collectable : MonoBehaviour
{
    public string itemName;
}

public class Potion : Collectable
{
    public Potion()
    {
        this.itemName = "Potion";
    }
}

public class Antidote : Collectable
{
    public Antidote()
    {
        this.itemName = "Antidote";
    }
}