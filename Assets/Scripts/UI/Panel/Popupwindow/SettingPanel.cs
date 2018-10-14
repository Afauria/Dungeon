using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingPanel : PanelBase
{
    private GameObject closeBtn;
    private GameObject backToHallBtn;
    private GameObject backToLogin;
    private GameObject saveGameBtn;
    public override void Init(string panelName, params object[] args)
    {
        base.Init(panelName, args);
        prefabPath = "SettingPanel";
        type = PanelType.SettingPanel;
        initedObj.AddComponent<IPanelDragable>();
        closeBtn = objTrans.Find("CancelBtn_Bg").gameObject;
        backToLogin = objTrans.Find("BackLoginBtn_Bg").gameObject;
        backToHallBtn = objTrans.Find("BackHallBtn_Bg").gameObject;
        saveGameBtn = objTrans.Find("SaveBtn_Bg").gameObject;
        EventTriggerListener.Get(saveGameBtn).onClick = (obj) =>
        {
            WebUtil.instance.SaveGame(PlayerData.instance.playerEntity, SaveGameCallBack);
        };
        EventTriggerListener.Get(closeBtn).onClick = (obj) =>
        {
            KeyValuesUpdate kv = new KeyValuesUpdate("MenuItemClick", PanelType.SettingPanel);
            MessageCenter.instance.SendMessage(PanelType.MenuPanel.ToString(), kv);
        };
        EventTriggerListener.Get(backToLogin).onClick = (obj) =>
        {
            SceneMgr.instance.BackToLogin();
        };
        EventTriggerListener.Get(backToHallBtn).onClick = (obj) =>
        {
            SceneMgr.instance.BackToHall();
        };
    }
    private void SaveGameCallBack(RemoteResult<PlayerEntity> result)
    {
        if (result.code == 0)
        {
            KeyValuesUpdate kv = new KeyValuesUpdate("Error", result.message);
            MessageCenter.instance.SendMessage("ErrorPanel", kv);
        }
        else
        {
            Debug.Log(result.message);
            KeyValuesUpdate kv = new KeyValuesUpdate("Tips", result.message);
            MessageCenter.instance.SendMessage("TipsPanel", kv);
        }
    }
}