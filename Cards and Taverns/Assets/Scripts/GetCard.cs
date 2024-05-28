using System.Collections.Generic;
using UnityEngine;

public class GetCard : MonoBehaviour
{
    [SerializeField]
    private GameObject _hand;
    private int _cardNum;
    [SerializeField]
    private List<GameObject> _cards;
    private bool _firstGive;
    private bool _moveTake;
    private void FixedUpdate()
    {
        if (StaticHolder.cardsOnHand == 7)
        {
            StaticHolder.canTake = false;
        }
        if (StaticHolder.cardsOnHand < 7 && StaticHolder.canTake == false)
        {
            StaticHolder.canTake = true;
        }
        if (_firstGive == false)
        {
            FirstGive();
        }
        if (StaticHolder.Move % 2 == 0 && _moveTake == true && StaticHolder.Move != 2)
        {
            _cardNum = Random.Range(0, 5);
            if (StaticHolder.cardsOnHand < 7 && StaticHolder.canTake == true && StaticHolder.putCard == false && StaticHolder.playerTurn)
            {
                Instantiate(_cards[_cardNum], _hand.transform.position, _hand.transform.rotation);
                StaticHolder.cardsOnHand++;
                StaticHolder.canTake = false;
            }
            _moveTake = false;
        }
        if (StaticHolder.Move % 2 != 0)
        {
            _moveTake = true;
        }
    }
    public void FirstGive()
    {
        for (int i = 0; i < 5; i++)
        {
            _cardNum = Random.Range(0, 5);
            Instantiate(_cards[_cardNum], _hand.transform.position, _hand.transform.rotation);
            StaticHolder.cardsOnHand++;
        }
        _firstGive = true;
    }
    public void OnMouseDown()
    {
        _cardNum = Random.Range(0, 5);
        if (StaticHolder.cardsOnHand < 7 && StaticHolder.canTake == true&&StaticHolder.putCard==false&&StaticHolder.playerTurn)
        {
            Instantiate(_cards[_cardNum], _hand.transform.position, _hand.transform.rotation);
            StaticHolder.cardsOnHand++;
            StaticHolder.canTake = false;
            StaticHolder.Move++;
        }
    }
}
