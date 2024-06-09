using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queues : MonoBehaviour
{
    /// <summary>
    /// a Queues is a collection of elements of the same specified type, same a stack
    /// The length of a queue is variable, meaning it can change depending on how many elements it’s holding, like stacks
    /// The important difference between a stack and a list or array is how the elements are stored.
    /// While lists or arrays store elements by index, stacks follow first in first out (fifo) model, meaning the last element in the stack is the first accessible element. 
    /// You should note that queues can store null and duplicate values but can’t be initialized with elements when they’re created. 
    /// Insertion happens and rear, deletion happens at front
    /// </summary>

    Queue<string> activePlayers = new Queue<string>();

    private void Enqueue() // adds elements to the end of the Queue
    {
        activePlayers.Enqueue("Harrison");
        activePlayers.Enqueue("Alex");
        activePlayers.Enqueue("Haley");
    }

    private void Peek() //Peeks at the first element in the Queues
    {
        string firstPlayer = activePlayers.Peek(); //returns "Harrison"
    }

    private void Dequeue() //Returns and removes the first element in the Queue.
    {
        string firstPlayer = activePlayers.Dequeue();
    }

/// Also has Contains, Clear, Equals, ToArray and so

//Examples of using Queues:
// Could be used Power Up legic, where you have to use the Power ups in the order you collected them.


}
