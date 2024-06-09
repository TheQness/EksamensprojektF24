using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Xml;

public class XML : MonoBehaviour
{
    /* DELETE THIS FOR EXAM
    /// <summary>
    /// XML (Extensible Markup Language), which is a way of encoding document information so it’s readable for you and a computer
    /// XML is better at storing information in a document format,
    /// </summary>

    //SYNTAX

    <?xml version="1.0" encoding="utf-8"?> //version
    <root_element> //starting element
        <element_item>[Information goes here]</element_item> //opening and closing with tag attributes.
        <element_item>[Information goes here]</element_item>
        <element_item>[Information goes here]</element_item>
    </root_element> //closing/root element


    //ARRAY OF WEAPONS:
    <?xml version="1.0"?> //The version being used.
    <ArrayOfWeapon> //The root element is declared with an opening tag named ArrayOfWeapon, which will hold all our element items
    <Weapon> //A weapon item is created with an opening tag named Weapon
        <name>Sword of Doom</name> //Its child properties are added with opening and closing tags on a single line for name and damage
        <damage>100</damage>
    </Weapon> //The weapon item is closed, and two more weapon items are added
    <Weapon>
        <name>Butterfly knives</name>
        <damage>25</damage>
    </Weapon>
    <Weapon>
        <name>Brass Knuckles</name>
        <damage>15</damage>
    </Weapon>
    </ArrayOfWeapon> //The array is closed, marking the end of the document

    /*
    Sometimes, you won’t just have plain old text to write and read from a file. Your project might 
    require XML-formatted documents, in which case, you’ll need to know how to use a regular 
    FileStream to save and load XML data.
    Writing XML data to a file isn’t all that different from what we’ve been doing with text and streams. 
    The only difference is we’ll explicitly create a FileStream and use it to create an instance of an 
    XmlWriter. ¨
    Think of the XmlWriter class as a wrapper that takes our data stream, applies XML formatting, and spits out our information as an XML file. 
    Once we have that, we can structure the document in the proper XML format using methods from the XmlWriter class and close the file.
    */

}
