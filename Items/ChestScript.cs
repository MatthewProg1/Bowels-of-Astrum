using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class ChestScript : NetworkBehaviour
{
    [SerializeField] private List<InventoryItemObject> _items = new List<InventoryItemObject>(10);
    [SerializeField] private List<int> _itemCount = new List<int>(10);


   public void AddToChestInventory(InventoryItemObject item)
    {
        if (!IsLocalPlayer)
            return;

        for (int i = 0; i < _items.Count; i++)
        {
            if (_items[i] == item || _items[i] == null)
            {
                Debug.Log(item);
                Debug.Log(i);
                Debug.Log(item.ItemImage);

                _items[i] = item;
               // _inventoriManager.SlotsButtons[i].image.sprite = item.ItemImage;
                _itemCount[i]++;
                //   questController.SearchTakingQuests(item);
                break;
            }
        }
    }
}
