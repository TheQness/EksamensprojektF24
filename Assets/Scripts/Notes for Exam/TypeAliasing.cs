using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Strings = System.Collections.Generic.List<string>; //type aliasing of list of strings
using ExtraTest = Extra.Test;
using GameTest = Game.Test;

public class TypeAliasing : MonoBehaviour
{
    Strings names = new Strings()
    {
        "Sofie",
        "Christoffer"  
    };

    void Start()
    {
        GameTest gt = new GameTest();
        ExtraTest et = new ExtraTest();
        gt.DebugName();
        et.DebugName();
    }
    
}
