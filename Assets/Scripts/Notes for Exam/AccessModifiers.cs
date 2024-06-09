using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessModifiers : MonoBehaviour
{
    //constant keyword
    //only for built in c# types
    //must be initialized at declaretion, cannot be initialized during runtime.
    public const int maxLife = 90;

    //readonly keyword
    //same as const, but can be declared without initial value, but lets is assign it at any given point in runtime. after there, it cannot be changed.
    public readonly string characterName;

    /*
    • Protected: Accessible from their containing class or types derived from it.
    • Internal: Only available in the current assembly (an automatically generated file that bundles your code, resources, and pretty much everything else together in a neat package). 
*/


}
