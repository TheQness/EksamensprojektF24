using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delegates : MonoBehaviour
{
    /// <summary>
    /// Delegates in C# are type-safe function pointers, meaning they hold references to methods with a specific signature.
    /// They are primarily used to define callback methods and implement event handling and listener functionality. 
    /// Delegates allow methods to be passed as parameters, stored as fields, and invoked dynamically.
    /// delegate types stores references to methods and can be treated like any other variable.
    /// the delegate itself and any assigned method need to have the same signature
    /// </summary>
    void Start()
    {
        //DECLARING DELEGATE
        printNumberDelegate = PrintNumber; // Assign the PrintNumber method to the delegate instance.
        printNumberDelegate(myNumber); // Invoke the delegate, which calls PrintNumber with myNumber as the parameter.

        printNumberDelegate = PrintDoubleNumber; // Reassign the delegate instance to the PrintDoubleNumber method.
        printNumberDelegate(myNumber); // Invoke the delegate, which now calls PrintDoubleNumber with myNumber as the parameter.

        //MULTICASTING
        multiDelegate += PowerUp;
        multiDelegate += ChangeColor;
        multiDelegate();

        //DELEGATE AS PARAMETER
        debugNameDelegate = DebugName;
        DebugWithDelegate(debugNameDelegate, myName);
    }

    //DECLARING DELEGATE
    delegate void PrintNumberDelegate(int number); // Declare a delegate type named PrintNumberDelegate that takes an int parameter and returns void.
    PrintNumberDelegate printNumberDelegate;  // Declare an instance of the delegate.

    int myNumber = 50; // Declare and initialize an integer variable named myNumber
    void PrintNumber(int number)// Define a method that matches the delegate signature.
    {
        Debug.Log("The number is" + number);
    }

    void PrintDoubleNumber(int number) // Define another method that matches the delegate signature.
    {
        Debug.Log("The number is" + number);
    }

    //MULTICASTING
    delegate void MultiDelegate();
    MultiDelegate multiDelegate;

    void PowerUp()
    {
        Debug.Log("You are powering up");
    }

    void ChangeColor()
    {
        Debug.Log("You're color changed to red");
    }

    //DELEGATE AS PARAMETER
    public string myName = "Sofie";
    public delegate void DebugNameDelegate(string name); //Declares a public delegate type named DebugDelegate to hold a method that takes in a string parameter and returns void
    DebugNameDelegate debugNameDelegate; 

    public void DebugName(string name)
    {
        Debug.Log("The name is" + myName);
    }

    public void DebugWithDelegate(DebugNameDelegate debug, string name) //Declares a new method that takes in a parameter of the DebugDelegate type
    {
        debug(name); //Calls the delegate parameter’s function and passes in a string literal to be printed out:
    }

}
