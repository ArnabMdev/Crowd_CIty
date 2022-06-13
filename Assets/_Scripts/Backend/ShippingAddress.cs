using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Defective.JSON;
using System;
using System.Text;
using UnityEngine.UI;
using TMPro;

public class ShippingAddress : MonoBehaviour
{
    public TMP_InputField nameinput;
    public TMP_InputField addressinput;
    public TMP_InputField mobileinput;
    public TMP_InputField cityinput;
    public TMP_InputField stateinput, countryinput, postal_codeinput;
    public GameObject warningTxt;

    private void Start()
    {
        warningTxt.gameObject.SetActive(false);
    }

    public void Submit(string url)
    {

        if (nameinput.text.Equals("") && addressinput.text.Equals("") && mobileinput.text.Equals("") && cityinput.text.Equals("") && stateinput.text.Equals("") &&
            countryinput.text.Equals("") && postal_codeinput.text.Equals(""))
        {
            warningTxt.gameObject.SetActive(true);
        }
        else
        {
            StartCoroutine(ShippingAddress_Courutine(url));
        }


    }

    IEnumerator ShippingAddress_Courutine(string url)
    {
        userAddress address = new userAddress();

        address.name = nameinput.text;
        Debug.Log(address.name);


        address.address = addressinput.text;
        Debug.Log(address.address);

        address.mobile = mobileinput.text;
        Debug.Log(address.mobile);

        address.city = cityinput.text;
        Debug.Log(address.city);

        address.state = stateinput.text;
        Debug.Log(address.state);

        address.country = countryinput.text;
        Debug.Log(address.country);

        address.postal_code = postal_codeinput.text;
        Debug.Log(address.postal_code);

        address.user_id = PlayerPrefs.GetString("userID");
        Debug.Log(address.user_id);


        /* address.token = PlayerPrefs.GetString("token");
         Debug.Log(address.token);*/

        String test = JsonUtility.ToJson(address);

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

                //   PlayerPrefs.SetString("userID", response["body"].ToString());
            }
        }
        yield return null;

    }
}


[Serializable]
public class userAddress
{
    public string name;
    public string address;
    public string mobile;
    public string city;
    public string state, country, postal_code;
    public string user_id;
    public string token;
}
