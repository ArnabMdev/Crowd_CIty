using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Defective.JSON;
using System;
using System.Text;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;

public class UserRegister : MonoBehaviour
{
    public TMP_InputField Fnameinput;

    public TMP_InputField mobileinput;
    public TMP_InputField refferalcodeinput;
    public TMP_InputField emailinput;
    public TMP_Text date;
    public TMP_Dropdown genderinput;
    public TMP_Dropdown countryinput;
    public GameObject genderObject;
    public GameObject countryObject;
    public TMP_Text warningTextInfoScreen;
    //  public TMP_Text warningTextRegisterScreen;
    int genderValue;
    //  int countryValue;
    private UIManager ui;
    public TMP_Text response;


    string url = "http://13.127.139.220:5200/api/v_1.0/user_register";     //user register API

    private void Start()
    {
        ui = FindObjectOfType<UIManager>();
        warningTextInfoScreen.gameObject.SetActive(false);
        // warningTextRegisterScreen.gameObject.SetActive(false);
        genderinput = genderObject.GetComponent<TMP_Dropdown>();
    }

    private void Update()
    {
        genderValue = genderinput.value;
        //Debug.Log(genderValue);
    }

    public void Register(string url)
    {
        if (emailinput.text.Contains("@"))
        {
            if (date.text.Equals(""))
            {
                warningTextInfoScreen.gameObject.SetActive(true);
                warningTextInfoScreen.text = "Please Choose Date";
            }
            else
            {
                StartCoroutine(UserRegister_Courutine(url));
            }

        }
        else
        {
            warningTextInfoScreen.gameObject.SetActive(true);
            warningTextInfoScreen.text = "Please Enter Valid Email";
        }

    }

    IEnumerator UserRegister_Courutine(string url)
    {
        //  string countryName = menuOptions[menuIndex].text;


        Register register = new Register();
        register.Fname = Fnameinput.text;
        register.dob = date.text;
        register.mobile = mobileinput.text;
        register.email = emailinput.text;
        register.gender = (genderValue + 1).ToString();

        int menuIndex = countryinput.GetComponent<TMP_Dropdown>().value;
        List<TMP_Dropdown.OptionData> menuOptions = countryinput.GetComponent<TMP_Dropdown>().options;
        register.country = menuOptions[menuIndex].text;                             //takes the text from the dropbox
        register.refferalcode = refferalcodeinput.text;

        String test = JsonUtility.ToJson(register);

        using (UnityWebRequest postRequest = new UnityWebRequest(url, "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(test);
            postRequest.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            postRequest.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

            postRequest.SetRequestHeader("Content-Type", "application/json");
            yield return postRequest.SendWebRequest();

            if (postRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(postRequest.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
                JSONObject response = new JSONObject(postRequest.downloadHandler.text);
                Debug.Log(response["status"]);         //shows the status of Email whether it exist or not. true means exist and vice versa.
                Debug.Log(response["msg"]);
                Debug.Log(response["body"]);

                PlayerPrefs.SetString("userID", response["body"].ToString());

                string status = response["status"].ToString();          // checks the message in the status
                string msg = response["msg"].ToString();
                if (status == "true")
                {
                    ui.OTP.SetActive(true);
                    ui.Register.SetActive(false);
                    ui.Info.SetActive(false);
                }
                else
                {
                    warningTextInfoScreen.gameObject.SetActive(true);
                    warningTextInfoScreen.text = msg;
                }
            }
        }
        yield return null;

    }
}

[Serializable]
public class Register
{
    public string Fname;
    public string dob;
    public string mobile;
    public string email;
    public string gender, country, pass, refferalcode;
}


