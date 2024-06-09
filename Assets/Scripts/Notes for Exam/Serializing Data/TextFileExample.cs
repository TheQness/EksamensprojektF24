using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Xml;
using System.Xml.Serialization;
using System.Text;

public class TextFileExample : MonoBehaviour
{
    //CRUD: create, read, update, delete princriple!
    private string _dataPath; //a private variable to hold the data path string
    private string _textFile;
    private string fileContent;
    private string streamFileContent;
    private string _streamingTextFile; // private string path for the new streaming text file 
    private string _xmlLevelProgress; //string to hold the path for a xml file
    private string _xmlWeapons; //xml file path for the weapons
    private string _jsonWeapons; //json file path for the weapons
    private string _jsonWeaponsList;

    private List<Weapon> weaponInventory = new List<Weapon> //list of weapons
    {
        new Weapon("Sword of Doom", 100),
        new Weapon("Butterfly knives", 25),
        new Weapon("Brass Knuckles", 15),
    };

    void Awake()
    {
        _dataPath = Application.persistentDataPath + "/Player_Data/"; //Creating the path to a directory where your application's data will be stored persistently across sessions.
        _textFile = _dataPath + "Save_Data.txt"; //creates a path to a file named Save_Data in the directory.
        _streamingTextFile = _dataPath +  "Streaming_Save_Data.txt"; //creates a new path to another text file in the directory.
        _xmlLevelProgress = _dataPath + "Progress_Data.xml"; //creates a new path to a xml file in the directory.
        _xmlWeapons = _dataPath + "WeaponInventory.xml";
        _jsonWeapons = _dataPath + "WeaponJSON.json";
        _jsonWeaponsList = _dataPath + "WeaponJSONList.json";
    }

    void Start()
    {

        FilesystemInfo();
        CreateDirectory(); //1!! Creates a new directory using the path we have created before.
        CreateTextFile(); //2 Creates a new Textfile in the directory where the Player Data will be stored.
        UpdateTextFile(); //3: Updates the data files with when the game was started
        ReadFromFile();


        //using stream
        WriteToStream();
        ReadFromStream();

        //XML example using streamd
        WriteToXML();
        ReadFromXML();

        //Serializing and deserializing a list of objects to XML
        SerializeXML();
        DeSerializeXML();

        //Serializing and deserializing a sword to Json
        SerializeJSON();
        DeserializeJSON();

        //serializing and deserializing a list of swords
        SerializeJSONList();
        DeserializeJSONList();
    }

    // CREATE: MAKING THE PLAYER DATA FOLDER /DIRECTORY
    public void FilesystemInfo() //method to print out a few filesystem properties.
    {
        Debug.LogFormat("Path separator character: {0}", Path.PathSeparator); //This property returns the character used to separate individual paths in a list of paths.
        Debug.LogFormat("Directory separator character: {0}", Path.DirectorySeparatorChar); //This property returns the character used to separate directory levels in a path.
        Debug.LogFormat("Current directory: {0}", Directory.GetCurrentDirectory()); //This method returns the full path of the current working directory of the application.
        Debug.LogFormat("Temporary path: {0}", Path.GetTempPath()); // This method returns the path of the directory designated for temporary files.
    }

    public void CreateDirectory()
    {
        if(Directory.Exists(_dataPath)) //First, we check if the directory/folder already exists using the path we created
        {
            Debug.Log("Directory already exists..."); //If it’s already been created, we send ourselves a message in the console and use the return keyword to exit the method without going any further
            return;
        }
        else
        { 
            Directory.CreateDirectory(_dataPath); //If the directory folder doesn’t exist, we pass the CreateDirectory() method our data path and log that it’s been created
            Debug.Log("New directory created!");
        }
    }

    /* 2 CREATING THE FILE IN THE FOLDER/DIRECTORY */
    //  files need to be opened before you can add text, and they need to be closed after you’re finished. 
    //  If you don’t close the file you’re programmatically working with, it will stay open in the program’s memory.
    //  This both uses computation power for something you’re not actively editing and can create potential memory leaks.

    public void CreateTextFile() //Method for creating a text file
    {
        if (File.Exists(_textFile)) //If a textfile with the given name alreadye exists, debugs file already exists and exits the method.
        {
            Debug.Log("File already exists...");
            return;
        }
    
        File.WriteAllText(_textFile, "<SAVE DATA>\n"); //A new file is created using our _textFile path, We add a title string that says <SAVE DATA> and add two new lines with the \ncharacters
        //Then the file is closed for us automatically
        Debug.Log("New file created!"); //Debugs New File Created.
    
    }

    //UPDATE THE FILE
    public void UpdateTextFile() //Method for updating the existing file we have created,
    {
        if (!File.Exists(_textFile))
        {
            Debug.Log("File doesn't exist...");
            return;
        }

        File.AppendAllText(_textFile, $"Game started: {DateTime.Now}\n"); //all-in-one method called AppendAllText() to add the game’s start time: opens the file, It adds a new line of text for when game is started, It closes the file
        Debug.Log("File updated successfully!"); // Debugs file updates succesfully
    }

    //READ THE FILE
    public void ReadFromFile()
    {
        if (!File.Exists(_textFile))
        {
            Debug.Log("File doesn't exist...");
            return;
        }
        fileContent = File.ReadAllText(_textFile);
        Debug.Log(fileContent); //get all the file’s text data as a string and print it out to the console
    }




    //DELETE
    public void DeleteDirectory() //Method for deleting a directory that has been created
    {
        if(!Directory.Exists(_dataPath))
        {
            Debug.Log("Directory doesn't exist or has already been deleted...");
            return;
        }

        Directory.Delete(_dataPath, true);
        Debug.Log("Directory successfully deleted!");
    }




    //USE STREAMS WHEN DEALING WITH LARGE DATA SETS?
    public void WriteToStream() //How to update a text file with a stream
    {
        if (!File.Exists(_streamingTextFile)) //if a file with the streamingFileName does not exist, do the initial steps of adding a Header.
        {
            StreamWriter newStream = File.CreateText(_streamingTextFile); //creates an instance of the StreamWriter class. File.CreateTExt text returns a StreamWriter that writes to the specified file. newStream represents a StreamWriter that is ready to write to the  _streamingTextFile.
            newStream.WriteLine("<Save Data> for Hero Born \n"); //A line is written to the file through the stream associatiod with the textfile.
            newStream.Close(); //The stream is then closed and relases the ressources. You cannot write data to the file using that particulat StreamWriter Object anymore.
            Debug.Log("New file created with StreamWriter");
        }

        StreamWriter streamWriter = File.AppendText(_streamingTextFile); // Create a new StreamWriter instance to append/add text to the existing file, that now has the header.
        streamWriter.WriteLine("Game started:" + DateTime.Now); // Writes a line of text to the file containing the current date and time
        streamWriter.Close(); //Closes the stream and ressources are released.
        Debug.Log("File content updated with StreamWriter"); // Log a message indicating that the file content was updated
    }  

    public void ReadFromStream()
    {
        if (!File.Exists(_streamingTextFile)) //If the file you want to read from does not exist, exit the method
        {
            Debug.Log("File does not exist");
            return;
        }

        StreamReader streamReader = new StreamReader(_streamingTextFile); //create a new instance of a Streamreader object with the new of the file we want to access
        streamFileContent = streamReader.ReadToEnd(); //The StreamReaderinstance then calls the ReadToEnd method, which returns a string of all the data in the file.
        Debug.Log(streamFileContent); //file content is then logged.
    } 

    // XML EXAMPLE using streams
    public void WriteToXML()
    {
        if (!File.Exists(_xmlLevelProgress))
        {
            FileStream xmlStream = File.Create(_xmlLevelProgress);//creates an instance of the FileStreamn class. File.Create text returns a FileStream object that writes to the specified file.. xmlStream represents a FileStream that is ready to write to the  xmlLevelProgress.
            XmlWriter xmlWriter = XmlWriter.Create(xmlStream); //creates a new XmlWriter to write XML content to the stream represented by the xmlStream, whihc the previous line of code created.
            //create a new XML file and set up an XmlWriter to write XML content to that file. The FileStream (xmlStream) provides the underlying stream for writing, and the XmlWriter (xmlWriter) provides the XML-specific functionality for writing XML content to the file.
            
            xmlWriter.WriteStartDocument(); //Writes the XML declaration at the beginning of the XML document, whihc specifies the version?
            xmlWriter.WriteStartElement("level_progress"); // Writes the start element <level_progress> to indicate the beginning of the root element of the XML document.

            for (int i = 1; i < 5; i++) // Loops from 1 to 4.
            {
                xmlWriter.WriteElementString("level", "level-" + i); //Writes an XML element <level> with the value "level-i" (where i is the current loop index) as a child of <level_progress>.
            }

            xmlWriter.WriteEndElement(); //Writes the end element </level_progress> to close the root element.
            xmlWriter.Close(); //closes the xmlWriter
            xmlStream.Close(); //closes the stream and releases ressources
        }
    }

    public void ReadFromXML() //same method as read from Stream
    {
        if (!File.Exists(_xmlLevelProgress)) //If the file you want to read from does not exist, exit the method
        {
            Debug.Log("File does not exist");
            return;
        }

        StreamReader streamReader = new StreamReader(_xmlLevelProgress); //create a new instance of a Streamreader object with the new of the file we want to access
        streamFileContent = streamReader.ReadToEnd(); //The StreamReaderinstance then calls the ReadToEnd method, which returns a string of all the data in the file.
        Debug.Log(streamFileContent); //file content is then logged.
    } 

    //AUTOMATICALY CLOSING STREAMs
    public void ClosingStreamAutomatically() //How to update a text file with a stream
    {
        if (!File.Exists(_streamingTextFile)) //if a file with the streamingFileName does not exist, do the initial steps of adding a Header.
        {
            using(StreamWriter newStream = File.CreateText(_streamingTextFile)) //closes the Stream automatically, no need for the .Close method to be called. Uses the IDisposible interface and calls the Dispose() method
            {
                newStream.WriteLine("<Save Data> for Hero Born \n"); //A line is written to the file through the stream associatiod with the textfile.
                Debug.Log("New file created with StreamWriter");
            }
            
        }

        StreamWriter streamWriter = File.AppendText(_streamingTextFile); // Create a new StreamWriter instance to append/add text to the existing file, that now has the header.
        streamWriter.WriteLine("Game started:" + DateTime.Now); // Writes a line of text to the file containing the current date and time
        streamWriter.Close(); //Closes the stream and ressources are released.
        Debug.Log("File content updated with StreamWriter"); // Log a message indicating that the file content was updated
    }

    //SERIALIZING DATA
    // The act of serializing an object translates the object’s entire state into another format
    // The act of deserializing is the reverse, taking the data from a file and restoring it to its former object state
    // objects states includes: weapon, name and damage., but also includes properties or fields that are referencetype, example of character class has a weapon property.

    //SERIALIZING AND DESERIALIZING XML

    public void SerializeXML()
    {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Weapon>)); //new instance of the XmlSerializer class, specifying that the serializer will be used to serialize a List of Weapon objects.
        using(StreamWriter stream = File.CreateText(_xmlWeapons)) //creates instance of StreamWriter class. File.CreateText returns StreamWirter object that writes to the specified file.. stream represents a StreamStream that is ready to write to the xmlWeapons.
        {
            xmlSerializer.Serialize(stream, weaponInventory); //This line uses the Serialize method of the XmlSerializer object to serialize the weaponInventory object (presumably a List<Weapon>) to the file stream (stream). 
            //Serialization is the process of converting an object into a format (in this case, XML) that can be easily stored or transmitted and later reconstructed back into an object.
        }
    }

    public void DeSerializeXML()
    {
        if (!File.Exists(_xmlWeapons))
        {
            Debug.Log("File not found");
            return;
        }

        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Weapon>)); //If the file exists, we create an XmlSerializer object and specify that we’re going to put the XML data back into a List<Weapon> object
        using (StreamReader stream = File.OpenText(_xmlWeapons)) //Then, we open up a FileStream with the _xmlWeapons filename:
        {
            var weapons = (List<Weapon>)xmlSerializer.Deserialize(stream); //Next, we create a variable to hold our deserialized list of weapons:
            foreach(var weapon in weapons)
            {
                Debug.LogFormat("Weapon: {0} - Damage: {1}", weapon.weaponName, weapon.weaponDamage); //Finally, we use a foreach loop to print out each weapon’s name and damage values in the console
            }
        }
    }

    //SERIALIZING AND DESERIALIZING JSON
    public void SerializeJSON()
    {
        Weapon sword = new Weapon("Sword of Doom", 100); //create a new sword to work with
        string jsonString = JsonUtility.ToJson(sword, true); //Then we declare a variable to hold the translated JSON data when it’s formatted as a string and call the ToJson() method:
        // ToJson() method we’re using takes in the sword object we want to serialize and a Boolean value of true so the string is pretty printed with proper indenting. If we didn’t specify a true value, the JSON would still print out; it would just be a regular string, which isn’t easily readable.

        using(StreamWriter stream = File.CreateText(_jsonWeapons)) //Now that we have a text string to write to a file, we create a StreamWriter stream and pass in the _jsonWeapons filename
        {
            stream.WriteLine(jsonString); //we use the WriteLine() method and pass it the jsonString value to write to the file
        }
    }
    //if we tried with the list of weapon, the filw would be overwritten and end up empty
    // the way Unity handles JSON serialization doesn’t support lists or arrays by themselves. Any list or array needs to be part of a class object for Unity’s JsonUtility class to recognize and handle it correctly. 

    public void DeserializeJSON()
    {
        if (!File.Exists(_jsonWeapons))
        {
            Debug.Log("File not found");
            return;
        }
        using (StreamReader streamReader = File.OpenText(_jsonWeapons)) // Open a StreamReader to read from the file
        {
            string jsonString = streamReader.ReadToEnd(); // Read the JSON string from the file
            Weapon newWeapon = JsonUtility.FromJson<Weapon>(jsonString); // Deserialize the JSON string into a Weapon object
            Debug.LogFormat("Weapon: {0} - Damage: {1}", newWeapon.weaponName, newWeapon.weaponDamage);

        // Now you can use the 'weapon' object as needed
        }
    }


    public void SerializeJSONList()
    {
        WeaponShop shop = new WeaponShop(); //create a new variable called shop, which is an instance of the WeaponShop class

        shop.inventory = weaponInventory; // set the inventory property to the weaponInventory list of weapons we already declared

        string jsonString = JsonUtility.ToJson(shop, true); // uses JsonUtility.ToJson() method to convert the shop object into a JSON string. true specifies that the JSON string should be formatted for readability.
        
        using(StreamWriter stream = File.CreateText(_jsonWeaponsList)) //Now that we have a text string to write to a file, we create a StreamWriter stream and pass in the _jsonWeaponsList filename. CreateText Creates or opens the file specified. If the file already exists, its contents are replaced.
        {
            stream.WriteLine(jsonString); //writes the JSON string (jsonString) to the file using stream.WriteLine().
        }
    }

    public void DeserializeJSONList()
    {
        if(!File.Exists(_jsonWeaponsList))
        {
            Debug.LogError("File does not exist");
            return;
        }

        using(StreamReader stream = new StreamReader(_jsonWeaponsList)) //If it does exist, we create a stream with the _jsonWeaponsList file path wrapped in a using code block
        {
            string jsonString = stream.ReadToEnd(); //Then, we use the stream’s ReadToEnd() method to grab the entire JSON text from the file and store it ind the string jsonString
            WeaponShop weaponData = JsonUtility.FromJson<WeaponShop>(jsonString); //method is then called to deserialize the JSON string into C# objects of type WeaponShop. 
            //This method converts the JSON string into an instance of the specified WeaponShop class.
            //WeaponShop contains a List<Weapon> property named inventory, the deserialization process populates this list with the data from the JSON array.

            foreach (var weapon in weaponData.inventory)
            {
                Debug.LogFormat("Weapon: {0} - Damage: {1}", weapon.weaponName, weapon.weaponDamage); //debugs each wepaons name and damage
            }
        }
    }
}
