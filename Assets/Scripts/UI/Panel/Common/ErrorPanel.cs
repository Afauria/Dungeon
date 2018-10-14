using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ErrorPanel : PanelBase {
    private GameObject closeBtn;
    private Text errorText;
    public override void Init(string panelName, params object[] args)
    {
        base.Init(panelName, args);
        prefabPath = "ErrorPanel";
        type = PanelType.ErrorPanel;
        initedObj.name = prefabPath;
        closeBtn = objTrans.Find("ErrorPanel_Bg/CloseBtn_Frame").gameObject;
        errorText= objTrans.Find("ErrorPanel_Bg/Error_Text").GetComponent<Text>();
        MessageCenter.instance.AddMsgListener(type.ToString(),MsgHandle);
        EventTriggerListener.Get(closeBtn).onClick = (obj) =>
        {
            PanelManager.instance.TogglePanel<ErrorPanel>();
        };
    }
    private void MsgHandle(KeyValuesUpdate kv)
    {
        if(!PanelManager.instance.IsShown<ErrorPanel>())
            PanelManager.instance.TogglePanel<ErrorPanel>();
        errorText.text = kv.Values.ToString();
    }
}
