using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Registration : MonoBehaviour
{
    [SerializeField] private InputField login;
    [SerializeField] private InputField password;

    [SerializeField] private Button join;

 

    int money = 0;

    private void Start()
    {
        join.onClick.AddListener(Register);
    }



    public void Register()
    {
        StartCoroutine(WorkWithServer());
    }



  

    IEnumerator WorkWithServer()
    {
        WWWForm form = new WWWForm();
        form.AddField("login", login.text);
        form.AddField("password", password.text);
        WWW req = new WWW("http://a0690060.xsph.ru/script/registration.php", form);
        yield return req;
        if(req.error != null)
        {
            Debug.Log(req.error);
            yield break;
        }

        Debug.Log(req.text);

      //  SceneManager.LoadScene("Menu");


    }

    


}
