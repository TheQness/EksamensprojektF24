using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HashSet : MonoBehaviour
{
    /// <summary>
    ///  it cannot store duplicate values and is not sorted, meaning its elements are not ordered in any way. 
    /// therefore, Unlike arrays or lists, HashSet does not support indexing. You cannot access elements by their position in the set.
    /// Think of HashSets as dictionaries with just keys, instead of key-value pairs.
    /// They can perform set operations and element lookups extremely fast
    /// You can initialize thwm with default values
    /// Has performance-based mathematical set operations.
    /// </summary>

    HashSet<string> activePlayers = new HashSet<string>() { "Joy", "Joan", "Hank"};
    HashSet<string> inactivePlayers = new HashSet<string>() { "Anne", "James", "William"};

    private void Add()
    {
        activePlayers.Add("Walter");
        activePlayers.Add("Evelyn");
    }

    private void Remove()
    {
        inactivePlayers.Remove("James");
    }

    //Also has CopyTo array, Clear, Contains, Equals

    /// <summary>
    /// Has performance-based mathematical set operations.
    /// Set operations need two things: a calling collection object and a passed-in collection object
    /// The calling collection object is the HashSet you want to modify based on which operation is used
    /// the passed-in collection object is used for comparison by the set operation.
    /// Three mean operations, UnionWith, ExceptWith and IntersectWith
    /// There are two more groups of set operations that deal with subset and superset com￾putations, but is not wihtin the scope of this project
    /// </summary>

    private void UnionWith1() //The calling collection objects now also stores the elements of the passed in collection object
    {
        HashSet<string> fruitsInStock = new HashSet<string>() { "Apple", "Banana", "Strawberry"};
        HashSet<string> fruitsOutOfStock = new HashSet<string>() { "Pineapple", "Kiwi", "BlueBerry"};
        fruitsInStock.UnionWith(fruitsOutOfStock); //fruitsInStock now also stores fruits out of stock
        //Maybe it can cause problems, at the name Fruitsinstock is accurate.
    }

    private void UnionWith2() //The calling collection objects now also stores the elements of the passed in collection object
    {
        HashSet<string> fruitsInStock = new HashSet<string>() { "Apple", "Banana", "Strawberry"};
        HashSet<string> fruitsOutOfStock = new HashSet<string>() { "Pineapple", "Kiwi", "BlueBerry"};
        HashSet<string> allFruits = new HashSet<string>();
        allFruits.UnionWith(fruitsInStock);
        allFruits.UnionWith(fruitsOutOfStock);
        //Now they are all gathered in a new HashSet called allFruits.
    }

    private void IntersectWith() //Finds common elements in the hash sets and stores them in the calling collection objects.
    {
        HashSet<string> fruitsInStock = new HashSet<string>() { "Apple", "Banana", "Strawberry"};
        HashSet<string> fruitsOnSale = new HashSet<string>() { "Apple", "Mango", "Passionfruit"};
        fruitsInStock.IntersectWith(fruitsOnSale); //intersect the HashSets, if fruitsInStock has elements that intersects with fruitsOnSale, they are stored in fruitsInStock, others are removed. Fruits both in stock and on sale
    }

    private void ExceptWith() //Does the oppositte
    {
        HashSet<string> fruitsInStock = new HashSet<string>() { "Apple", "Banana", "Strawberry"};
        HashSet<string> fruitsOnSale = new HashSet<string>() { "Apple", "Mango", "Passionfruit"};
        fruitsInStock.ExceptWith(fruitsOnSale); //does the opposite, fruitsInStock now only stores the fruits that are in stock, but not on Sale.
    }
}
