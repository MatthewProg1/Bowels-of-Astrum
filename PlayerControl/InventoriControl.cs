using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

public class InventoriControl : NetworkBehaviour
{
    //  [SerializeField] private List<GameObject> _items = new List<GameObject>();
    [SerializeField] private List<InventoryItemObject> _items;

    [SerializeField] private List<InventoryItemObject> _armor;

    [SerializeField] private List<int> _itemCount = new List<int>();

    [SerializeField] private Arm arm;

    private InventoryItemObject _curentObject;

    [SerializeField] private GameObject Spear;

    [SerializeField] private QuestController questController;

    private GameObject none;

    private InventoriManager _inventoriManager;


    [SerializeField] private Sprite meat;

  
    void Start()
    {
        if (!IsLocalPlayer)
            return;


        _inventoriManager = FindObjectOfType<InventoriManager>();

        _inventoriManager.HeadArmor.onClick.AddListener(() => ChangeArmorHead(0));
        _inventoriManager.BodyArmor.onClick.AddListener(() => ChangeArmorHead(1));
        _inventoriManager.LegsArmor.onClick.AddListener(() => ChangeArmorHead(2));

        AddItem(ItemsContanier.instance.GetWeapon("Spear"));
        AddItem(ItemsContanier.instance.GetWeapon("Sword"));
        AddItem(ItemsContanier.instance.GetFurniture("Chest"));

   
     //   AddItem(ItemsContanier.instance.GetWeapon("TestArmorBody"));


        for (int i = 0; i < _inventoriManager.SlotsButtons.Count; i++)
        {
            int j = i;
            _inventoriManager.SlotsButtons[i].onClick.AddListener(() => OnClickSlot(j));
            Debug.Log(i);

        }
    }

    private void OnClickSlot(int i)
    {
        if (!IsLocalPlayer)
            return;

        _curentObject = _items[i];
        Debug.Log(_curentObject);

        if (_items[i].IsFurniture)
        {
            var it = Instantiate(_items[i].ItemPrefab);
            it.GetComponent<NetworkObject>().Spawn();
        }

        if (_items[i] == null)
        {
            arm.SetFalseList();
        }
        else
        {
            arm.SetItem(_items[i].ItemPrefab, _items[i].ItemName, _items[i]);
        }

    

    }
 

    public void AddItem(InventoryItemObject item)
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
                _inventoriManager.SlotsButtons[i].image.sprite = item.ItemImage;
                _itemCount[i]++;

             //   questController.SearchTakingQuests(item);

              
                break;
            }
        }
    }

    public void MinusItem(InventoryItemObject item)
    {
        if (!IsLocalPlayer) return;

        for (int i = 0; i < _items.Count; i++)
        {
            if (_items[i] == item)
            {

                _itemCount[i]--;

                //   questController.SearchTakingQuests(item);
                if (_itemCount[i] == 0)
                {
                    _items[i] = null;
                    _inventoriManager.SlotsButtons[i].image.sprite = null;
                }

                break;
            }
        }

    }

    public void ChangeArmorHead(int index)
    {

        if (_curentObject != null)
        {
            if (_armor[index] != null)
            {
                AddItem(_armor[index]);
                _armor[index] = _curentObject;

            }
            else
            {
                _armor[index] = _curentObject;
            }

            switch (index)
            {
                case 0:
                    Debug.Log(_curentObject.ItemImage);
                    _inventoriManager.HeadArmor.image.sprite = _curentObject.ItemImage;
                    break;
                case 1:
                    _inventoriManager.BodyArmor.image.sprite = _curentObject.ItemImage;
                    break;
                case 2:
                    _inventoriManager.LegsArmor.image.sprite = _curentObject.ItemImage;
                    break;

            }
        }
      
    }

    public void Search(InventoryItemObject obj, int count, InventoryItemObject product)
    {
        for (int i = 0; i < _items.Count; i++)
        {
            if(obj == _items[i])
            {
                count--;
                _itemCount[i]--;
                
            }
        }

        if(count == 0)
        {
            AddItem(product);
        }
    }

   
}
