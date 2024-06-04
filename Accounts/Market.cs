using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class Market : MonoBehaviour
{
    // Start is called before the first frame update


    string login;
    public InputField price;
    public Text about;

    public GameObject element;
    public Transform parent;

    int apples;

    public Text money;

    public Text HPBonusCount;


    public void PostNewProduct()
    {

        for (int i = 0; i < parent.childCount; i++)
        {
            Destroy(parent.GetChild(i));
        }
            StartCoroutine(getApples(true));
    }

    IEnumerator OnPostNewProduct()
    {
        WWWForm form = new WWWForm();
        form.AddField("login", login);
        form.AddField("price", price.text);
        form.AddField("about", about.text);
        WWW req = new WWW("http://a0690060.xsph.ru/script/registrationproduct.php", form);
        yield return req;
        if (req.error != null)
        {
            Debug.Log(req.error);
            yield break;
        }

        Debug.Log(req.text);

       

        GetProducts();

    }

    IEnumerator getApples(bool minus)
    {
        WWWForm form = new WWWForm();
        form.AddField("login", login);
        //        Debug.Log(name.text);
        WWW req = new WWW("http://a0690060.xsph.ru/script/getapples.php", form);
        yield return req;
        if (req.error != null)
        {
            Debug.Log(req.error);
            yield break;
        }

        apples = int.Parse(req.text);
        HPBonusCount.text = apples.ToString();

        // Debug.Log(req.text);
        if (apples > 0 && minus)
        {
            apples--;
            HPBonusCount.text = apples.ToString();
            StartCoroutine(changeApples());
        }
      
    }

    IEnumerator changeApples()
    {
        WWWForm form = new WWWForm();
        form.AddField("login", login);
        form.AddField("apples", apples);
        WWW req = new WWW("http://a0690060.xsph.ru/script/changeapples.php", form);
        yield return req;
        if (req.error != null)
        {
            Debug.Log(req.error);
            yield break;
        }
        StartCoroutine(OnPostNewProduct());

        PlayerPrefs.SetInt("SaveLive", apples);
        // Market.DeleteElement();


    }





    public void GetProducts()
    {
        StartCoroutine(OnGetProducts());
    }


    public IEnumerator OnGetProducts()
    {
        foreach (Transform child in parent)
        {
            Destroy(child.gameObject);
        }


        WWW req = new WWW("http://a0690060.xsph.ru/script/getproduct.php");
        yield return req;
        Debug.Log(req.text);
        List<string> leaders = req.text.Split('!').ToList();
        leaders.Remove(leaders.Last());
        foreach (var item in leaders)
        {

            Product l = JsonUtility.FromJson<Product>(item);

            var el = Instantiate(element, parent);
            el.GetComponentInChildren<Text>().text = $"{l.name};{l.about};{l.price}";


        }



    }
    void Start()
    {
        login = PlayerPrefs.GetString("PlayerNameAccount");

        GetProducts();

        StartCoroutine(getApples(false));

        money.text = PlayerPrefs.GetInt("SaveMoney2").ToString();


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}

public class Product
{
    public string name;
    public string about;
    public string price;
}
