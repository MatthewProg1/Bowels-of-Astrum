using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ItemsContanier : MonoBehaviour
{
    public static ItemsContanier instance;
    [SerializeField] private List<WeaponObject> weaponObjects;
    [SerializeField] private List<FurnitureObject> furnitureObjects;
    [SerializeField] private List<ArmorObject> armorObjects;
 
    
    private void Awake()
    {
        instance = this;
    }

    public WeaponObject GetWeapon(string name)
    {
        return weaponObjects.Where(x => x.ItemName == name).First();
    }
    public ArmorObject GetArmor(string name)
    {
        return armorObjects.Where(x => x.ItemName == name).First();
    }
    public FurnitureObject GetFurniture(string name)
    {
        return furnitureObjects.Where(x => x.ItemName == name).First();
    }

    //public ArmorObject GetArmor(string name)
    //{
    //    return armorObjects.Where(x => x.ItemName == name).First();
    //}
}
