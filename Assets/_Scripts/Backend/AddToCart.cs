using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Defective.JSON;
using System;
using System.Text;
using TMPro;


public class AddToCart : MonoBehaviour
{

   /* IEnumerator AddToCart_Courutine(string url)
    {
        cartData cart = new cartData();
        cart.user_id =

        String test = JsonUtility.ToJson(cart);

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


            }
        }
        yield return null;

    }*/
}


[Serializable]
public class cartData
{
    public string user_id;
    public string product_id;
    public string qty;
}