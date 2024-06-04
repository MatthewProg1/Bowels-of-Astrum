using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class WallHP : MonoBehaviour
{
   [SerializeField] private NetworkVariable<int> _hp = new NetworkVariable<int>(25);


    private void OnEnable()
    {
        _hp.OnValueChanged += ChangeGPValue;
    }


    void Start()
    {

    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "StartSword")
        {
            GetDamageServerRpc(2);
        }

        if (collision.gameObject.tag == "BigSpear")
        {
            GetDamageServerRpc(15);
        }
    }


    private void ChangeGPValue(int previousValue, int newValue)
    {

    }


    [ServerRpc]

    private void GetDamageServerRpc(int damage)
    {
        _hp.Value -= damage;

        if (_hp.Value <= 0)
        {
            Destroy(gameObject);

        }
    }

    private void OnDisable()
    {
        _hp.OnValueChanged -= ChangeGPValue;
    }
}
