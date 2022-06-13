using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Defective.JSON;
using System;
using System.Text;
using UnityEngine.UI;
using TMPro;
public class OTPVerify : MonoBehaviour
{
    public TMP_InputField otpInput;
    UIManager ui;
    public TMP_Text warningTextOTPVerify;


    private void Start()
    {
        ui = FindObjectOfType<UIManager>();
        warningTextOTPVerify.gameObject.SetActive(false);
    }

    public void verifyOTP(string url)
    {

        if (otpInput.text.Equals(""))
        {
            warningTextOTPVerify.gameObject.SetActive(true);
            warningTextOTPVerify.text = "Please Enter OTP";
        }
        else if(otpInput.text.Length < 6)
        {
            warningTextOTPVerify.gameObject.SetActive(true);
            warningTextOTPVerify.text = "Please Enter Correct OTP";
        }
        else
        {
            StartCoroutine(otpVerification(url));
        }
    }

    IEnumerator otpVerification(string url)
    {
        inputOTP otp = new inputOTP();
        otp.otp = otpInput.text;
        otp.user_id = PlayerPrefs.GetString("userID");


        String test = JsonUtility.ToJson(otp);

        using (UnityWebRequest postRequest = new UnityWebRequest(url, "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(test);
            postRequest.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            postRequest.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

            postRequest.SetRequestHeader("Content-Type", "application/json");

            yield return postRequest.SendWebRequest();

            if (postRequest.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Form upload complete!");
                JSONObject response = new JSONObject(postRequest.downloadHandler.text);
                Debug.Log(response["status"]);         //shows the status of Email whether it exist or not. true means exist and vice versa.
                Debug.Log(response["msg"]);
                Debug.Log(response["body"]);

                JSONObject bodyToken = new JSONObject(response["body"].ToString());
                Debug.Log(bodyToken["token"]);


                string token = bodyToken["token"].ToString();
                token = token.Trim('"');
                PlayerPrefs.SetString("token", token);
                Debug.Log(PlayerPrefs.GetString("token", "null"));

                string status = response["status"].ToString();          // checks the message in the status
                string msg = response["msg"].ToString();
                if (status == "true")
                {
                    ui.menuPanel.SetActive(true);
                    ui.OTP.SetActive(false);
                }
                else
                {
                    warningTextOTPVerify.gameObject.SetActive(true);
                    warningTextOTPVerify.text = "Please Enter Correct OTP";


                }
                //  userDetailResponse UDR = JsonUtility.FromJson<userDetailResponse>(postRequest.downloadHandler.text);
                //  Debug.Log(UDR.body[0].token.ToString());                                    This doesnt work 
                //  PlayerPrefs.SetString("userToken", UDR.body[0].token.ToString());


            }
            else
            {
                Debug.Log(postRequest.error);
            }
        }
        yield return null;

    }
}


[Serializable]
public class OTP
{
    public string status;
    public string msg;

    // public userDetail[] body;
}

[Serializable]
public class inputOTP
{
    public string otp;
    public string user_id;
}
