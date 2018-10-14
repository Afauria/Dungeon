using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPanel : PanelBase
{
    private Image healthBar;
    public override void Init(string panelName, params object[] args)
    {
        base.Init(panelName, args);
        prefabPath = "HealthPanel";
        type = PanelType.HealthPanel;
        initedObj.name = prefabPath;
        healthBar = objTrans.Find("Health_Bar").GetComponent<Image>();
        //添加消息处理监听方法
        MessageCenter.instance.AddMsgListener(initedObj.name, MsgHandle);
        //注册点击事件
        RegistClickEvent(p =>
        {
            SendMsg(initedObj.name, "UpdateHealth", 0.3f);
        });
    }
    //消息处理方法
    private void MsgHandle(KeyValuesUpdate kv)
    {
        if (kv.Key.Equals("UpdateHealth"))
            healthBar.fillAmount = (float)kv.Values;
    }

}
