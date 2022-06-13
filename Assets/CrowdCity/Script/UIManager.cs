using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private UIManager uiManager;
    private GameManager gameManager;

    public GameObject menuPanel, ingamePanel, timeOutPanel, killedByPanel, MultiplayerMenu,
        selectTournamnetScreen, select_mapPanel,
        yourTournament, toParticipateInTournament,
        selectCharacterScreen, LeaderBoard, Winners, shippingDetails, KYC, Shop,
        Settings, coinBalance, choosePrice, item, myWinItem, orderHistory, packageStatus, login,Register,Info,OTP;


    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        gameManager = FindObjectOfType<GameManager>();
        
    }

    public void TimeOut()
    {
        menuPanel.SetActive(false);
        ingamePanel.SetActive(false);
        timeOutPanel.SetActive(true);
        killedByPanel.SetActive(false);
        MultiplayerMenu.SetActive(false);
    }

    public void KilledBy()
    {
        menuPanel.SetActive(false);
        ingamePanel.SetActive(false);
        timeOutPanel.SetActive(false);
        killedByPanel.SetActive(true);
        MultiplayerMenu.SetActive(false);
    }

    public void startGame()
    {
        uiManager.ingamePanel.SetActive(true);
        gameManager.StartGame();

    }
}
