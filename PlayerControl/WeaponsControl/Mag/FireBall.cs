using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class FireBall : NetworkBehaviour
{
    private Rigidbody _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.AddForce(15 * transform.forward, ForceMode.Impulse);
        Invoke(nameof(DestroyProjectile), 0.5f);
    }

    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
