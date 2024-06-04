using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crafting : MonoBehaviour
{
    [SerializeField] private UIcontrol uIcontrol;
    [SerializeField] private InventoriControl inventori;
    [SerializeField] private InventoryItemObject bread;
    [SerializeField] private InventoryItemObject meat;
    [SerializeField] private InventoriManager inventoriManager;

    private void Start()
    {
        uIcontrol.GetInventoryManager().Craft.onClick.AddListener(Craft);
    }

    private void Craft()
    {
        switch (uIcontrol.GetInventoryManager().CraftText.text)
        {
            case "Bread": Debug.Log(inventori); inventori.Search(meat, 1, bread) ; break;
        }
    }
}
