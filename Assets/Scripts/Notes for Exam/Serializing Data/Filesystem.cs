using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Xml;


public class Filesystem : MonoBehaviour
{
    // the name of the folder, or directory as it’s called

    //Serializing: The act of serializing an object translates the object’s entire state into another format
    //The act of deserializing is the reverse, taking the data from a file and restoring it to its former object state

    public void FilesystemInfo() //method to print out a few filesystem properties.
    {
        Debug.LogFormat("Path separator character: {0}", Path.PathSeparator); //This property returns the character used to separate individual paths in a list of paths.
        Debug.LogFormat("Directory separator character: {0}", Path.DirectorySeparatorChar); //This property returns the character used to separate directory levels in a path.
        Debug.LogFormat("Current directory: {0}", Directory.GetCurrentDirectory()); //This method returns the full path of the current working directory of the application.
        Debug.LogFormat("Temporary path: {0}", Path.GetTempPath()); // This method returns the path of the directory designated for temporary files.
    }

    void Start()
    {
        FilesystemInfo();
        NewDirectory();
        NewTextFile();
        UpdateTextFile();
        ReadFromFile(_textFile);
    }

    //Store persistent data
    //Persistent data means the information is saved and kept each time the program runs, which makes it ideal for this kind of player information'

    private string _dataPath; //We created a private variable to hold the data path string

    void Awake()
    {
        _dataPath = Application.persistentDataPath + "/Player_Data_Lala/"; //We set the data path string to the application’s persistentDataPath value, added a new folder name called Player_Data using open and closed forward slashes
        _textFile = _dataPath + "Save_Data.txt"; 
        _streamingTextFile = _dataPath + "Streaming_Save_Data.txt";
        _xmlLevelProgress = _dataPath + "Progress_Data.xml";
        Debug.Log(_dataPath); // printed out the complete path:
    }

    //CREATING NEW DIRECTORY
    public void NewDirectory()
    {
        if(Directory.Exists(_dataPath)) //First, we check if the directory folder already exists using the path we created
        {
            Debug.Log("Directory already exists..."); //If it’s already been created, we send ourselves a message in the console and use the return keyword to exit the method without going any further
            return;
        }

        Directory.CreateDirectory(_dataPath); //If the directory folder doesn’t exist, we pass the CreateDirectory() method our data path and log that it’s been created
        Debug.Log("New directory created!");
    }

    public void DeleteDirectory()
    {
        if(!Directory.Exists(_dataPath))
        {
            Debug.Log("Directory doesn't exist or has already been deleted...");
            return;
        }

        Directory.Delete(_dataPath, true);
        Debug.Log("Directory successfully deleted!");
    }

    //  CREATING FILES
    //  files need to be opened before you can add text, and they need to be closed after you’re finished. 
    //  If you don’t close the file you’re programmatically working with, it will stay open in the program’s memory.
    //  This both uses com￾putation power for something you’re not actively editing and can create potential memory leaks. 

    private string _textFile;

    public void NewTextFile()
    {
        if (File.Exists(_textFile))
        {
            Debug.Log("File already exists...");
            return;
        }
    
        File.WriteAllText(_textFile, "<SAVE DATA>\n"); //We use the WriteAllText() method because it does everything we need all in one: 
        //A new file is created using our _textFile path, We add a title string that says <SAVE DATA> and add two new lines with the \ncharacters
        //Then the file is closed for us automatically
        Debug.Log("New file created!");
    
    }

    public void UpdateTextFile()
    {
        if (!File.Exists(_textFile))
        {
            Debug.Log("File doesn't exist...");
            return;
        }
        File.AppendAllText(_textFile, $"Game started: {DateTime.Now}\n"); //If the file does exist, we use another all-in-one method called AppendAllText() to add the game’s start time:
        //This method opens the file, It adds a new line of text that’s passed in as a method parameter, It closes the file
        Debug.Log("File updated successfully!");
    }

    public void ReadFromFile(string filename)
    {
        if (!File.Exists(filename))
        {
            Debug.Log("File doesn't exist...");
            return;
        }
        Debug.Log(File.ReadAllText(filename));
    }

    public void DeleteFile(string filename) //method to delete file
    {
        if (!File.Exists(filename))
        {
            Debug.Log("File doesn't exist or has already been deleted...");
            return;
        }

        File.Delete(_textFile);
        Debug.Log("File successfully deleted!");
    }

    //STREAMS
    //  When we read, write, or update a file, our data is con￾verted into an array of bytes, which are then streamed to or from the file using a Stream object. 
    //  The data stream is responsible for carrying the data as a sequence of bytes to or from a file, acting as a translator for us between our game application and the data files themselves.

    //  The File class uses Stream objects for us automatically, and there are different Stream subclasses 
    //  for different functionality:
    //  • Use a FileStream to read and write data to your files
    //  • Use a MemoryStream to read and write data to memory
    //  • Use a NetworkStream to read and write data to other networked computers
    //  • Use a GZipStream to compress data for easier storage and downloading
    // streams are the way to go if you’re dealing with large and complex data objects.

    private string  _streamingTextFile;

    public void WriteToStream(string filename)
    {
        if (!File.Exists(filename)) //First, we check that the file doesn’t exist using its name
        {
            StreamWriter newStream = File.CreateText(filename); //If the file hasn’t been created yet, we add a new StreamWriter instance called newStream, which uses the CreateText() method to create and open the new file
            newStream.WriteLine("<Save Data> for HERO BORN \n"); //Once the file is open, we use the WriteLine() method to add a header
            newStream.Close(); //close the stream
            Debug.Log("New file created with StreamWriter!"); //print out a debug message
        }

        StreamWriter streamWriter = File.AppendText(filename); //If the file already exists, wejust want to update it, we grab our file through a new StreamWriter instance using the AppendText() method so our existing data doesn’t get overwritten
        streamWriter.WriteLine("Game ended: " + DateTime.Now); //Finally, we write a new line with our game data
        streamWriter.Close(); //close the stream
        Debug.Log("File contents updated with StreamWriter!"); //print out a debug message:
    }

    /*
    Reading from a stream is almost exactly like the ReadFromFile() method we created in the last 
    section. The only difference is that we’ll use a StreamReader instance to open and read the in￾formation. 
    Again, you want to use streams when you’re dealing with big data files or complex 
    objects instead of manually creating and writing to files with the File class:
    */

    public void ReadFromStream(string filename)
    {
        if (!File.Exists(filename)) //First, we check that the file doesn’t exist, and if it doesn’t, then we print out a console message and exit the method
        {
            Debug.Log("File doesn't exist...");
            return;
        }
        
        StreamReader streamReader = new StreamReader(filename); //If the file does exist, we create a new StreamReader instance with the name of the file we want to access and print out the entire contents using the ReadToEnd method:
        Debug.Log(streamReader.ReadToEnd());
    }

    //XML WRITER

    private string _xmlLevelProgress;

    public void WriteToXML(string filename)
    {
        if (!File.Exists(filename)) //First, we check if the file already exists
        {
            FileStream xmlStream = File.Create(filename); //If the file doesn’t exist, we create a new FileStream using the new path variable we created
            XmlWriter xmlWriter = XmlWriter.Create(xmlStream); //We then create a new XmlWriter instance and pass it our new FileStream
            xmlWriter.WriteStartDocument(); //Next, we use the WriteStartDocument method to specify XML version 1.0
            xmlWriter.WriteStartElement("level_progress"); //Then we call the WriteStartElement method to add the opening root element tag named level_progress

            for (int i = 1; i < 5; i++) //Now we can add individual elements to our document using the WriteElementString method, passing in level as the element tag and the level number using a for loop and its index value of i
            {
                xmlWriter.WriteElementString("level", "Level-" + i);
            }

            xmlWriter.WriteEndElement(); //To close the document, we use the WriteEndElement method to add a closing level tag
            xmlWriter.Close(); //Finally, we close the writer and stream to release the stream resources we’ve been using
            xmlStream.Close();

            //If you run the game now, you’ll see a new .xml file in our Player_Data folder with the level progress information:
        }      
    }

    //Automaticcaly closing streams by using the using 
    /*using(StreamWriter newStream = File.CreateText(filename))
    {
        // Any writing functionality goes inside the curly braces
        newStream.WriteLine("<Save Data> for HERO BORN \n");
    }
    */


    ///EXAMPLE OF SERIALIZING AND DESERIALIZING



}

