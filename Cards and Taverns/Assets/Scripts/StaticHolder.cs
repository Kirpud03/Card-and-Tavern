using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticHolder
{
    public static int cardsOnHand = 0;
    public static bool canTake = true;
    public static bool switchCam = false;
    public static bool putCard = false;
    public static int cardOnTable = 0;
    public static List<int> cardNum = new List<int> { 0, 0, 0, 0, 0, 0, 0 };
    public static GameObject[] cardTable;
    public static bool playerTurn = false;
    public static int Move = 1;
    public static int enHealth = 20;
    public static int plHealth = 20;
}
