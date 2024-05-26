using UnityEngine;

public class GetreserveCard : MonoBehaviour
{
    [SerializeField]
    private GameObject _reserveCard;
    [SerializeField]
    private GameObject _hand;
    private void OnMouseDown()
    {
        if (StaticHolder.cardsOnHand < 7 && StaticHolder.canTake == true && StaticHolder.putCard == false&&StaticHolder.playerTurn)
        {
            Instantiate(_reserveCard, _hand.transform.position, _hand.transform.rotation);
            StaticHolder.cardsOnHand++;
            StaticHolder.canTake = false;
            Destroy(gameObject);
        }
    }
}
