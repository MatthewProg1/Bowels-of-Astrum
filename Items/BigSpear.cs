using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class BigSpear : NetworkBehaviour
{
    
    void Start()
    {
        if (!IsLocalPlayer)
        {
            return;
        }


        Invoke(nameof(Destroy), 3);
    }

    private void Update()
    {

    }


    private void Destroy()
    {
        Destroy(gameObject);
    }
}
