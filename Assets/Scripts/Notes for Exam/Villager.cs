using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Villager : MonoBehaviour
{
    //Variables also known as class fields
    public string villagerName;
    public int healthPoints = 100;
    public string profession;

    public Villager() // costum constructor without parameter
    {
        villagerName = "not assigned";
        profession = "unknown";
    }

    public Villager(string villagerName, string profession) //costum constructor with parameter
    {
        this.villagerName = villagerName;
        this.profession = profession;
    }

    public virtual void printVillagerInfo() //class method, virtual allows to override the method in derived classes
    {
        Debug.LogFormat("Name: {0}, HealthPoints: {1}, Profession: {2}", this.villagerName, this.healthPoints, this.profession);

    }

    //encapsulation
    private void ResetVillagerInfo()
    {
        this.villagerName = "not assigned";
        this.healthPoints = 0;
        this.profession = "unknown";
    }
}
