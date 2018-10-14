using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FastBarPanel : PanelBase
{
    private GameObject[] fastItemsObj = new GameObject[10];
    private readonly Image[] fastItemsPic = new Image[10];
    private readonly Image[] fastItemsMask = new Image[10];
    private readonly bool[] isFastItemsColding = new bool[10];    
    public override void Init(string panelName, params object[] args)
    {
        base.Init(panelName, args);
        prefabPath = "FastBarPanel";
        type = PanelType.FastBarPanel;
        initedObj.name = prefabPath;

        MessageCenter.instance.AddMsgListener(initedObj.name, MsgHandle);
        for (int i = 0; i < objTrans.childCount; i++)
        {
            
            fastItemsObj[i] = objTrans.GetChild(i).Find("FastItem_Pic").gameObject;
            fastItemsPic[i] = fastItemsObj[i].GetComponent<Image>();
            fastItemsMask[i] = fastItemsObj[i].transform.Find("FastItem_Pic_Mask").GetComponent<Image>();
            EventTriggerListener.Get(fastItemsPic[i].gameObject, i).onClick = (obj) =>
            {
                KeyValuesUpdate kv = new KeyValuesUpdate("StartCold", obj.GetComponent<EventTriggerListener>().param[0]);
                MessageCenter.instance.SendMessage(initedObj.name, kv);
            };
            objTrans.GetChild(i).GetComponentInChildren<Text>().text = ((i + 1) % 10).ToString();

            if (PlayerData.instance.playerEntity.fastBar[i].fastItemType == FastItemType.Null)
            {
                fastItemsPic[i].gameObject.SetActive(false);
            }
            else
            {
                fastItemsPic[i].sprite = PlayerData.instance.playerEntity.fastBar[i].icon;
                fastItemsMask[i].sprite = PlayerData.instance.playerEntity.fastBar[i].icon;
            }
            
        }
    }


    private void MsgHandle(KeyValuesUpdate kv)
    {
        if (kv.Key.Equals("StartCold"))
        {
            StartCoroutine(StartCold((int)kv.Values));
        }
    }
    private IEnumerator StartCold(int index)
    {
        float coldTime = 5;
        float step = 0.03f;
        float finish = 0;
        if (isFastItemsColding[index])
        {
            yield break;
        }
        isFastItemsColding[index] = true;
        while (true)
        {
            finish += step;
            fastItemsMask[index].fillAmount = 1 - finish / coldTime;
            if (finish >= coldTime)
            {
                isFastItemsColding[index] = false;
                break;
            }
            yield return new WaitForSeconds(step);
        }
    }
}
