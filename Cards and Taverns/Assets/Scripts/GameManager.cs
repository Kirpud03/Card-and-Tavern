using UnityEngine;
using Cinemachine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public CinemachineVirtualCamera cam1;
    public CinemachineVirtualCamera cam2;
    public List<GameObject> cardZone;
    private bool _playerLose;
    private bool _playerWin;
    private bool _pause;
    [SerializeField]
    private GameObject _pauseMenu, _loseMenu, _winMenu;
    private void Start()
    {
        Time.timeScale = 1;
    }
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
        if (StaticHolder.plHealth <= 0)
        {
            _playerLose = true;
        }
        if(StaticHolder.enHealth <= 0)
        {
            _playerWin = true;
        }
        if (_playerWin)
        {
            Time.timeScale = 0;
            _winMenu.SetActive(true);
        }
        if (_playerLose)
        {
            Time.timeScale = 0;
            _loseMenu.SetActive(true);
        }
        if (_pause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
    public void PauseMenu()
    {
        _pause = !_pause;
        _pauseMenu.SetActive(_pause);
    }
    public void ResumeButton()
    {
        _pause = false;
        _pauseMenu.SetActive(_pause);
    }
    public void QuitButton()
    {
        Application.Quit();
    }
    public void MenuButton()
    {
        Time.timeScale = 1;
        _pause = false;
        SceneManager.LoadScene(0);
    }
    public void RestartButton()
    {
        Time.timeScale = 1;
        _pause = false;
        SceneManager.LoadScene(1);
    }
}
