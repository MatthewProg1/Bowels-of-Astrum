using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;


public class BodyDamage : MonoBehaviour
{
    [SerializeField] private int bodyPart;

    private PlayerControl _playerControl;

    private void Start()
    {
        _playerControl = GetComponentInParent<PlayerControl>();
    }

    public void ApplyDamage(int damage)
    {
        _playerControl.GetDamage(damage, bodyPart);
    }

 
}
