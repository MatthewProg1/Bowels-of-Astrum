using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Horse : NetworkBehaviour
{
    private int _hp = 70;
    public bool IsRiding;
    public Transform player;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (!IsLocalPlayer)
        //    return;

        if (IsRiding == true)
        {
            transform.position = player.position - new Vector3(0, 1.2f, 0);
            transform.rotation = player.rotation;
        }
    }
}
