using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Defective.JSON;
using System;
using System.Text;
using TMPro;

public class LoginData : MonoBehaviour
{

    public TMP_InputField loginPhoneNumberInput;
    //  public TMP_InputField loginPasswordInput;
    public GameObject warningTextLoginScreen;
    private UIManager ui;


    string url = "http://13.127.139.220:5200/api/v_1.0/login";


    private void Start()
    {
        ui = FindObjectOfType<UIManager>();
        warningTextLoginScreen.SetActive(false);

    }

    public void Login(string url)
    {

        if (loginPhoneNumberInput.text.Equals(""))
        {
            warningTextLoginScreen.gameObject.SetActive(true);
        }
        else
        {
            StartCoroutine(LoginRequest_Courutine(url));
            ui.OTP.SetActive(true);
            ui.login.SetActive(false);
        }
    }

    IEnumerator LoginRequest_Courutine(string url)
    {

        inputLoginNumber login = new inputLoginNumber();
        login.mobileNumber = loginPhoneNumberInput.text;


        String test = JsonUtility.ToJson(login);

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


                loginData data = JsonUtility.FromJson<loginData>(postRequest.downloadHandler.text);
              //  Debug.Log(data.body[0].token.ToString());
            }
        }
        yield return null;

    }


}


[Serializable]
public class loginData
{
    public string status;
    public string msg;


    public registerBodyData[] body;
}

[Serializable]

public class registerBodyData
{
    public int user_id;
    public string user_name;
    public string unique_Name;
    public string user_type;
    public string mobile;
    public string email;
    public string pass;
    public string address;
    public string gender;
    public string otp;
    public string token;
}

public class inputLoginNumber
{
    public string mobileNumber;
}

