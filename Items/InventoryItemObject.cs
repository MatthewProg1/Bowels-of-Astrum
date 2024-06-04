using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemObject : ScriptableObject
{
    [SerializeField] private string itemName;
    [SerializeField] private Sprite itemImage;

    [SerializeField] private GameObject itemPrefab;

    [SerializeField] private bool isFurniture;


    public GameObject ItemPrefab => itemPrefab;

    public string ItemName => itemName;
    public Sprite ItemImage => itemImage;

    public bool IsFurniture => isFurniture;




}
