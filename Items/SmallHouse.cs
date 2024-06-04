using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallHouse : MonoBehaviour
{
    private ShoppingControl shoppingControl;



    void Start()
    {
        Invoke(nameof(PlusMoney), 1);
    }

    private void PlusMoney()
    {
        shoppingControl.PlusMoney(10);
        Invoke(nameof(PlusMoney), 5);
    }

    public void SetShoppingControl(ShoppingControl shoppingControl)
    {
        this.shoppingControl = shoppingControl;
    }

    
}
