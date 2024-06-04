using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AccountsMenu : MonoBehaviour
{
    public Image panel;

    public void OnRegistrate()
    {
        SceneManager.LoadScene("RegistrateScene");
    }

    public void OnJoinAccount()
    {
        SceneManager.LoadScene("JoinScene");
    }

    public void OnClose()
    {
        panel.gameObject.SetActive(false);
    }


    public void OnOpen()
    {
        panel.gameObject.SetActive(true);
    }
}
