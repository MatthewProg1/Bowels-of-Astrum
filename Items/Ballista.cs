using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Ballista : NetworkBehaviour
{
    private GameObject _player;
    [SerializeField] private GameObject projectilePrefab;

    // Update is called once per frame
    void Update()
    {

        if (_player != null)
        {
            transform.rotation = _player.transform.rotation;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (!IsLocalPlayer)
            {
                return;
            }

            _player = collision.gameObject;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!IsLocalPlayer)
            {
                return;
            }

            _player = null;
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void ShootServerRpc()
    {

        var patron = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        patron.transform.rotation = transform.rotation;
 
        patron.GetComponent<Rigidbody>().AddForce(0, 0, 80, ForceMode.Impulse);
        patron.GetComponent<NetworkObject>().Spawn();
    }
}
