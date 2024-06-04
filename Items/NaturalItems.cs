using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class NaturalItems : NetworkBehaviour
{
    private int hp = 50;
    [SerializeField] private InventoryItemObject resource;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponentInParent<InventoriControl>())
        {
            Debug.Log("AAAAAAAAAAAA");
        
            hp -= 10;
            if(hp <= 0)
            {
                gameObject.SetActive(false);
                collision.gameObject.GetComponentInParent<InventoriControl>().AddItem(resource);
            }
        }
    }
}
