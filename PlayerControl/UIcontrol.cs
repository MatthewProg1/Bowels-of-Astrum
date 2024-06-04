using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
using Unity.Collections;
using System;

public class UIcontrol : NetworkBehaviour
{
    private Slider _HPbarBody;
    private Slider _HPbarHead;
    private Slider _HPbarArms;
    private Slider _HPbarLegs;
    private Slider _HPbarLegs2;
    private Text _hpText;

    private Button _jump;
    private Button _kick;
    private Button _kickDirect;

    private Button _sitOnTheHorse;
    private Button _getOffFromHorse;

    private PlayerControl _playerControl;

    [SerializeField] private Text _nick;

    [SerializeField] private MagAttack magAttack;

    private InventoriManager _inventoriManager;

    private HorseControl _horseControl;

    public Button shootBallista;

    private Dropdown iconGuildList1;

    private Text _money;

    private string _nickValue;

    public Dropdown _classList;

    private NetworkVariable<FixedString64Bytes> _nickString = new NetworkVariable<FixedString64Bytes>();

    

    private void OnEnable()
    {
        _nickString.OnValueChanged += SetNick;
    }
    private void Awake()
    {
        _playerControl = GetComponent<PlayerControl>();
    }
    void Start()
    {

        _HPbarBody = GameObject.FindGameObjectWithTag("HPBarBody")
            .GetComponent<Slider>();
        _HPbarHead = GameObject.FindGameObjectWithTag("HPBarHead")
               .GetComponent<Slider>();
        _HPbarLegs = GameObject.FindGameObjectWithTag("HPBarLegs")
               .GetComponent<Slider>();
        _HPbarLegs2 = GameObject.FindGameObjectWithTag("HPBarLegs2")
               .GetComponent<Slider>();
        //_HPbarArms = GameObject.FindGameObjectWithTag("HPBarArms")
        //       .GetComponent<Slider>();

        //_hpText = GameObject.FindGameObjectWithTag("HPText")
        //    .GetComponent<Text>();
        //_jump = GameObject.FindGameObjectWithTag("JumpButton")
        //    .GetComponent<Button>();
  
        _money = GameObject.FindGameObjectWithTag("Money")
            .GetComponent<Text>();
 

        _inventoriManager = FindObjectOfType<InventoriManager>();

        _classList = _inventoriManager.ClassList;

        iconGuildList1 = _inventoriManager.iconGuildList1;

        _jump = _inventoriManager.Jump;


       // _jump.onClick.AddListener(() => _playerControl.Jump());

        _kick = _inventoriManager.Kick1;
        _kickDirect = _inventoriManager.Kick2;


        magAttack = GetComponent<MagAttack>();

        _horseControl = GetComponent<HorseControl>();

        _sitOnTheHorse = _inventoriManager.SitDownOnTheHorse;
        _getOffFromHorse = _inventoriManager.GetOffFromHorse;
       // shootBallista = _inventoriManager.shootBalista;

    //    _sitOnTheHorse.onClick.AddListener(_horseControl.SitOnHorse);
    //    _getOffFromHorse.onClick.AddListener(_horseControl.GetOffHorse);

        //switch(_classList.value)
        //{
        //    case 0:
        //        _kick.onClick.AddListener(_playerControl.Kick());
        //        _kickDirect.onClick.AddListener(_playerControl.KickDirect);
        //        break;
        //    case 1:
        //        magAttack._attackFire = _inventoriManager.attackFire;
        //        magAttack._attackFire.onClick.AddListener(magAttack.FireShoot);
        //        magAttack._attackRock = _inventoriManager.attackRock;
        //        magAttack._attackRock.onClick.AddListener(magAttack.RockShoot);
        //        break;
        //}

     //   _playerControl.SetIndex(iconGuildList1.value);
    }

    public override void OnNetworkSpawn()
    {
        if (!IsLocalPlayer) return;
        _nickValue = FindObjectOfType<Connect>()._nick.text;
        SetNickServerRpc(_nickValue);
        Debug.Log("Connected");
    }


    public void ChangeHPBarValueBody(int damage)
    {
        if (!IsLocalPlayer) return;
        _HPbarBody.value -= damage;

    }

    public void ChangeHPBarValueHead(int damage)
    {
        if (!IsLocalPlayer) return;
        _HPbarHead.value -= damage;

    }

    public void ChangeHPBarValueLegs(int damage)
    {
        if (!IsLocalPlayer) return;
        _HPbarLegs.value -= damage;
        _HPbarLegs2.value -= damage;

    }
    public void ChangeHPBarValueArms(int damage)
    {
        if (!IsLocalPlayer) return;
        _HPbarArms.value -= damage;

    }

    public void UpdateTextMoney(int money)
    {
        if (!IsLocalPlayer) return;
        _money.text = money.ToString();
    }

    private void SetNick(FixedString64Bytes previousValue, FixedString64Bytes newValue)
    {
    //    if (!IsLocalPlayer) return;
        _nick.text = newValue.ToString();
    }



    [ServerRpc(RequireOwnership = false)]

    private void SetNickServerRpc(string nick, ServerRpcParams serverRpcParams = default)
    {
        _nickString.Value = nick;
    }


    private void OnDisable()
    {

        _nickString.OnValueChanged -= SetNick;
    }

    public InventoriManager GetInventoryManager()
    {
        return _inventoriManager;
    }

}
