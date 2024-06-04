using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Join : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private InputField login;
    [SerializeField] private InputField password;

    [SerializeField] private Button join;

    [SerializeField] private Text name;
    [SerializeField] private Text moneyText;

    int money;

    public void OnClick()
    {
        

    }

    private void Start()
    {
     //   join = GameObject.FindGameObjectWithTag("JoinButton").GetComponent<Button>();
        join.onClick.AddListener(Joining);
    }





    public void Save()
    {
        StartCoroutine(changemoney());
    }

    public void Joining()
    {
        Debug.Log("Cliked");
        StartCoroutine(joining());
       
    }

    IEnumerator joining()
    {
        WWWForm form = new WWWForm();
        form.AddField("login", login.text);
        form.AddField("password", password.text);
        WWW req = new WWW("http://a0690060.xsph.ru/script/join.php", form);
        yield return req;
        if (req.error != null)
        {
            Debug.Log(req.error);
            yield break;
        }
        if (req.text != "Error")
        {
            name.text = req.text;
            PlayerPrefs.SetString("PlayerNameAccount", name.text);
            StartCoroutine(getMoney());

            yield return new WaitForSeconds(1);
          //  SceneManager.LoadScene("Menu");
        }



    }

    IEnumerator getMoney()
    {
        WWWForm form = new WWWForm();
        form.AddField("login", name.text);
        Debug.Log(name.text);
        WWW req = new WWW("http://a0690060.xsph.ru/script/getmoney.php", form);
        yield return req;
        if (req.error != null)
        {
            Debug.Log(req.error);
            yield break;
        }

        money = int.Parse(req.text);

        PlayerPrefs.SetInt("SaveMoney2", money);

       

        moneyText.text = money.ToString();
    }

    IEnumerator changemoney()
    {
        WWWForm form = new WWWForm();
        form.AddField("login", name.text);
        form.AddField("money", money);
        WWW req = new WWW("http://a0690060.xsph.ru/script/changemoney.php", form);
        yield return req;
        if (req.error != null)
        {
            Debug.Log(req.error);
            yield break;
        }

        Debug.Log(req.text);

    }


}
