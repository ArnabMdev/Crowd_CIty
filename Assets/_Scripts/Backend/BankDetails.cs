using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Defective.JSON;
using System;
using System.Text;
using UnityEngine.UI;
using TMPro;

public class BankDetails : MonoBehaviour
{

    public TMP_InputField banknameinput;
    public TMP_InputField bank_account_noinput;
    public TMP_InputField ifscInput;
    public void Submit(string url)
    {
        StartCoroutine(Bank_Courutine(url));
    }


    IEnumerator Bank_Courutine(string url)
    {

        bank bankDetails = new bank();
        bankDetails.bank_name = banknameinput.text;
        bankDetails.bank_account_no = bank_account_noinput.text;
        bankDetails.ifsc_code = ifscInput.text;
        bankDetails.user_id = PlayerPrefs.GetString("userID");


        String test = JsonUtility.ToJson(bankDetails);



        using (UnityWebRequest postRequest = new UnityWebRequest(url, "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(test);
            postRequest.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            postRequest.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();


            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("token", PlayerPrefs.GetString("token"));
            postRequest.SetRequestHeader("token", PlayerPrefs.GetString("token"));

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


            }
        }
        yield return null;

    }

}



[Serializable]
public class bank
{
    public string bank_name;
    public string bank_account_no;
    public string token;
    public string ifsc_code;
    public string user_id;
}
