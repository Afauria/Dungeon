using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TipsPanel : PanelBase
{
    private Text tipsText;
    public override void Init(string panelName, params object[] args)
    {
        base.Init(panelName, args);
        prefabPath = "TipsPanel";
        type = PanelType.TipsPanel;
        initedObj.name = prefabPath;
        tipsText = objTrans.Find("TipsPanel_Bg/Tips_Text").GetComponent<Text>();
        MessageCenter.instance.AddMsgListener(type.ToString(), MsgHandle);
    }
    private void MsgHandle(KeyValuesUpdate kv)
    {
        StartCoroutine(TipAuto(kv.Values.ToString()));
    }
    IEnumerator TipAuto(string str)
    {
        tipsText.text = str;
        PanelManager.instance.TogglePanel<TipsPanel>();
        yield return new WaitForSeconds(1.5f);
        PanelManager.instance.TogglePanel<TipsPanel>();
    }
}