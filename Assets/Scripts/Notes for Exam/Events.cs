using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events : MonoBehaviour
{
     /// <summary>
    /// C# events allow you to essentially create a subscription system based on actions in your games
    /// if you wanted to send out an event whenever an item is collected, or when a player presses the spacebar, you could do that. 
    /// Any class can subscribe or unsubscribe to an event through the calling class the event is fired from; 
    /// events form a kind of distributed-information superhighway for sharing actions and data across your application.
    /// </summary>

    public delegate void ClickingEvent(); //Declares a new delegate type that returns void and takes in no parameters

    public event ClickingEvent clickOnButton; //Creates an event of the ClickingEvent type, named clickOnButton, that can be treated as a method that matches the preceding delegate’s void return and no parameter signature

    void ButtonClick()
    {
        Debug.Log("The button was clicked");
        clickOnButton(); //calls the clickOnButton event in the ButtonClick methods, which gets called when a specific button is clicked
    }

   
}

public class Subscribe : MonoBehaviour
{
    public Events events; //Reference to Events class, gets set in inspector
    
    void OnEnable()
    {
        events.clickOnButton += HandleClickOnButton; //Subscribes to the clickOnButton event declared in Events class with a method named HandleClickOnButton using the += operator
    }

    public void HandleClickOnButton() //Declares the HandlePlayerJump() method with a signature that matches the event’s type and logs a success message each time the event is received:
    {
        Debug.Log("Player has clicked the button");
    }

    void OnDisable()
    {
        events.clickOnButton -= HandleClickOnButton; //unsubscirbes the HandleClickOnButton method to the event, when the gaeobject is destroyed
    }
}
