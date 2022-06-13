using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Defective.JSON;
using System;
using System.Text;
using TMPro;

public class ResendOTP : MonoBehaviour
{


    public void resendOTP(string url)
    {
        StartCoroutine(LoginRequest_Courutine(url));
    }


    IEnumerator LoginRequest_Courutine(string url)
    {
        resndOTP otp = new resndOTP();
        otp.user_id = PlayerPrefs.GetString("userID");
        Debug.Log(otp.user_id);

        String test = JsonUtility.ToJson(otp);

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

                //  Debug.Log(data.body[0].token.ToString());
            }
        }
        yield return null;

    }
}

[Serializable]
public class resndOTP
{
    public string user_id;
}