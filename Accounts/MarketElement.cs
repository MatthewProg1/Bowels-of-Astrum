using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketElement : MonoBehaviour
{
    // Start is called before the first frame update

    public Text element;
    string name;
    int money;
    int moneyShopper;

    int apples;

    public Text applesText;

    int pr;

    public string ShopperName;
    public string price;

    Market Market;

    Text moneyText;
    private void Awake()
    {
        moneyText = GameObject.FindGameObjectWithTag("Money").GetComponent<Text>();
        applesText = GameObject.FindGameObjectWithTag("Apple").GetComponent<Text>();

    }


    public void OnBuy()
    {
        Market = GameObject.FindGameObjectWithTag("Market").GetComponent<Market>();
        name = PlayerPrefs.GetString("PlayerNameAccount");


        StartCoroutine(getMoney());
    }

    IEnumerator Buy()
    {
        string[] leaders = element.text.Split(';');
        foreach (var item in leaders)
        {
            Product l = new Product()
            {
                name = leaders[0],
                about = leaders[1],
                price = leaders[2]
            };

            ShopperName = $"{l.name}";
            string about = $"{l.about}";
            price = $"{l.price}";

            pr = int.Parse(price);

            Debug.Log(pr);

            // Debug.Log(el);

        }

        if (money >= pr && ShopperName != name)
        {
            money -= pr;
            moneyText.text = money.ToString();
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(changeMoney());
        }
    }

    IEnumerator getMoney()
    {
        WWWForm form = new WWWForm();
        form.AddField("login", name);
        Debug.Log(name);
        WWW req = new WWW("http://a0690060.xsph.ru/script/getmoney.php", form);
        yield return req;
        if (req.error != null)
        {
            Debug.Log(req.error);
            yield break;
        }
        money = int.Parse(req.text);
        StartCoroutine(Buy());

     
    }

    IEnumerator changeMoney()
    {
        WWWForm form = new WWWForm();
        form.AddField("login", name);
        form.AddField("money", money);
        WWW req = new WWW("http://a0690060.xsph.ru/script/changemoney.php", form);
        yield return req;
        if (req.error != null)
        {
            Debug.Log(req.error);
            yield break;
        }

    //    Debug.Log(req.text);

        StartCoroutine(getApples());

    }

    IEnumerator getApples()
    {
        WWWForm form = new WWWForm();
        form.AddField("login", name);
//        Debug.Log(name.text);
        WWW req = new WWW("http://a0690060.xsph.ru/script/getapples.php", form);
        yield return req;
        if (req.error != null)
        {
            Debug.Log(req.error);
            yield break;
        }

         apples = int.Parse(req.text);
       // Debug.Log(req.text);
        apples++;
        applesText.text = apples.ToString();
        StartCoroutine(changeApples());
    }

    IEnumerator changeApples()
    {
        WWWForm form = new WWWForm();
        form.AddField("login", name);
        form.AddField("apples", apples);
        WWW req = new WWW("http://a0690060.xsph.ru/script/changeapples.php", form);
        yield return req;
        if (req.error != null)
        {
            Debug.Log(req.error);
            yield break;
        }
        StartCoroutine(getMoneyShopper());
     

       // Market.DeleteElement();

        
    }

    IEnumerator OnDeleteElement()
    {
        WWWForm form = new WWWForm();
        form.AddField("login", ShopperName);
        form.AddField("price", price);
        WWW req = new WWW("http://a0690060.xsph.ru/script/deleteelement.php", form);
        yield return req;
        if (req.error != null)
        {
            Debug.Log(req.error);
            yield break;
        }

        Debug.Log(req.text);

        Market.GetProducts();

      

    }

    IEnumerator getMoneyShopper()
    {
        WWWForm form = new WWWForm();
        form.AddField("login", ShopperName);
    //    Debug.Log(name);
        WWW req = new WWW("http://a0690060.xsph.ru/script/getmoney.php", form);
        yield return req;
        if (req.error != null)
        {
            Debug.Log(req.error);
            yield break;
        }
        Debug.Log(req.text);
        moneyShopper = int.Parse(req.text);
        moneyShopper = moneyShopper + pr;
        Debug.Log(moneyShopper);
        
        StartCoroutine(changeMoneyShopper());


    }

    IEnumerator changeMoneyShopper()
    {
        WWWForm form = new WWWForm();
        form.AddField("login", ShopperName);
        form.AddField("money", moneyShopper);
        WWW req = new WWW("http://a0690060.xsph.ru/script/changemoney.php", form);
        yield return req;
        if (req.error != null)
        {
            Debug.Log(req.error);
            yield break;
        }

           Debug.Log(req.text);

        StartCoroutine(OnDeleteElement());
    }
}


