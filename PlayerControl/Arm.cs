using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System.Linq;

public class Arm : NetworkBehaviour
{
    [SerializeField] private NetworkObject _item;

    private string currentItemName = "";

    private InventoryItemObject currentObject;

    [SerializeField] private NetworkItemsManager networkItemsManager;




    [SerializeField] private Dictionary<string, GameObject> _items = new Dictionary<string, GameObject>();

    [SerializeField] private PlayerControl playerControl;
    [SerializeField] private InventoriControl inventoriControl;

    private void Start()
    {
        networkItemsManager = FindObjectOfType<NetworkItemsManager>();
    }

    public void SetItem(GameObject item, string itemName, InventoryItemObject inventoryItemObject)
    {
        SetFalseList();

        currentItemName = itemName;
        currentObject = inventoryItemObject;

        

        if (_items.ContainsKey(itemName))
        {
            _items[itemName].SetActive(true);
            
        }
        else
        {

            if (IsClient)
            {
                SpawnServerRpc(currentItemName);
            }
            
        }


        UseItem();
    }


    public void SetFalseList()
    {
        foreach (var item in _items)
        {
            item.Value.SetActive(false);
        }
    }

    public void UseItem()
    {
        Debug.Log(currentItemName);
        if(currentItemName == "Meat" || currentItemName == "Bread")
        {
            playerControl.GetDamage(-10, 3);
            inventoriControl.MinusItem(currentObject);

        }
    }

    [ServerRpc]
    private void SpawnServerRpc(string itemName)
    {

        var item = Instantiate(networkItemsManager.inventoryItems.Where(x => x.ItemName == itemName).First().ItemPrefab, _item.transform);
        var it = item.GetComponent<NetworkObject>();

        it.Spawn();
        it.TrySetParent(_item);

        SpawnClientRpc(it.NetworkObjectId, _item.NetworkObjectId);
    }

    [ClientRpc]
    private void SpawnClientRpc(ulong objectId, ulong parentId)
    {
        var obj = NetworkManager.SpawnManager.SpawnedObjects[objectId];
        var objParent = NetworkManager.SpawnManager.SpawnedObjects[parentId];
        obj.transform.parent = objParent.transform;
    }



}
