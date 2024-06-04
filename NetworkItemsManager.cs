using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class NetworkItemsManager : NetworkBehaviour
{
    public List<InventoryItemObject> inventoryItems;

    public void SpawnItem(string itemName)
    {
        if (!IsServer) 
            return;

        var item = Instantiate(inventoryItems.Where(x => x.ItemName == itemName).First());
        item.GetComponent<NetworkObject>().Spawn();
    }
}
