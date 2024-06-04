using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class CameraTurn : MonoBehaviour
{
    [SerializeField] private GameObject playerCamera;
    void Start()
    {
        if (!GetComponent<NetworkObject>().IsLocalPlayer)
        {
            playerCamera.SetActive(false);
            //GetComponent<PlayerControl>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(1, 1, 1);
    }
}
