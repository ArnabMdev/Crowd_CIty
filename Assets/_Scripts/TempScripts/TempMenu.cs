using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TempMenu : MonoBehaviour
{
    GameManager gm;
    public GameObject LoadingPanel, RegisterPanel, InfoPanel, OTPPanel;
    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        closePanels();
        RegisterPanel.SetActive(true);
    }

    void closePanels()
    {
        LoadingPanel.SetActive(false);
        RegisterPanel.SetActive(false);
        InfoPanel.SetActive(false);
        OTPPanel.SetActive(false);
    }
    public void skipButton()
    {
        closePanels();
        InfoPanel.SetActive(true);
        
    }

    public void infoScreenSubmit()
    {
        closePanels();
        OTPPanel.SetActive(true);
    }

    public void loadGame()
    {
        Debug.Log("Loaded Next Scene");
        SceneManager.LoadScene("_MainScene");
    }
}
