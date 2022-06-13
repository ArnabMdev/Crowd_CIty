using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Menu : MonoBehaviour {


	public static Menu instance;
	private UIManager uiManager;
	private GameManager gameManager;
	private GroupData groupData;
	public AudioSource audioSource;

	public TextMeshProUGUI bestscoreTxt, rankTxt, nameTxt;
	public Image city, play, sound;
	public Sprite  soundOn,soundOff;
	//public Color[] colors;
	public Material playerMat;

	void Awake(){
		instance = this;
	}

	void Start(){
		uiManager = FindObjectOfType<UIManager> ();
        gameManager = FindObjectOfType<GameManager> ();
		FirstFill ();
	}

	public void FirstFill()
	{
		bestscoreTxt.text = gameManager.data[0].KillCount.ToString();
		nameTxt.text = "You";
	}

	public void PlayBtnPress()
	{
		/*uiManager.menuPanel.SetActive (false);
		uiManager.timeOutPanel.SetActive (false);
		uiManager.killedByPanel.SetActive (false);
		uiManager.MultiplayerMenu.SetActive(false);
		uiManager.menuPanel.SetActive(false);
		uiManager.yourTournament.SetActive(false);
		uiManager.toParticipateInTournament.SetActive(false);
		uiManager.selectCharacterScreen.SetActive(false);
		uiManager.LeaderBoard.SetActive(false);
		uiManager.Winners.SetActive(false);
		uiManager.shippingDetails.SetActive(false);
		uiManager.KYC.SetActive(false);
		uiManager.Shop.SetActive(false);
		uiManager.Settings.SetActive(false);
		uiManager.coinBalance.SetActive(false);
		uiManager.choosePrice.SetActive(false);
		uiManager.item.SetActive(false);
		uiManager.myWinItem.SetActive(false);
		uiManager.orderHistory.SetActive(false);
		uiManager.packageStatus.SetActive(false);
		uiManager.select_mapPanel.SetActive(false);*/
		uiManager.ingamePanel.SetActive(true);
		gameManager.StartGame ();
		

	}

	public void SoundBtnPress()
	{
		if (audioSource.volume == 0) {
			audioSource.volume = 1;
			sound.sprite = soundOn;
		} else {
			audioSource.volume = 0;
			sound.sprite = soundOff;
		}
	}

	public void Left()
	{
		//FindObjectOfType<PlayerMaterialChanger> ().leftButton ();

		city.color = playerMat.color;
		play.color = playerMat.color;	
	}

	public void Right()
	{
		//FindObjectOfType<PlayerMaterialChanger> ().rightButton();

		city.color = playerMat.color;
		play.color = playerMat.color;
	}

	public void PrivacyPolicy(){
		Application.OpenURL ("https://sites.google.com/view/privacysimulation/accueil");

    }
}
