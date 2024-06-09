using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Interface//Common good practice to name them wiht a big I, like IManager
{
    /// <summary>
    /// Like abstract classes, interfaces cannot be used to create objects (in the example above, it is not possible to create an "IAnimal" object in the Program class)
    /// Interface methods do not have a body - the body of methods is provided by the "implement" class
    /// On implementation of an interface, you must override all of its methods
    /// Interfaces can contain properties and methods, but not fields/variables
    /// Interface members are by default abstract and public
    /// An interface cannot contain a constructor (as it cannot be used to create objects)
    ///  Breaking out functionality into interfaces lets you build up classes like building blocks, picking and choosing how you want them to behave
    ///  huge efficiency boost to your code base, breaking away from long, messy subclassing hierarchies
    /// </summary>

    void Hit();

    void Die();

    void Shrink();

    int lives {get; set; } //can contain propteries, but not variables.


    //common methods for both Enemys and mario


}
