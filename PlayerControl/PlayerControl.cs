using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using Unity.Netcode.Components;

public class PlayerControl : NetworkBehaviour
{
    private Vector3 lastMouse = new Vector3(255, 255, 255); //kind of in the middle of the screen, rather than at the top (play)
    float camSens = 0.05f; //How sensitive it with mouse

    [SerializeField] private Animator _swordAnimationAttack1;

    private int _hpBody = 50;
    private int _hpHead = 50;
    private int _hpLegs = 50;
    private int _hpArms = 50;

    [SerializeField]private float _speed = 0.25f;

    private UIcontrol _uIcontrol;

    [SerializeField] private GameObject weapon;

    private Rigidbody _rb;

    public bool _canJump;

    private int _colorIndexBackSide;


    private Joystick _joystick;
    private Joystick _joystickRotate;

    private Vector3 _spavnPos;

  //  [Header("Movement")]
    [SerializeField] private GameObject fence1;

    [SerializeField] private Transform spavnPosition;

    [SerializeField] private GameObject FireBall;

    [SerializeField] private Transform ShootPosition;

    [SerializeField] private BuildCastle buildCastle;

   // [SerializeField] private GameObject cameraPlayer;



   // [SerializeField]  private Image _quest;

    // public bool IsAttack = false;

    private void Awake()
    {

      //  
    }

    void Start()
    {
        _uIcontrol = GetComponent<UIcontrol>();
        _rb = GetComponent<Rigidbody>();
        _spavnPos = transform.position;

        _joystickRotate = GameObject.FindGameObjectWithTag("RotateJoystick")
            .GetComponent<Joystick>();

     //   _quest = GameObject.FindGameObjectWithTag("Quest").GetComponent<Image>();

        //weapon.gameObject.tag = "StartSword";
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsLocalPlayer) return;

        transform.Rotate(0, _joystickRotate.Horizontal * 6f, 0);
  //      cameraPlayer.transform.Rotate(0, _joystickRotate.Vertical * 3f, 0);

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(-Vector3.forward * _speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * _speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * _speed * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (Input.GetMouseButtonDown(0))
        {
            Kick();
        }
        if (Input.GetMouseButtonDown(1))
        {
            KickDirect();
        }


    }

    //private void ChangePanel()
    //{
    //    _quest.gameObject.SetActive(true);
    //    _quest.GetComponentInChildren<Text>().text = "Shoper";
    //}
    //private void OnCollisionEnter(Collision collision)
    //{

    //    if (collision.gameObject.tag == "Skelet")
    //    {
    //        if (!IsLocalPlayer)
    //            return;

    //        GetDamage(10, 3);
    //    }

    //    if (collision.gameObject.tag == "BigSpear")
    //    {
    //        if (!IsLocalPlayer)
    //            return;

    //        GetDamage(50, 3);
    //    }


    //    if (collision.gameObject.GetComponent<Terrain>() || collision.gameObject.tag == "StoneThing")
    //    {
    //        if (!IsLocalPlayer)
    //            return;

    //        _canJump = true;
    //    }

    //    if (collision.gameObject.tag == "StartSword")
    //    {
    //        //          if (!IsLocalPlayer) return;
    //        if (!IsLocalPlayer)
    //            return;

    //        GetDamage(5,3);

    //    }

    //    if (collision.gameObject.tag == "StartShoper")
    //    {
    //        //ChangePanel();
    //    }
    //}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Terrain>() || collision.gameObject.tag == "StoneThing")
        {
            if (!IsLocalPlayer)
                return;

            _canJump = true;
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.GetComponent<Terrain>())
        {
            _canJump = false;
        }
    }
    private IEnumerator StopAnimatiomAttackSword()
    {
        yield return new WaitForSeconds(1.1f);

        SetEnableSwordCollisionServerRpc(false);

    }


    public void GetDamage(int damage, int bodyPart)
    {
        if (!IsLocalPlayer) 
            return;

        switch (bodyPart)
        {
            case 1:
                _hpHead -= damage;
                _uIcontrol.ChangeHPBarValueHead(damage);
                break;
            case 2:
                _hpArms -= damage;
                _uIcontrol.ChangeHPBarValueArms(damage);
                break;
            case 3:
                _hpBody -= damage;
                _uIcontrol.ChangeHPBarValueBody(damage);
                break;
            case 4:
                _hpLegs -= damage;
                _uIcontrol.ChangeHPBarValueLegs(damage);

                if(_hpLegs < 25)
                {
                    _speed -= 0.03f;
                }
                break;
        }


        if (_hpHead <= 0 || _hpBody < 0)
        {
            _hpHead = 50;
            _hpArms = 50;
            _hpBody = 50;
            _hpLegs = 50;
            _uIcontrol.ChangeHPBarValueBody(-50);
            //_uIcontrol.ChangeHPBarValueArms(-50);
            //_uIcontrol.ChangeHPBarValueBody(-50);
            _uIcontrol.ChangeHPBarValueLegs(-50);
            transform.position = _spavnPos;
            _speed = 9f;
        
        }

    }


    private void Kick()
    {
        if (!IsLocalPlayer)
            return;


        _swordAnimationAttack1.SetBool("isAttack", true);

        SetEnableSwordCollisionServerRpc(true);
        weapon.GetComponent<BoxCollider>().enabled = true;
   
        Debug.Log("Work");
        StartCoroutine(StopAnimatiomAttackSword());
    }

    private void KickDirect()
    {
        if (!IsLocalPlayer)
            return;


        SetEnableSwordCollisionServerRpc(true);
        weapon.GetComponent<BoxCollider>().enabled = true;
        _swordAnimationAttack1.SetBool("isAttackDirect", true);
        Debug.Log("Work");
        StartCoroutine(StopAnimatiomAttackSword());
    }


    private void Jump()
    {
        if (!IsLocalPlayer) return;

        if (_canJump)
        {
            _rb.AddForce(Vector3.up * 100, ForceMode.Impulse);
        }
    }

    [ServerRpc]
    private void SpavnObjectServerRpc()
    {
        Instantiate(fence1, spavnPosition.position, Quaternion.identity).GetComponent<NetworkObject>().Spawn();
    }


    [ServerRpc] 
    private void SetEnableSwordCollisionServerRpc(bool value)
    {
        weapon.GetComponent<BoxCollider>().enabled = value;
    }

    public void AddSpeed(float add)
    {
        _speed += add;
    }

    public void SetIndex(int index)
    {
        _colorIndexBackSide = index;
        buildCastle.SetIndex(index);
    }
}
