using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoriManager : MonoBehaviour
{
    public List<Button> SlotsButtons = new List<Button>();

    public List<Button> BuildMaterialsButton = new List<Button>();

    public List<Button> BuildMaterialsButtonVillage = new List<Button>();


    public Button buySmallCastle;
    public Button buyNormalCastle;
    public Button buyBigCastle;

    public Button buySmallVillage;
    public Button buyNormalVillage;
    public Button buyBigVillage;

    public Button attackFire;
    public Button attackRock;

    public Button shootBalista;


    public Button SitDownOnTheHorse;
    public Button GetOffFromHorse;

    public Dropdown ClassList;
    public Dropdown iconGuildList1;

    public GameObject castlePanel;
    public GameObject villagePanel;

    public GameObject BuyCastlePanel;
    public GameObject BuyVillagePanel;


    public Text QuestName;
    public Text QuestDiscription;
    public Text QuestReward;


    public Button HeadArmor;
    public Button BodyArmor;
    public Button LegsArmor;


    public Button Craft;
    public Text CraftText;

    public Button Jump;
    public Button Kick1;
    public Button Kick2;




}
