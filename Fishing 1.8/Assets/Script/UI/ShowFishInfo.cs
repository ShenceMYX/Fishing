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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.transform.parent.gameObject.SetActive(false);
        }
    }

    public void ShowFishInfoUI(string name, float weight)
	{
        fishName = transform.GetChild(1).GetComponent<Text>();
        fishWeight = transform.GetChild(2).GetComponent<Text>();
        fishName.text = name;
        fishWeight.text = weight.ToString();
	}

}
