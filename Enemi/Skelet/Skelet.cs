using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using System;

public class Skelet : NetworkBehaviour
{
    [SerializeField] private bool _attack = false;
    [SerializeField] private PlayerControl _target;

    private NetworkVariable<int> _hp = new NetworkVariable<int>(20);
    [SerializeField] private Slider _HPBar;

    [SerializeField] private InventoryItemObject meat;

    private GameObject _player;


    private void OnEnable()
    {
        _hp.OnValueChanged += ChangeHPValue;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       // if (!IsLocalPlayer) return;

        if (_attack)
        {
            transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, 3f * Time.deltaTime);

            transform.LookAt(_target.transform.position);
        }

        
    }



    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            _attack = true;
            _target = other.gameObject.GetComponentInParent<PlayerControl>();
            _player = other.gameObject;
            //ChageHpValueServerRpc(10);
        }
    }


    public void OnCollisionEnter(Collision collision)
    {
     //   collision.gameObject.GetComponentInChildren<BodyDamage>()?.ApplyDamage(10);
        collision.collider.gameObject.GetComponent<BodyDamage>()?.ApplyDamage(10);

        if(collision.gameObject.tag == "StartSword")
        {
            ChageHpValueServerRpc(5);
    
        }

        if(collision.gameObject.tag == "BigSpear")
        {
            ChageHpValueServerRpc(20);
        }

        if (collision.gameObject.tag == "Horse")
        {
            ChageHpValueServerRpc(1);

        }
    }

    private void ChangeHPValue(int previousValue, int newValue)
    {
        _HPBar.value = newValue;
    }

    [ServerRpc(RequireOwnership = false)]
    private void ChageHpValueServerRpc(int damage)
    {
        _hp.Value -= damage;
        if(_hp.Value <= 0)
        {
            _player.GetComponentInParent<ShoppingControl>().PlusMoney(10);
            _player.GetComponentInParent<InventoriControl>().AddItem(meat);

            var quest = _player.GetComponentInChildren<QuestController>();
            quest.SearchKillingQuests(gameObject);
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        _hp.OnValueChanged -= ChangeHPValue;
    }
}
