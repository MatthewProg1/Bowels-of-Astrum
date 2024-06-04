using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

public class BallistaControl : NetworkBehaviour
{
    private UIcontrol _uIcontrol;

    private Button _shootBallista;

    private GameObject _ballista;

    private Transform _shootPos;

    [SerializeField] private GameObject shootItem;

    void Start()
    {
        _uIcontrol = GetComponent<UIcontrol>();

        _shootBallista = _uIcontrol.shootBallista;

        _shootBallista.onClick.AddListener(Shoot);
        
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ballista")
        {
            if (!IsLocalPlayer)
            {
                return;
            }
            _shootPos = collision.gameObject.transform;
            _ballista = collision.gameObject;
            _shootBallista.gameObject.SetActive(true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ballista")
        {
            if (!IsLocalPlayer)
            {
                return;
            }

            _shootPos = null;
            _shootBallista.gameObject.SetActive(false);
        }
    }

  
    private void Shoot()
    {
        if (!IsLocalPlayer)
        {
            return;
        }
        _ballista.GetComponent<Ballista>().ShootServerRpc();

    }


}
