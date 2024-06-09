using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathHelper
{
    /// <summary>
    /// Static classes
    /// static classes are sealed, meaning they cannot be used in class inheritance and objects cannot be created from them. Cannot derive of MonoBehavior
    /// Used as containers for static memebers/variables, constructors and methods
    /// Static classes are often used to organize related utility methods, helper functions
    /// all members must be static
    /// Static classes cannot have instance fields or properties because they do not allow the creation of objects or instances.
    /// A static method belongs to the class itself rather than to any specific instance. It can be called on the class itself rather than on an instance of the class.
    /// Usage: Static classes are used when a class provides utility or helper methods that do not need to store any state
    /// </summary>

    private static float Pi = 3.14f;

    public static float Sum(float a, float b)
    {
        float result = a + b;
        return result;
    }

    public static int Sum(int a, int b)
    {
        return a + b;
    }

    public static float Square(float a)
    {
        return a * a;
    }

    public static float CircleArea(float radius)
    {
        return Pi * radius * radius;
    }

     public static float CircleArea(int radius)
    {
        return Pi * radius * radius;
    }

}


public class Car : MonoBehaviour
{
    /// <summary>
    /// Static variables /field belongs to the class itself rather than to any specific instance. It is shared among all instances of the class
    /// used to store data that is common to all instances of a class.
    /// Static variables are useful for maintaining shared state or data across instances of a class.
    /// This means that any change made to a static variable is visible to all instances, as they all refer to the same memory location.
    ///To access a static variable, you use the class name followed by a dot notation and the variable name.
    ///If you do not assign a value to a static variable explicitly, it will be initialized with the default value based on its data type. 
    /// </summary>

    private string model; 
    public static int numberOfCars; //belongs to the class rather than instances of the class. If it was not static and incremented in 

    public Car (string model) //costum constructur
    {
        this.model = model;
        numberOfCars ++; //increments numberOfCars everytime a Car is instantiated
    }
}

public class Flower : MonoBehaviour //EXAMPLE OF HOW IT WOULD WORK WITH NON-STATIC MEMBER
{
    private string family;
    public int numberOfFlowers;

    public Flower (string family) 
    {
        this.family = family;
        numberOfFlowers++;
    }
}

public class Example : MonoBehaviour
{
    void Start()
    {
        Car car1 = new Car("Mustang"); //create car
        Car car2 = new Car("Lambo"); //create car
        Debug.Log(Car.numberOfCars); // now the numberOfCars belongs to the class car, rather than instances like car1 and car2. Debugs  

        Flower flower1 = new Flower("Asteraceae");
        Flower flower2 = new Flower("Rosaceae");
        //Debug.Log(Flower.numberOfFlowers);  // Compiler error "An object reference is required for the non-static field, method, or property 'Flower.numberOfFlowers"

        Debug.Log(flower1.numberOfFlowers); //debugs 1
        Debug.Log(flower2.numberOfFlowers); //debugs 1 again, because the numberOfFlowers belongs to the instance and not the class.
    }
}
