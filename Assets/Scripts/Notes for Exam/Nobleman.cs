using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nobleman : Villager
{
    public Weapon weapon; //composition, accessing the classes methods and variables

    //Inheritance of base constrcutor from parent class and weapon struct
    public Nobleman(string villagerName, string profession, Weapon weapon): base(villagerName, profession)
    {
            this.weapon = weapon;
    }

    public override void printVillagerInfo() //Polymorphism, overriding a method from parent class
    {
        Debug.LogFormat("Name: {0}, Weapon equipped: {1}, Profession: {2}", this.villagerName, this.weapon.weaponName, this.profession);
    }

    //Instantiating Nobleman
     public GameObject prefabNobleman;
     public Vector3 spawnPosition1;
     public Quaternion spawnRotation1 = Quaternion.identity;

    // Start is called before the first frame update
    void Start()
    {
        spawnPosition1 = new Vector3(17,2,17);
        GameObject newNobleman = Instantiate(prefabNobleman, spawnPosition1, spawnRotation1);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
