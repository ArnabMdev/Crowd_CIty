using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFunctionality : MonoBehaviour
{
    UIManager ui;

    private void Start()
    {
        ui = GetComponent<UIManager>();
    }

    public void settings()
    {

        closePanels();
        ui.Settings.SetActive(true);
        Debug.Log("settings was clicked");
    }
    public void selectTournament()
    {
        closePanels();
        ui.selectTournamnetScreen.SetActive(true);
        Debug.Log("tournament");
    }
    public void toParticipateInTournament()
    {
        closePanels();
        ui.toParticipateInTournament.SetActive(true);
        Debug.Log("participateintournament");
    }
    public void shippingDetails()
    {
        closePanels();
        ui.shippingDetails.SetActive(true);
        Debug.Log("shippingDetails");
    }

    public void yourTournament()
    {
        closePanels();
        ui.yourTournament.SetActive(true);
        Debug.Log("yourtournament");
    }

    public void leaderBoard()
    {
        closePanels();
        ui.LeaderBoard.SetActive(true);
        Debug.Log("leaderBoard");
    }

    public void chooseYourPrice()
    {
        closePanels();
        ui.choosePrice.SetActive(true);
        Debug.Log("choosePrice");
    }

    public void KYC()
    {
        closePanels();
        ui.KYC.SetActive(true);
        Debug.Log("KYC");
    }

    public void packageStatus()
    {
        closePanels();
        ui.packageStatus.SetActive(true);
        Debug.Log("packageStatus");
    }

    public void orderHistory()
    {
        closePanels();
        ui.orderHistory.SetActive(true);
        Debug.Log("orderHistory");
    }

    public void winners()
    {
        closePanels();
        ui.Winners.SetActive(true);
        Debug.Log("winners");
    }
    public void Shop()
    {
        closePanels();
        ui.Shop.SetActive(true);
        ui.menuPanel.SetActive(false);
        Debug.Log("Shop");
    }

    public void item()
    {
        closePanels();
        ui.item.SetActive(true);
        Debug.Log("item");
    }

    public void mywinItem()
    {
        closePanels();
        ui.myWinItem.SetActive(true);
        Debug.Log("myWinItem");
    }

    public void selectMap()
    {
        closePanels();
        ui.select_mapPanel.SetActive(true);
        Debug.Log("selectMap");
    }
    public void selectCharacter()
    {
        closePanels();
        ui.selectCharacterScreen.SetActive(true);
        Debug.Log("selectCharacter");
    }
    public void coinBalance()
    {
        closePanels();
        ui.coinBalance.SetActive(true);
        Debug.Log("coinBalance");
    }

    public void Menu()
    {
        closePanels();
        ui.menuPanel.SetActive(true);
        Debug.Log("menu");
    }

    void closePanels()
    {
        ui.menuPanel.SetActive(false);
        ui.yourTournament.SetActive(false);
        ui.toParticipateInTournament.SetActive(false);
        ui.selectCharacterScreen.SetActive(false);
        ui.LeaderBoard.SetActive(false);
        ui.Winners.SetActive(false);
        ui.shippingDetails.SetActive(false);
        ui.KYC.SetActive(false);
        ui.Shop.SetActive(false);
        ui.Settings.SetActive(false);
        ui.coinBalance.SetActive(false);
        ui.choosePrice.SetActive(false);
        ui.item.SetActive(false);
        ui.myWinItem.SetActive(false);
        ui.orderHistory.SetActive(false);
        ui.packageStatus.SetActive(false);
    }
}
