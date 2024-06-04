using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryArmorObject : InventoryItemObject 
{
    [SerializeField] private float protectionPrecent;

    public float ProtectionPercent => protectionPrecent;
   
}
