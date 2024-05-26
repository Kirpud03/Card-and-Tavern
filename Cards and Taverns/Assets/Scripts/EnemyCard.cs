using UnityEngine;
using TMPro;

public class EnemyCard : MonoBehaviour
{
    private int _num;
    private Rigidbody rb;
    private bool _cardPlayed;
    public int health;
    public TMP_Text healthTex;
    public int damage;
    public TMP_Text damageTex;
    public int blood;
    public TMP_Text bloodTex;
    [SerializeField]
    private GameObject _bloodNeed;
    [HideInInspector]
    public  bool thisCardSel;
    [HideInInspector]
    public EnemyCard inc;
    [HideInInspector]
    public GameObject target;
    private TMP_Text _playerHealth;
    private bool _shifted;
    private GameObject _hisZone;
    private bool _attack;
    private void Awake()
    {
        inc = this;
    }
    private void Start()
    {
        _playerHealth = GameObject.FindGameObjectWithTag("PlayerHealth").GetComponent<TMP_Text>();
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;  
        for (int i = 0; i < EnemyAI.cardNum.Count; i++)
        {
            if (EnemyAI.cardNum[i] == 0)
            {
                _num = i + 1;
                EnemyAI.cardNum[i] = _num;
                break;
            }
        }
    }
    private void Update()
    {
        bloodTex.text = blood.ToString();
        healthTex.text = health.ToString();
        damageTex.text = damage.ToString();
        if (blood <= 0)
        {
            _bloodNeed.SetActive(false);
        }
        if (_cardPlayed == false&&thisCardSel==false&&_cardPlayed==false)
        {
            switch (_num)
            {
                case 1:
                    transform.position = new Vector3(0.001f, 1.605f, 0.46f);
                    break;
                case 2:
                    transform.position = new Vector3(-0.23f, 1.605f, 0.46f);
                    break;
                case 3:
                    transform.position = new Vector3(0.232f, 1.605f, 0.46f);
                    break;
                case 4:
                    transform.position = new Vector3(-0.461f, 1.605f, 0.46f);
                    break;
                case 5:
                    transform.position = new Vector3(0.463f, 1.605f, 0.46f);
                    break;
                case 6:
                    transform.position = new Vector3(-0.692f, 1.605f, 0.46f);
                    break;
                case 7:
                    transform.position = new Vector3(0.694f, 1.605f, 0.46f);
                    break;
            }
        }
        if (target != null&&thisCardSel&&blood==0)
        {
            Vector3 _direction = target.transform.position - transform.position;
            rb.isKinematic = false;
            rb.velocity = _direction * 1.5f;
            gameObject.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
        }
        if (health < 0)
        {
            health = 0;
        }
        if (health <= 0)
        {
            _hisZone.tag = "Zone";
            Destroy(gameObject);
        }
        if (_shifted == false && _cardPlayed && StaticHolder.Move % 2 == 0)
        {
            Vector3 _direction = new Vector3(transform.position.x, transform.position.y, -0.3285169f) - transform.position;
            rb.isKinematic = false;
            rb.velocity = _direction * 1.5f;
        }
        if (_shifted == false && _cardPlayed && StaticHolder.Move % 2 == 0 && transform.position.z == -0.3285169f)
        {
            rb.isKinematic = true;
            transform.position = new Vector3(transform.position.x, transform.position.y, -0.3285169f);
            _shifted = true;
        }
        if (_shifted && StaticHolder.Move % 2 == 0 && _attack == false && StaticHolder.playerTurn == true)
        {
            GameObject[] card = GameObject.FindGameObjectsWithTag("CardPuted");
            for (int i = 0; i < card.Length; i++)
            {
                if (card[i].transform.position.x == transform.position.x)
                {
                    card[i].GetComponent<Card>().card._health = card[i].GetComponent<Card>().card._health - damage;
                    _attack = true;
                    break;
                }
            }
            if (_attack == false)
            {
                StaticHolder.plHealth = StaticHolder.plHealth - damage;
                _playerHealth.text = StaticHolder.plHealth.ToString();
                _attack = true;
            }
        }
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Zone"&&_cardPlayed==false)
        {
            StaticHolder.switchCam = false;
            thisCardSel = false;
            gameObject.tag = "EnemyCardPlayed";
            gameObject.transform.rotation = Quaternion.Euler(-90, 0, 0);
            rb.isKinematic = true;
            _hisZone = col.gameObject;
            col.tag = "EnemyBusyZone";
            transform.position = target.transform.position;
            EnemyAI.cardNum[_num - 1] = 0;
            EnemyAI.cardPuted = false;
            _cardPlayed = true;
            if (StaticHolder.Move == 1)
            {
                StaticHolder.Move++;
                StaticHolder.playerTurn = true;
            }
        }
    }
}
