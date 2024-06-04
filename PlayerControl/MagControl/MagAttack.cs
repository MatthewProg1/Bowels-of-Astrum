using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class MagAttack : NetworkBehaviour
{
    [SerializeField] private GameObject stone;
    [SerializeField] private GameObject fire;
    [SerializeField] private GameObject fireBall;
    [SerializeField] private Transform shootPosition;
    private UIcontrol _uicontrol;

    public Button _attackFire;
    public Button _attackRock;

    private GameObject m_PrefabInstance;
    private NetworkObject m_SpawnedNetworkObject;


    private int _magickIndex = 0;

    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RockShoot()
    {
        if (_magickIndex == 2)
        {
            _magickIndex = 3;
        }
        else
        {
            _magickIndex = 1;
        }
        StartCoroutine(WaitSecondTouch());
    }

    public void FireShoot()
    {
        if (_magickIndex == 1)
        {
            _magickIndex = 3;
        }
        else
        {
            _magickIndex = 2;
        }
        StartCoroutine(WaitSecondTouch());

    }


    private void MakeRock()
    {
        enabled = IsServer;
        if (!enabled || stone == null)
        {
            return;
        }
        // Instantiate the GameObject Instance
        m_PrefabInstance = Instantiate(stone);

        // Optional, this example applies the spawner's position and rotation to the new instance
        m_PrefabInstance.transform.position = transform.position;
        m_PrefabInstance.transform.rotation = transform.rotation;

        // Get the instance's NetworkObject and Spawn
        m_SpawnedNetworkObject = m_PrefabInstance.GetComponent<NetworkObject>();
        m_SpawnedNetworkObject.Spawn();
        _magickIndex = 0;
    }


    private void MakeFire()
    {
        enabled = IsServer;
        if (!enabled || fire == null)
        {
            return;
        }
        // Instantiate the GameObject Instance
        m_PrefabInstance = Instantiate(fire);

        // Optional, this example applies the spawner's position and rotation to the new instance
        m_PrefabInstance.transform.position = transform.position;
        m_PrefabInstance.transform.rotation = transform.rotation;

        // Get the instance's NetworkObject and Spawn
        m_SpawnedNetworkObject = m_PrefabInstance.GetComponent<NetworkObject>();
        m_SpawnedNetworkObject.Spawn();
        _magickIndex = 0;
    }

 
    private void MakeFireBall()
    {
        enabled = IsServer;
        if (!enabled || fireBall == null)
        {
            return;
        }
        // Instantiate the GameObject Instance
        m_PrefabInstance = Instantiate(fireBall);

        // Optional, this example applies the spawner's position and rotation to the new instance
        m_PrefabInstance.transform.position = transform.position;
        m_PrefabInstance.transform.rotation = transform.rotation;

        // Get the instance's NetworkObject and Spawn
        m_SpawnedNetworkObject = m_PrefabInstance.GetComponent<NetworkObject>();
        m_SpawnedNetworkObject.Spawn();
        _magickIndex = 0;
    }


    IEnumerator WaitSecondTouch()
    {
        yield return new WaitForSeconds(0.3f);
        switch (_magickIndex)
        {
            case 1:
                MakeRock();
                break;
            case 2:
                MakeFire();
                break;
            case 3:
                MakeFireBall();
                break;

        }

        _magickIndex = 0;
    }

}
