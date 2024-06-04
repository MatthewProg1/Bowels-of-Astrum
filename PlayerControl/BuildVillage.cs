using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class BuildVillage : NetworkBehaviour
{

    private InventoriManager _inventoriManager;

    [SerializeField] private List<Button> ButtonsList = new List<Button>();


    [SerializeField] private GameObject StoneWall;
    [SerializeField] private GameObject StoneFloor;
    [SerializeField] private GameObject WoodHouse;
    [SerializeField] private GameObject StoneTower;

    private GameObject _villagePanel;

    [SerializeField] private GameObject CentreOfVillage;
    [SerializeField] private GameObject CentreOfVillageNormal;
    [SerializeField] private GameObject CentreOfVillageBig;

    private Button _buySmallVillage;
    private Button _buyNormalVillage;
    private Button _buyBigVillage;

    [SerializeField] private Transform SpavnTransform;

    private GameObject _buyVillagePanel;


    private bool _canBuild = false;

    [SerializeField] private ShoppingControl shoppingControl;

    private int _colorIndexBackSide;

    private ShoppingControl _shoppingControl;




    void Start()
    {
        _inventoriManager = FindObjectOfType<InventoriManager>();

        _shoppingControl = GetComponent<ShoppingControl>();

        _villagePanel = _inventoriManager.villagePanel;

        _buySmallVillage = _inventoriManager.buySmallVillage;
        _buyNormalVillage = _inventoriManager.buyNormalVillage;
        _buyBigVillage = _inventoriManager.buyBigVillage;

        _buySmallVillage.onClick.AddListener(BuySmallVillage);
        _buyNormalVillage.onClick.AddListener(BuyNormalVillage);
        _buyBigVillage.onClick.AddListener(BuyBigVillage);

        _buyVillagePanel = _inventoriManager.BuyVillagePanel;

        ButtonsList = _inventoriManager.BuildMaterialsButtonVillage;

        ButtonsList[0].onClick.AddListener(Slot1ServerRpc);
        ButtonsList[1].onClick.AddListener(Slot2ServerRpc);
        ButtonsList[2].onClick.AddListener(Slot3ServerRpc);
        ButtonsList[3].onClick.AddListener(Slot4ServerRpc);
    }


    private void BuySmallVillage()
    {

        if (_canBuild == false && shoppingControl.GetMoney() >= 10)
        {
            SpavnSmallCentreServerRpc();
            _villagePanel.SetActive(true);
            _buyVillagePanel.SetActive(false);
            shoppingControl.PlusMoney(-10);
        }
    }

    private void BuyNormalVillage()
    {
        if (_canBuild == false && shoppingControl.GetMoney() >= 20)
        {
            SpavnNormalCentreServerRpc();
            _villagePanel.SetActive(true);
            _buyVillagePanel.SetActive(false);
            shoppingControl.PlusMoney(-20);
        }
    }

    private void BuyBigVillage()
    {
        if (_canBuild == false && shoppingControl.GetMoney() >= 30)
        {
            SpavnBigCentreServerRpc();
            _villagePanel.SetActive(true);
            _buyVillagePanel.SetActive(false);
            shoppingControl.PlusMoney(-30);
        }
    }
    public void SetCanBuild(bool can)
    {
        _canBuild = can;
    }




    [ServerRpc]
    private void SpavnSmallCentreServerRpc()
    {

        Instantiate(CentreOfVillage, SpavnTransform.position, Quaternion.identity).GetComponent<NetworkObject>().Spawn();
     //   centre.GetComponent<BuildCastle>().SetIndex(_colorIndexBackSide);
   

    }

    [ServerRpc]
    private void SpavnNormalCentreServerRpc()
    {

        Instantiate(CentreOfVillageNormal, SpavnTransform.position, Quaternion.identity).GetComponent<NetworkObject>().Spawn(); 
      //  centre.GetComponent<BuildCastle>().SetIndex(_colorIndexBackSide);

    }

    [ServerRpc]
    private void SpavnBigCentreServerRpc()
    {

        Instantiate(CentreOfVillageBig, SpavnTransform.position, Quaternion.identity).GetComponent<NetworkObject>().Spawn();
    }




    [ServerRpc]

    private void Slot1ServerRpc()
    {
        if (_canBuild && shoppingControl.GetMoney() >= 2)
        {
            var f = Instantiate(StoneWall, SpavnTransform.position, Quaternion.identity);
            f.transform.rotation = SpavnTransform.rotation;
            f.GetComponent<NetworkObject>().Spawn();
            shoppingControl.PlusMoney(-2);
        }
    }


    [ServerRpc]

    private void Slot2ServerRpc()
    {
        if (_canBuild && shoppingControl.GetMoney() >= 2)
        {
            var f = Instantiate(StoneFloor, SpavnTransform.position, transform.rotation);
            f.transform.rotation = Quaternion.Euler(0, 0, 90);

            f.GetComponent<NetworkObject>().Spawn();
            shoppingControl.PlusMoney(-2);
        }

    }

    [ServerRpc]

    private void Slot3ServerRpc()
    {
        if (_canBuild && shoppingControl.GetMoney() >= 1)
        {
            var f = Instantiate(StoneTower, SpavnTransform.position, transform.rotation);

            f.GetComponent<NetworkObject>().Spawn();

            shoppingControl.PlusMoney(-5);
        }

    }

    [ServerRpc]

    private void Slot4ServerRpc()
    {
        if (_canBuild && shoppingControl.GetMoney() >= 1)
        {
            var f = Instantiate(WoodHouse, SpavnTransform.position, transform.rotation);
            f.GetComponent<SmallHouse>().SetShoppingControl(shoppingControl);
      

            f.GetComponent<NetworkObject>().Spawn();

            shoppingControl.PlusMoney(-5);
        }

    }

    public void SetIndex(int index)
    {
        _colorIndexBackSide = index;
    }
}
