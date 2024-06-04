using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using System;

public class CentreControl : NetworkBehaviour
{
    [SerializeField]  private NetworkVariable<int> _hp = new NetworkVariable<int>(50);
    [SerializeField] private Slider HPBar;

    [SerializeField] private Text HPText;

    private int _iconGuildBackSide;

    private void OnEnable()
    {
        _hp.OnValueChanged += ChangeGPValue;
    }


    void Start()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "StartSword")
        {
            GetDamageServerRpc(5);
        }

        if (collision.gameObject.tag == "BigSpear")
        {
            GetDamageServerRpc(25);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponentInParent<BuildCastle>().SetCanBuild(true);
            other.gameObject.GetComponentInParent<BuildVillage>().SetCanBuild(true);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponentInParent<BuildCastle>().SetCanBuild(false);
            other.gameObject.GetComponentInParent<BuildVillage>().SetCanBuild(false);
        }
    }

    private void ChangeGPValue(int previousValue, int newValue)
    {
        HPBar.value = newValue;
        HPText.text = newValue.ToString();
    }


    [ServerRpc(RequireOwnership = false)]

    private void GetDamageServerRpc(int damage)
    {
        _hp.Value -= damage;

        if(_hp.Value <= 0)
        {
            Destroy(gameObject);

        }
    }

    public void SetIconGuildBackSide(int index)
    {
        _iconGuildBackSide = index;
    }

    private void OnDisable()
    {
        _hp.OnValueChanged -= ChangeGPValue;
    }

}
