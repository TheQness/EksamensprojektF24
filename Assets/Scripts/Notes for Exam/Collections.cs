using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collections : MonoBehaviour
{
    private int _playerStamina = 10;

    public int PlayerStamina //get set property
    {
        get
        {
            return _playerStamina;
        }
        set
        {
            if (value > 20)
            {
                _playerStamina = 20;
                Debug.LogFormat("Stamina has reached maximum of: {0}", _playerStamina); //limits _playerStamine to 20
            }
            else
            {
                _playerStamina = value;
                Debug.LogFormat("Stamina: {0}", _playerStamina);
            } 
        }
    }

    public int PlayerLevel { get; set; } //automatically generates get set accesor and _playerLevel private variable
    

    //Array Longhand
    public string[] months = new string []
    {
        "January",
        "February",
        "March",
        "April",
        "June",
        "July",
        "August",
        "September",
        "October",
        "November",
        "December"
    };
    //Array Short hand
    public string[] weekDays =
    {
        "Monday",
        "Tuesday",
        "Wednesday",
        "Thursday",
        "Friday",
        "Saturday",
        "Sunday"
    };
    public string currentMonth;
    public string currentWeekDay;

    //Multidimensional arrays, 3 rows, 2 collums
    public int[,] coordinates = new int[3, 2]
    {
        {1,1},
        {2,2},
        {3,3}
    };

    void ChangeValueMultiArray()
    {
        Debug.Log(coordinates[0, 1]); //debugs 1
        coordinates[0, 1] = 10;
        Debug.Log(coordinates[0, 1]); //debugs 10
    }

    //Lists
    List<string> itemsInInventory = new List<string>()
    {
        "Gold",
        "Map",
        "Water",
        "Food"
    };

    void PrintItems() //Counts number of elements in list
    {
        Debug.LogFormat("There are {0} items in your inventory!", itemsInInventory.Count);
    }

    void ModifyItemsToInventory() //Adds element to list
    {
        itemsInInventory.Add("Potion"); //Adds potion after Food
        Debug.LogFormat("There are {0} items in your inventory!", itemsInInventory.Count);

        itemsInInventory.Insert(0, "Compas"); //Adds Compas at index 0
        Debug.Log(itemsInInventory[0]);

        itemsInInventory.RemoveAt(0);
        itemsInInventory.Remove("Gold");
    }

    //for loop
    public void ForLoopList() //initializer, conditions, iterator
    {
        int listLength = itemsInInventory.Count;
        for (int i = 0; i < listLength; i++)
        {
            Debug.Log(itemsInInventory[i]);
            Debug.LogFormat("Index {0}, {1}", i, itemsInInventory[i]);
            if (itemsInInventory[i] == "Potion") //nested
            {
                Debug.Log("You have a potion in your inventory");
            }
        }
    }

    public void ForEachLoopList() //stores each element in local variable accesible within the scope of the method
    {
        foreach(string item in itemsInInventory)
        {
            Debug.LogFormat("{0} - is in your inventory", item);
        }
    }

    //dictionary, key value pairs
    Dictionary<string, int> ItemInventory = new Dictionary<string, int>()
    {
        { "Map", 10},
        { "Water", 5},
        { "Food", 7},
        { "Potion", 50}
    };

    //Retrivieving values
    public void RetrieveValuesDictionary()
    {
        int costOfPotion = ItemInventory["Potion"];
    }
    //Updating values
    public void UpdateValuesDictionary()
    {
        ItemInventory["Potion"] = 55;
    }

    //Adding values
    public void AddValuesDictionary()
    {
        ItemInventory.Add("Pencil", 55);

        //if subscript operator is used to assign values to af key not exisiting, it adds it
        ItemInventory["Book"] = 55;
    }
    //Safer way to update
    public void UpdateValuesSaferDictionary() //check if the key exists. if pt does change it value.
    {
        if (ItemInventory.ContainsKey("Book"))
        {
            ItemInventory["Potion"] = 20;
        }
    }


    public void ForEachLoopDictionary()
    {
        foreach(KeyValuePair<string, int> keyValuePair in ItemInventory)
        {
            Debug.LogFormat("Item: {0} is worth {1} dollars", keyValuePair.Key, keyValuePair.Value);
        }
    }

    //while loop
    public int healthPlayer = 5;

    public void WhileLoop() //lops whole liver are above 0
    {
        while(healthPlayer > 0)
        {
            Debug.Log("Player Alive");
            healthPlayer--;
        }
        Debug.Log("Player Dead");
    }

    // Start is called before the first frame update
    void Start()
    {
        currentMonth = months[0];
        currentMonth = weekDays[3];
        ChangeValueMultiArray();
        PrintItems();
        ModifyItemsToInventory();
        ForLoopList();
        ForEachLoopList();
        ForEachLoopDictionary();
        WhileLoop();
        
    }
}
