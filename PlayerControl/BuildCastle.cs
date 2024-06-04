using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;


public class BuildCastle : NetworkBehaviour
{

    private InventoriManager _inventoriManager;

    [SerializeField]private List<Button> ButtonsList = new List<Button>();


    [SerializeField] private GameObject StoneWall;
    [SerializeField] private GameObject StoneFloor;
    [SerializeField] private GameObject StoneTower;

    private GameObject _castlePanel;

    [SerializeField] private GameObject CentreOfCastle;
    [SerializeField] private GameObject CentreOfCastleNormal;
    [SerializeField] private GameObject CentreOfCastleBig;

    private Button _buySmallCastle;
    private Button _buyNormalCastle;
    private Button _buyBigCastle;

    [SerializeField] private Transform SpavnTransform;

    private GameObject _buyCastlePanel;


    private bool _canBuild = false;

    [SerializeField] private ShoppingControl shoppingControl;

    private int _colorIndexBackSide;




    void Start()
    {
        _inventoriManager = FindObjectOfType<InventoriManager>();

        _castlePanel = _inventoriManager.castlePanel;

        _buySmallCastle = _inventoriManager.buySmallCastle;
        _buyNormalCastle = _inventoriManager.buyNormalCastle;
        _buyBigCastle = _inventoriManager.buyBigCastle;

        _buySmallCastle.onClick.AddListener(BuySmallCatle);
        _buyNormalCastle.onClick.AddListener(BuyNormalCatle);
        _buyBigCastle.onClick.AddListener(BuyBigCatle);

        _buyCastlePanel = _inventoriManager.BuyCastlePanel;

        ButtonsList = _inventoriManager.BuildMaterialsButton;

        ButtonsList[0].onClick.AddListener(Slot1ServerRpc);
        ButtonsList[1].onClick.AddListener(Slot2ServerRpc);
        ButtonsList[2].onClick.AddListener(Slot3ServerRpc);
    }


    private void BuySmallCatle()
    {
        
        if (_canBuild == false && shoppingControl.GetMoney() >= 1)
        {
            SpavnSmallCentreServerRpc();
            _castlePanel.SetActive(true);
            _buyCastlePanel.SetActive(false);
            shoppingControl.PlusMoney(-10);
        }
    }

    private void BuyNormalCatle()
    {
        if (_canBuild == false && shoppingControl.GetMoney() >= 20)
        {
            SpavnNormalCentreServerRpc();
            _castlePanel.SetActive(true);
            _buyCastlePanel.SetActive(false);
            shoppingControl.PlusMoney(-20);
        }
    }

    private void BuyBigCatle()
    {
        if (_canBuild == false && shoppingControl.GetMoney() >= 30)
        {
            SpavnBigCentreServerRpc();
            _castlePanel.SetActive(true);
            _buyCastlePanel.SetActive(false);
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
    
        Instantiate(CentreOfCastle, SpavnTransform.position, Quaternion.identity).GetComponent<NetworkObject>().Spawn();
    }

    [ServerRpc]
    private void SpavnNormalCentreServerRpc()
    {

        Instantiate(CentreOfCastleNormal, SpavnTransform.position, Quaternion.identity).GetComponent<NetworkObject>().Spawn();
    }

    [ServerRpc]
    private void SpavnBigCentreServerRpc()
    {

        Instantiate(CentreOfCastleBig, SpavnTransform.position, Quaternion.identity).GetComponent<NetworkObject>().Spawn();
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

    public void SetIndex(int index)
    {
        _colorIndexBackSide = index;
    }
}
