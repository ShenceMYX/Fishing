using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


///<summary>
///UI上显示鱼信息
///</summary>
public class ShowFishInfo : MonoBehaviour
{
    private Text fishName;
    private Text fishWeight;

    private void Start()
    {
        fishName = transform.GetChild(0).GetComponent<Text>();
        fishWeight=transform.GetChild(1).GetComponent<Text>();
    }

    public void ShowFishInfoUI(string name, float weight)
	{
        fishName.text = name;
        fishWeight.text = weight.ToString();
	}
}
