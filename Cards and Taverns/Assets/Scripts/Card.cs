using TMPro;
using UnityEngine;

public class Card : MonoBehaviour
{ 
    private Rigidbody rb;
    private bool _cardPlayed = false;
    private GameObject _cardZone;
    private bool _thisCardPut = false;
    private Vector3 _direct;
    private bool _canPuted = false;
    private int _num;
    [SerializeField]
    private TMP_Text _healthTex;
    public int _health;
    [SerializeField]
    public int _damage;
    [SerializeField]
    private TMP_Text _bloodTex;
    [SerializeField]
    private int _blood;
    [SerializeField]
    private GameObject _bloodNeed;
    private bool _bloodLess;
    private TMP_Text _enemyHealth;
    public Card card;
    private bool _attack;
    private void Awake()
    {
        card = this;
    }
    private void Start()
    {
        _enemyHealth = GameObject.FindGameObjectWithTag("EnemyHealth").GetComponent<TMP_Text>();
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        for (int i = 0; i < StaticHolder.cardNum.Count; i++)
        {
            if (StaticHolder.cardNum[i] == 0)
            {
                _num = i+1;
                StaticHolder.cardNum[i] = _num;
                break;
            }
        }
    }
    private void Update()
    {
        _enemyHealth.text = StaticHolder.enHealth.ToString();
        _bloodTex.text = _blood.ToString();
        _healthTex.text = _health.ToString();
        if (_cardPlayed == false && _thisCardPut == false && StaticHolder.putCard == false)
        {
            switch (_num)
            {
                case 1:
                    transform.position = new Vector3(0.001f, 1.605f, -0.988f);
                    break;
                case 2:
                    transform.position = new Vector3(0.232f, 1.605f, -0.988f);
                    break;
                case 3:
                    transform.position = new Vector3(-0.23f, 1.605f, -0.988f);
                    break;
                case 4:
                    transform.position = new Vector3(0.463f, 1.605f, -0.988f);
                    break;
                case 5:
                    transform.position = new Vector3(-0.461f, 1.605f, -0.988f);
                    break;
                case 6:
                    transform.position = new Vector3(0.694f, 1.605f, -0.988f);
                    break;
                case 7:
                    transform.position = new Vector3(-0.692f, 1.605f, -0.988f);
                    break;
            }
        }
        if (_health < 0)
        {
            _health = 0;
        }
        if (transform.position.x <= -0.923f && _cardPlayed == false)
        {
            transform.position = new Vector3(0.694f, transform.position.y, transform.position.z);
        }
        if (StaticHolder.putCard&&_thisCardPut&&_cardPlayed==false&&_blood<=0&&StaticHolder.playerTurn)
        {
            _cardZone = GameObject.FindGameObjectWithTag("CardZoneAct");
            if(_cardZone != null && _thisCardPut && _canPuted && StaticHolder.playerTurn)
            {
                _direct = _cardZone.transform.position - transform.position;
                rb.isKinematic=false;
                rb.velocity = _direct*1.5f;
                transform.rotation = Quaternion.Euler(-90, 0, -180);
            }
        }
        if (StaticHolder.playerTurn && StaticHolder.Move % 2 == 0)
        {
            _attack = false;
        }
        if (StaticHolder.putCard == false)
        {
            StaticHolder.cardOnTable = StaticHolder.cardTable.Length;
        }
        if (_blood <= 0)
        {
            _bloodNeed.SetActive(false);
            _canPuted = true;
        }
        if (StaticHolder.cardOnTable != StaticHolder.cardTable.Length&& _blood>0&&_thisCardPut&&_bloodLess==false&& StaticHolder.playerTurn)
        {
            Invoke("LessBlood", 0);
        }
        if (_bloodLess)
        {
            _bloodLess = false;
        }
        if (StaticHolder.playerTurn == false && _cardPlayed && StaticHolder.Move % 2 != 0 && _attack == false)
        {
            RaycastHit hit;
            Ray ray = new Ray(new Vector3(transform.position.x, 1.217654f, transform.position.z), transform.up);
            if (Physics.Raycast(ray, out hit, 0.3f))
            {
                if (hit.collider.CompareTag("EnemyCardPlayed"))
                {
                    hit.collider.GetComponent<EnemyCard>().inc.health = hit.collider.GetComponent<EnemyCard>().inc.health - _damage;
                    _attack = true;
                }
            }
            if (_attack == false)
            {
                StaticHolder.enHealth = StaticHolder.enHealth - _damage;
                _enemyHealth.text = StaticHolder.enHealth.ToString();
                _attack = true;
            }
        }
        if (_health <= 0)
        {
            GameObject[] zone = GameObject.FindGameObjectsWithTag("PlayerBusyZone");
            for (int i = 0; i < zone.Length; i++)
            {
                if (zone[i].transform.position.x == transform.position.x && zone[i].transform.position.z == transform.position.z)
                {
                    zone[i].tag = "Zone";
                    Destroy(gameObject);
                    break;
                }
            }
        }
    }
    private void LessBlood()
    {
        if (_bloodLess == false)
        {
            _blood--;
            StaticHolder.cardOnTable = StaticHolder.cardTable.Length;
            _bloodLess = true;
        }
    }
    private void OnMouseDown()
    {
        if (_cardPlayed == false&& StaticHolder.playerTurn)
        {
            StaticHolder.switchCam = !StaticHolder.switchCam;
            StaticHolder.putCard = !StaticHolder.putCard;
            _thisCardPut =! _thisCardPut;
        }
        if (StaticHolder.putCard && _cardPlayed&&_thisCardPut==false&& StaticHolder.playerTurn)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("CardZoneAct"))
        {
            transform.position = new Vector3(col.transform.position.x, transform.position.y, col.transform.position.z);
            rb.isKinematic = true;
            _cardZone.SetActive(false);
            StaticHolder.putCard = !StaticHolder.putCard;
            StaticHolder.switchCam = !StaticHolder.switchCam;
            StaticHolder.canTake = !StaticHolder.canTake;
            _thisCardPut = false;
            StaticHolder.cardsOnHand--;
            gameObject.tag = "CardPuted";
            _cardPlayed = true;
            _cardZone.tag = "CardZone";
            StaticHolder.cardNum[_num - 1] = 0;
            transform.rotation = Quaternion.Euler(-90, 0, -180);
        }
    }
    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("CardZone"))
        {
            col.gameObject.SetActive(false);
        }
    }
}
