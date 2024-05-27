using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _cards;
    public static List<int> cardNum = new List<int> { 0, 0, 0, 0, 0, 0, 0 };
    private GameObject[] _cardOnHand;
    private GameObject[] _cardOnTable;
    private bool _firstGived = false;
    [SerializeField]
    private List<GameObject> _zone;
    public static bool cardPuted = false;
    private bool _canTake = true;
    private bool _firstMove;
    private bool _moveTake;
    private int moveDone;
    private void Start()
    {
        if (_firstGived == false)
        {
            FirstGiveEn();
        }
    }
    private void Update()
    {
        _cardOnTable = GameObject.FindGameObjectsWithTag("EnemyCardPlayed");
        _cardOnHand = GameObject.FindGameObjectsWithTag("EnemyCard");
        if (_moveTake == true && StaticHolder.Move % 2 != 0 && StaticHolder.Move != 1)
        {
            if (_cardOnHand.Length < 7 && _canTake == true)
            {
                Instantiate(_cards[Random.Range(0, _cards.Count)], transform.position, Quaternion.Euler(15, 0, 0));
                _canTake = false;
            }
            _moveTake = false;
        }
        if (StaticHolder.Move % 2 == 0)
        {
            _moveTake = true;
        }
        if (_canTake == false && StaticHolder.playerTurn == false)
        {
            _canTake = true;
        }
        if (StaticHolder.playerTurn == false&&StaticHolder.Move%2!=0&&_firstMove==true)
        {
            int zone = Random.Range(0, _cards.Count);
            if (_zone[zone].tag != "Zone")
            {
                zone = Random.Range(0, _cards.Count);
            }
            for (int i = 0; i < _cardOnHand.Length; i++)
            {
                if (_cardOnHand[i].GetComponent<EnemyCard>().inc.blood == 0 && cardPuted == false && _cardOnHand.Length > 2)
                {
                    _cardOnHand[i].GetComponent<EnemyCard>().inc.thisCardSel = true;
                    _cardOnHand[i].GetComponent<EnemyCard>().inc.target = _zone[zone];
                    cardPuted = true;
                }
                if (_cardOnHand[i].GetComponent<EnemyCard>().inc.blood == 1 && cardPuted == false&&_cardOnHand.Length>1)
                {
                    int imin = 0;
                    for (int c = 1; c < _cardOnTable.Length; c++)
                    {
                        if (_cardOnTable[c].GetComponent<EnemyCard>().inc.damage < _cardOnTable[imin].GetComponent<EnemyCard>().inc.damage)
                        {
                            imin = c;
                        }
                    }
                    _cardOnHand[i].GetComponent<EnemyCard>().inc.thisCardSel = true;
                    _cardOnTable[imin].GetComponent<EnemyCard>().inc.destroyed = true;
                    _cardOnHand[i].GetComponent<EnemyCard>().inc.blood--;
                    _cardOnHand[i].GetComponent<EnemyCard>().inc.target = _zone[zone];
                    cardPuted = true;
                }
                if (_cardOnHand[i].GetComponent<EnemyCard>().inc.blood == 3 && cardPuted == false && _cardOnHand.Length > 1)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        int imin = 0;
                        for (int c = 1; c < _cardOnTable.Length; c++)
                        {
                            if (_cardOnTable[c].GetComponent<EnemyCard>().inc.damage < _cardOnTable[imin].GetComponent<EnemyCard>().inc.damage)
                            {
                                imin = c;
                            }
                        }
                        _cardOnTable[imin].GetComponent<EnemyCard>().inc.destroyed = true;
                        _cardOnHand[i].GetComponent<EnemyCard>().inc.blood--;
                        _cardOnHand[i].GetComponent<EnemyCard>().inc.thisCardSel = true;
                        _cardOnHand[i].GetComponent<EnemyCard>().inc.target = _zone[zone];
                        cardPuted = true;
                    }
                }
            }
            if (StaticHolder.Move == 1 && cardPuted == false && _firstMove == false)
            {
                bool allCardBlood = true;
                for (int i = 0; i < _cardOnHand.Length; i++)
                {
                    if (_cardOnHand[i].GetComponent<EnemyCard>().inc.blood == 0)
                    {
                        Invoke("FirstMove", 3f);
                        StaticHolder.switchCam = true;
                        allCardBlood = false;
                        _firstMove = true;
                        break;
                    }
                }
                if(allCardBlood)
                {
                    GetCard();
                }
            }
        }
        if (_cardOnHand.Length == 2)
        {
            GetCard();
        }
    }
    private void FirstMove()
    {
        if (cardPuted == false)
        {
            int zone = Random.Range(0, 5);
            for (int i = 0; i < _cardOnHand.Length; i++)
            {
                if (_cardOnHand[i].GetComponent<EnemyCard>().inc.blood == 0 && _zone[zone].CompareTag("Zone"))
                {
                    _cardOnHand[i].GetComponent<EnemyCard>().inc.thisCardSel = true;
                    _cardOnHand[i].GetComponent<EnemyCard>().inc.target = _zone[zone];
                    cardPuted = true;
                    break;
                }
            }
        }
    }
    private void GetCard()
    {
        if (_cardOnHand.Length < 7 && _canTake==true)
        {
            Instantiate(_cards[Random.Range(0, _cards.Count)], transform.position, Quaternion.Euler(15, 0, 0));
            StaticHolder.Move++;
            StaticHolder.switchCam = false;
            StaticHolder.playerTurn = true;
            _canTake = false;
            _firstMove = true;
        }
    }
    private void FirstGiveEn()
    {
        for (int i = 0; i < 5; i++)
        {
            Instantiate(_cards[Random.Range(0, _cards.Count)], transform.position, Quaternion.Euler(15, 0, 0));
        }
        _firstGived = true;
    }
}
