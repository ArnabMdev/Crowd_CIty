using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Defective.JSON;
using System;
using System.Text;
using UnityEngine.UI;
using TMPro;
public class updateUser : MonoBehaviour
{
    public TMP_InputField nameInput;
    public TMP_InputField addressInput;
    public TMP_InputField dobInput;

    public TMP_Dropdown genderInput;
    public GameObject genderObject;
    int genderValue;

    private void Start()
    {
        
        genderInput = genderObject.GetComponent<TMP_Dropdown>();

    }

    private void Update()
    {
        genderValue = genderInput.value;
       // Debug.Log(genderValue);
    }
    public void Register(string url)
    {
        StartCoroutine(UserRegister_Courutine(url));
    }

    IEnumerator UserRegister_Courutine(string url)
    {
        updateRegisteredUser update = new updateRegisteredUser();
        update.name = nameInput.text;
        update.address = addressInput.text;
        update.dob = dobInput.text;
        update.gender = genderValue + 1.ToString();

        //String test = JsonUtility.ToJson(update);
        WWWForm updateuser = new WWWForm();
        updateuser.AddField("name", nameInput.text);
        updateuser.AddField("address", addressInput.text);
        updateuser.AddField("gender", genderValue + 1.ToString());
        updateuser.AddField("dob", dobInput.text);


        using (UnityWebRequest updateUSerInfo = UnityWebRequest.Put("https://www.my-server.com/myform", url))
        {
            yield return updateUSerInfo.SendWebRequest();

            if (updateUSerInfo.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(updateUSerInfo.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
                JSONObject response = new JSONObject(updateUSerInfo.downloadHandler.text);
                Debug.Log(response["status"]);

            }
        }


    }

}

[Serializable]
public class updateRegisteredUser
{
    public string name;
    public string address;
    public string dob;
    public string gender;
}
