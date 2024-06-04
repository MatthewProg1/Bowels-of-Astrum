using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraiderBase : ScriptableObject
{
    [SerializeField] private List<InventoryItemObject> itemObjects;
    [SerializeField] private List<int> itemPrices;
    [SerializeField] private string nameTraider;

    public List<InventoryItemObject> ItemObjects => itemObjects;
    public List<int> ItemPrices => itemPrices;
    public string NameTraider => nameTraider;



}
