using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class HorseControl : NetworkBehaviour
{
    private bool _isOnHorse = false;
    [SerializeField]private Horse _horse;

    private PlayerControl _playerControl;

    [SerializeField] private Transform playerModel;

    void Start()
    {
        _playerControl = GetComponent<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Horse")
        {
            _horse = collision.gameObject.GetComponent<Horse>();
            
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Horse")
        {
            _horse = null;
        }
    }


    public void SitOnHorse()
    {
        if(_horse != null && _isOnHorse == false)
        {
            _isOnHorse = true;
            _horse.IsRiding = true;
            _horse.player = gameObject.transform;

            _playerControl.AddSpeed(0.12f);         
        }
    }

    public void GetOffHorse()
    {
        if (_isOnHorse)
        {


     //       _horse.player = _horse.transform;
            _horse.IsRiding = false;
            _isOnHorse = false;

            //  _horse = null;
            _playerControl.AddSpeed(-0.12f);
        }
    }


}
