using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoppingControl : MonoBehaviour
{
    [SerializeField] private int _money = 100;

    [SerializeField]private UIcontrol _uiControl;
        
    void Start()
    {
        
    }
 
    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlusMoney(int countPlusMoney)
    {
        _money += countPlusMoney;
        _uiControl.UpdateTextMoney(_money);
    }

    public int GetMoney()
    {
        return _money;
    }
}
