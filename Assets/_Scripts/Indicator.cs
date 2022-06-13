﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Indicator : MonoBehaviour
{

    public TextMeshProUGUI onScreenText;
    public TextMeshProUGUI onScreenName;
    public LocationIndicator locationIndicator;
    GroupData data;
    public Image[] imageRenders;

    private void Start()
    {
        transform.position = new Vector3(0,0,8000);

    }
    public void setUpIndicator(GroupData data)
    {

        this.data = data;
        locationIndicator.TargetToIndicate = data.groupLeader;
        locationIndicator.enabled = true;
        ChangeColor(data.leaderMaterial.GetColor("_Color"));
        UpdateText();
    }

    public void OnDeath()
    {
        gameObject.SetActive(false);
    }
    // Use this for initialization
    public void UpdateText()
    {

        onScreenText.text = "" + this.data.GroupCount;
        onScreenName.text = "" + data.LeaderName;
        //  Debug.Log("Changed Text " + onScreenText.text);

    }
    public void ChangeColor(Color color)
    {
        foreach (var item in imageRenders)
        {
            item.color = color;
        }
         onScreenName.color = color;

        //   Debug.Log("Changed COlor" + color);
    }
}
