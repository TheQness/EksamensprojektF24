using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Stacks : MonoBehaviour //Lets say this is struct of Loot
{
     /// <summary>
    /// a stack is a collection of elements of the same specified type.
    /// The length of a stack is variable, meaning it can change depending on how many elements it’s holding.
    /// The important difference between a stack and a list or array is how the elements are stored.
    /// While lists or arrays store elements by index, stacks follow the last-in-first-out (LIFO) model, meaning the last element in the stack is the first accessible element. 
    /// stacks can’t be initialized with elements when they’re created. Instead, all elements must be added after the stack is created.
    ///Insertion and Deletions happens at the same end
    /// </summary>

    public Stack<Loot> LootStack = new Stack<Loot>();

    void Push() //Adds an element to the top of the stack.
    {
        LootStack.Push(new Loot("Sword of Doom", 5));
        LootStack.Push(new Loot("HP Boost", 1));
        LootStack.Push(new Loot("Golden Key", 3));
        LootStack.Push(new Loot("Pair of Winged Boots", 2));
        LootStack.Push(new Loot("Mythril Bracer", 4));
    }

    void Peek() //returns next element in stack without removing out, letting you peek at it wihtout changing anything.
    {
        Loot nextItem = LootStack.Peek();
        Debug.Log("Next item in your inventory is " + nextItem.name);
    }

    void Pop() //returns next element in stack and removes out, popping out the element of the Stack.
    {
        Loot currentItem = LootStack.Pop();
        Debug.Log("You just picked up " + currentItem.name);
    }

    void Clear() //emties out the stack, deletes every element
    {
        LootStack.Clear();
    }

    void Contains() //Checks if the stack contains an Items and return true if the stack contains it
    {
        Loot itemToCheck = new Loot("Golden Key", 3);
        bool itemFound = LootStack.Contains(itemToCheck);
    }

    void CopyTo() //copies the stack elements to an existing array:
    {
        Loot[] Chest = new Loot [LootStack.Count];
        LootStack.CopyTo(Chest, 0);
        Clear();
    }

    void ToArray() // creates a new array out of your stack
    {
        Loot[] array = LootStack.ToArray();
    }

    void Equals()
    {
        Loot item1 = new Loot("Golden Key", 3);
        Loot item2 = new Loot("Golden Key", 3);
        bool areEqual = item1.Equals(item2);
        Debug.Log($"Is {item1.name} euquals to {item2.name}? Answer: {areEqual}");
    }

    public void PrintLootInformation()
    {
        Debug.LogFormat("There are {0} random loot items waiting for you!", LootStack.Count); //counts number of elements
    }

    void Start()
    {
        
        Push();
        /*
        PrintLootInformation();
        Pop();
        Peek();
        Equals();
        */
        ChainingQuiries();
    }

    //Could be used for un-do logic? Storing their last actions or like states in a queue, and when undoing, it pops the last gamestate or something
    
    /// <summary>
    ///  LINQ = Language Integrated Query
    ///  fast, efficient, and, most importantly, customizable for complex data filtering,
    /// LINQ extension methods work on any collection type that implements IEnumerable<T>, which includes Lists, Dictionaries, Queues, Stacks, HashSets and Arrays
    /// First, you need a data source—the collection type holding all the data elements you’re trying to filter, order, or group. 
    /// 2. Second, you create a query—the rules you want to apply to the data source you’re working with. A predicate is a rule or criteria that evaluates a certain condition. 
    /// 3. Third, you run the query—the data source needs to be iterated over with a looping state￾ment for the query commands to execute. This is called deferred execution.
    /// </summary>

    private void FilterLootByRarity() // the Where extension method filters out loot items that don’t meet our criteria.
    {
        IEnumerable<Loot> rareLoot = LootStack.Where(LootPredicate); // When adding the first parentheses after the Where method, Visual Studio will let you know that the extension method is expecting a predicate argument in the form of a delegate with a specific method signature
        foreach (var item in rareLoot)
        {
            Debug.LogFormat("Rare item: {0}!", item.name);
        }
    }
    //The delegate (methods) signature needs to match predicate argument for the function  Func<Loot, bool>, takes in loot and returns a bool
    //Each time the Where query iterates over a loot item, it’ll evaluate the predicate condition and return true or false.

    private bool LootPredicate(Loot loot)
    {
        return loot.rarity >= 3; //returns all loot with rarity over 
    }

    //LAMBDA EXPRESSIONS
    // anonymous functions, meaning they don’t have or need a name but still have method arguments (inputs) and return types, which makes them perfect for LINQ queries. 
    private void FilterLootWithLambda() // the Where extension method filters out loot items that don’t meet our criteria.
    {
        IEnumerable<Loot> rareLoot = LootStack.Where(item => item.rarity >= 3); // When adding the first parentheses after the Where method, Visual Studio will let you know that the extension method is expecting a predicate argument in the form of a delegate with a specific method signature
        foreach (var item in rareLoot)
        {
            Debug.LogFormat("Rare item: {0}!", item.name);
        }
    }

    //CHAINING QUIRIES
    //You can chain different Quiries together

    private void ChainingQuiries() // the Where extension method filters out loot items that don’t meet our criteria.
    {
        IEnumerable<Loot> rareLoot = LootStack
        .Where(item => item.rarity >= 3)
        .OrderBy(item => item.rarity); //orders from lowest to higherst rarity.
        foreach (var item in rareLoot)
        {
            Debug.LogFormat("Rare item: {0} with rarity: {1}", item.name, item.rarity);
        }
    }

    //Transform data from Quiries
    //Not all data from the quiries are useful. Might not be useful to know a players location after filtering the data.
    private void ChangingType() // the Where extension method filters out loot items that don’t meet our criteria.
    {
        var rareLoot = LootStack
        .Where(item => item.rarity >= 3)
        .OrderBy(item => item.rarity) //orders from lowest to higherst rarity.
        .Select(item => new //creates an anonymous type without the rariryt property.
            {
                item.name
            });
        foreach (var item in rareLoot)
        {
            Debug.LogFormat("Rare item: {0}", item.name);
        }
    }
    //Anonymous types let you encapsulate properties into an object without having to explicitly define the object type
    //Rare loot is an anonymous type of just the names of the items.
    //like a shortcut to creating a new object without the added declaration syntax. 
    // This is perfect for LINQ queries, since we don’t need the extra headache of creating a new object class just to project our filtered data into a new containing type.

    private void ShortHand()
    {
        var rareLoot = from item in LootStack
        where item.rarity >= 3
        orderby item.rarity
        select new { item.name };
    }

    //only the most commonly used extension methods have counterparts in query comprehension syntax. 
    //Skip does not have a short-hand, therefore we need to put parenthesis around the shorthand.
    
    private void ShortHandMix()
    {
        var rareLoot = (from item in LootStack
        where item.rarity >= 3
        orderby item.rarity
        select new { item.name })
            .Skip(1); //skips the first rare item
    }
}

