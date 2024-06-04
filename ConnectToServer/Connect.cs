using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.Netcode;

public class Connect : MonoBehaviour
{
    public InputField _nick;

    [SerializeField] private Joystick _joystick;
    [SerializeField] private Button _server;
    [SerializeField] private Button _host;
    [SerializeField] private Button _client;
    [SerializeField] private Dropdown _classList;
    [SerializeField] private Dropdown _guildColoBackSide;
    public void StartHost()
    {
        NetworkManager.Singleton.StartHost();
        SwitchOffButtons();
 //       if (!IsOwner) return;
      //  PlayerPrefs.SetString("PlayerNickName", _nick.text);
 //       NetworkManager.SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }
    public void StartServer()
    {
        NetworkManager.Singleton.StartServer();
        SwitchOffButtons();
       // if (!IsOwner) return;
     //   PlayerPrefs.SetString("PlayerNickName", _nick.text);
        //  NetworkManager.SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);

    }
    public void StartClient()
    {
        NetworkManager.Singleton.StartClient();
        SwitchOffButtons();
     //   if (!IsOwner) return;
      //  PlayerPrefs.SetString("PlayerNickName", _nick.text);
        //   NetworkManager.SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }

    //private void Start()
    //{
    //    PlayerPrefs.SetString("PlayerNickName", "");
    //}

    private void SwitchOffButtons()
    {
     //   if (!IsLocalPlayer) return;
        _server.gameObject.SetActive(false);
        _host.gameObject.SetActive(false);
        _client.gameObject.SetActive(false);
        _joystick.gameObject.SetActive(true);
        StartCoroutine(TurnOffClassList());
    }

    private IEnumerator TurnOffClassList()
    {
        yield return new WaitForSeconds(0.1f);
        _classList.gameObject.SetActive(false);
     //   _guildColoBackSide.gameObject.SetActive(false);
    }


}
