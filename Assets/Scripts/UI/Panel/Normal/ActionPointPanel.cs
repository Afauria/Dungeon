using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPointPanel : PanelBase
{
    //private GameObject actionPointPrefab;
    private int curCount = 0;
    public override void Init(string panelName, params object[] args)
    {
        base.Init(panelName, args);
        prefabPath = "ActionPointPanel";
        type = PanelType.ActionPointPanel;
        initedObj.name = prefabPath;
        //actionPointPrefab = Resources.Load<GameObject>("UI/ActionPoint");
        MessageCenter.instance.AddMsgListener(type.ToString(), MsgHandle);
    }
    private void MsgHandle(KeyValuesUpdate kv)
    {
        //提高效率
        int newCount = int.Parse(kv.Values.ToString());
        if (newCount > curCount)
            for (int i = 0; i < newCount - curCount; i++)
            {
                objTrans.GetChild(i + curCount).gameObject.SetActive(true);
            }
        else
        {
            for (int i = 0; i < curCount - newCount; i++)
            {
                objTrans.GetChild(i + newCount).gameObject.SetActive(false);
            }
        }
        curCount = newCount;
    }
}
