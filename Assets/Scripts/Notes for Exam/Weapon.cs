using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Xml;
using System.Xml.Serialization;

[Serializable]
public struct Weapon
{
    public string weaponName;
    public int weaponDamage;

    public Weapon(string weaponName, int weaponDamage) // costum constructor
    {
        this.weaponName = weaponName;
        this.weaponDamage = weaponDamage;
    }

    public void PrintWeaponStats() //struct method
    {
        Debug.LogFormat("Weapon: {0}, Damage: {1}", this.weaponName, this.weaponDamage);
    }
}

[Serializable]
public class WeaponShop
{
    public List<Weapon> inventory;
}
