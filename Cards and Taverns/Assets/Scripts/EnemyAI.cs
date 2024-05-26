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
        if (_cardOnHand.Length == 7)
        {
            int cardToDestroy = Random.Range(0, _cardOnHand.Length);
            Destroy(_cardOnHand[cardToDestroy]);
        }
        if (_moveTake == true && StaticHolder.Move % 2 != 0 && StaticHolder.Move != 1)
        {
            if (_cardOnHand.Length < 7 && _canTake == true)
            {
                Instantiate(_cards[Random.Range(0, _cards.Count)], transform.position, Quaternion.Euler(15, 0, 0));
                _canTake = false;
                _firstMove = true;
            }
            _moveTake = false;
        }
        if (StaticHolder.Move % 2 == 0)
        {
            _moveTake = true;
        }
        _cardOnTable = GameObject.FindGameObjectsWithTag("EnemyCardPlayed");
        _cardOnHand = GameObject.FindGameObjectsWithTag("EnemyCard");
        if (_canTake == false && StaticHolder.playerTurn == false)
        {
            _canTake = true;
        }
        if (StaticHolder.playerTurn == false&&cardPuted==false&&StaticHolder.Move%2!=0)
        {
            int move = Random.Range(0, 3);
                switch (move)
                {
                    case 0:
                        bool allBlood = false;
                        int zone = Random.Range(0, _zone.Count);
                        StaticHolder.switchCam = true;
                        for (int i = 0; i < _cardOnHand.Length; i++)
                        {
                            if (_cardOnHand[i].GetComponent<EnemyCard>().inc.blood == 0 && _zone[zone].tag != "EnemyBusyZone" && moveDone != 3)
                            {
                                _cardOnHand[i].GetComponent<EnemyCard>().inc.thisCardSel = true;
                                _cardOnHand[i].GetComponent<EnemyCard>().inc.target = _zone[zone];
                                cardPuted = true;
                                allBlood = false;
                                moveDone++;
                                break;
                            }
                            else
                            {
                                zone = Random.Range(0, _zone.Count);
                                allBlood = true;
                            }
                        }
                        if (allBlood)
                        {
                            GetCard();
                        }
                        break;
                    case 1:
                        StaticHolder.switchCam = true;
                        int imin = 0;
                        zone = Random.Range(0, _zone.Count);
                        for (int i = 0; i < _cardOnHand.Length; i++)
                        {
                            if (_cardOnHand[i].GetComponent<EnemyCard>().inc.blood == 1 && _zone[zone].tag != "EnemyBusyZone" && _cardOnTable.Length > 1 && moveDone != 3)
                            {
                                _cardOnHand[i].GetComponent<EnemyCard>().inc.thisCardSel = true;
                                if (_cardOnTable.Length > 0)
                                {
                                    for (int j = 1; j < _cardOnTable.Length; j++)
                                    {
                                        if (_cardOnTable[j].GetComponent<EnemyCard>().inc.damage < _cardOnTable[imin].GetComponent<EnemyCard>().inc.damage)
                                        {
                                            imin = j;
                                        }
                                    }
                                    Destroy(_cardOnTable[imin]);
                                    _cardOnHand[i].GetComponent<EnemyCard>().inc.blood--;
                                    _cardOnHand[i].GetComponent<EnemyCard>().inc.target = _zone[zone];
                                    cardPuted = true;
                                    allBlood = false;
                                    moveDone++;
                                    break;
                                }
                            }
                            else
                            {
                                zone = Random.Range(0, _zone.Count);
                            }
                        }
                        if (_cardOnHand.Length < 3)
                        {
                            GetCard();
                        }
                        break;
                    case 2:
                        StaticHolder.switchCam = true;
                        imin = 0;
                        zone = Random.Range(0, _zone.Count);
                        for (int i = 0; i < _cardOnHand.Length; i++)
                        {
                            if (_cardOnHand[i].GetComponent<EnemyCard>().inc.blood == 3 && _zone[zone].tag != "EnemyBusyZone" && _cardOnTable.Length > 2 && moveDone != 3)
                            {
                                _cardOnHand[i].GetComponent<EnemyCard>().inc.thisCardSel = true;
                                for (int c = 0; c < 3; c++)
                                {
                                    for (int j = 1; j < _cardOnTable.Length; j++)
                                    {
                                        if (_cardOnTable[j].GetComponent<EnemyCard>().inc.damage < _cardOnTable[imin].GetComponent<EnemyCard>().inc.damage)
                                        {
                                            imin = j;
                                        }
                                    }
                                    Destroy(_cardOnTable[imin]);
                                }
                                _cardOnHand[i].GetComponent<EnemyCard>().inc.blood = _cardOnHand[i].GetComponent<EnemyCard>().inc.blood - 3;
                                _cardOnHand[i].GetComponent<EnemyCard>().inc.target = _zone[zone];
                                cardPuted = true;
                                allBlood = false;
                                moveDone++;
                                break;
                            }
                            else
                            {
                                zone = Random.Range(0, _zone.Count);
                            }
                        }
                        break;
                }
            if (moveDone == 3)
            {
                StaticHolder.Move++;
                moveDone = 0;
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
        if (_cardOnHand.Length < 3)
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
