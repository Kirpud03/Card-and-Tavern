using UnityEngine;
using Cinemachine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public CinemachineVirtualCamera cam1;
    public CinemachineVirtualCamera cam2;
    public List<GameObject> cardZone;
    private void Update()
    {
        StaticHolder.cardTable = GameObject.FindGameObjectsWithTag("CardPuted");
        cam2.enabled = StaticHolder.switchCam;
        if (StaticHolder.putCard)
        {
            for(int i =0;i<cardZone.Count;i++)
            {
                cardZone[i].SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < cardZone.Count; i++)
            {
                cardZone[i].tag = "CardZone";
            }
            for (int i = 0; i < cardZone.Count; i++)
            {
                cardZone[i].SetActive(false);
            }
        }
        if (StaticHolder.Move % 2 == 0)
        {
            StaticHolder.playerTurn = true;
        }
        else if (StaticHolder.Move % 2 != 0)
        {
            StaticHolder.playerTurn = false;
        }
    }
}
