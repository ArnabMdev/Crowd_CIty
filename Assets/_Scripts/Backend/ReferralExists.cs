using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Defective.JSON;
using System;
using System.Text;
using TMPro;

public class ReferralExists : MonoBehaviour
{
    UIManager ui;
    public TMP_InputField referralCodeInput;
    public TMP_Text warningTextRegisterScreen;
    public TMP_InputField phoneNumber;
    public TMP_InputField userName;
    private void Start()
    {
        ui = FindObjectOfType<UIManager>();
        warningTextRegisterScreen.gameObject.SetActive(false);
    }


    public void nextButton(string url)
    {
        if (phoneNumber.text.Length < 10)
        {
            warningTextRegisterScreen.gameObject.SetActive(true);
            warningTextRegisterScreen.text = "Please Enter Correct Number";
        }
        else if (userName.text.Equals(""))
        {
            warningTextRegisterScreen.gameObject.SetActive(true);
            warningTextRegisterScreen.text = "Please Enter User Name";
        }
        else
        {
            StartCoroutine(referralExists_Courutine(url));
        }
    }

    IEnumerator referralExists_Courutine(string url)
    {
        Referral reff = new Referral();
        reff.referral_code = referralCodeInput.text;

        String test = JsonUtility.ToJson(reff);

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
                Debug.Log(response["bady"]);

                string status = response["status"].ToString();          // checks the message in the status

                if (status == "true")
                {
                    ui.Info.SetActive(true);
                }
                else
                {
                    warningTextRegisterScreen.gameObject.SetActive(true);
                }

            }
        }
        yield return null;

    }

}

[Serializable]
public class Referral
{
    public string referral_code;

}