using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MenuPanel : PanelBase
{
    private Button playerItemBtn;
    private Button propItemBtn;
    private Button skillItemBtn;
    private Button taskItemBtn;
    private Button settingItemBtn;
    private RectTransform playerItemTrans;
    private RectTransform propItemTrans;
    private RectTransform skillItemTrans;
    private RectTransform taskItemTrans;
    private RectTransform settingItemTrans;
    private Vector2 hidePos = new Vector2(30, 0);
    private Vector2 shownPos = new Vector2(0, 0);
    public override void Init(string panelName, params object[] args)
    {
        base.Init(panelName, args);
        prefabPath = "MenuPanel";
        type = PanelType.MenuPanel;

        playerItemBtn = objTrans.Find("MenuItemPanel/PlayerItemBtn").GetComponent<Button>();
        propItemBtn = objTrans.Find("MenuItemPanel/PropItemBtn").GetComponent<Button>();
        skillItemBtn = objTrans.Find("MenuItemPanel/SkillItemBtn").GetComponent<Button>();
        taskItemBtn = objTrans.Find("MenuItemPanel/TaskItemBtn").GetComponent<Button>();
        settingItemBtn = objTrans.Find("MenuItemPanel/SettingItemBtn").GetComponent<Button>();

        //不用button组件的原因：需要对外暴露方法或对象，外部需要获取该类的引用，使用消息中心转发消息可以回避引用问题
        //playerItemBtn.onClick.AddListener(OnPlayerItemClick);
        //propItemBtn.onClick.AddListener(OnPropItemClick);
        //skillItemBtn.onClick.AddListener(OnSkillItemClick);
        //taskItemBtn.onClick.AddListener(OnTaskItemClick);
        //settingItemBtn.onClick.AddListener(OnSettingItemClick);
        MessageCenter.instance.AddMsgListener(type.ToString(), MsgHandle);
        //lambda表达式
        EventTriggerListener.Get(playerItemBtn.gameObject).onClick = (obj) =>
        {
            KeyValuesUpdate kv = new KeyValuesUpdate("MenuItemClick", PanelType.PlayerPanel);
            MessageCenter.instance.SendMessage(type.ToString(), kv);
        };
        EventTriggerListener.Get(propItemBtn.gameObject).onClick = (obj) =>
        {
            KeyValuesUpdate kv = new KeyValuesUpdate("MenuItemClick", PanelType.PropsPanel);
            MessageCenter.instance.SendMessage(type.ToString(), kv);
        };
        EventTriggerListener.Get(skillItemBtn.gameObject).onClick = (obj) =>
        {
            KeyValuesUpdate kv = new KeyValuesUpdate("MenuItemClick", PanelType.SkillPanel);
            MessageCenter.instance.SendMessage(type.ToString(), kv);
        };
        //EventTriggerListener.Get(taskItemBtn.gameObject).onClick = (obj) =>
        //{
        //    KeyValuesUpdate kv = new KeyValuesUpdate("MenuItemClick", PanelType.PlayerPanel.ToString());
        //    MessageCenter.instance.SendMessage(type.ToString(), kv);
        //};
        EventTriggerListener.Get(settingItemBtn.gameObject).onClick = (obj) =>
        {
            KeyValuesUpdate kv = new KeyValuesUpdate("MenuItemClick", PanelType.SettingPanel);
            MessageCenter.instance.SendMessage(type.ToString(), kv);
        };
        playerItemTrans = playerItemBtn.GetComponent<RectTransform>();
        propItemTrans = propItemBtn.GetComponent<RectTransform>();
        skillItemTrans = skillItemBtn.GetComponent<RectTransform>();
        taskItemTrans = taskItemBtn.GetComponent<RectTransform>();
        settingItemTrans = settingItemBtn.GetComponent<RectTransform>();
    }
    private void MsgHandle(KeyValuesUpdate kv)
    {
        if (kv.Key.Equals("MenuItemClick"))
        {
            switch ((PanelType)kv.Values)
            {
                case PanelType.PlayerPanel:
                    OnPlayerItemClick();
                    break;
                case PanelType.PropsPanel:
                    OnPropItemClick();
                    break;
                case PanelType.SkillPanel:
                    OnSkillItemClick();
                    break;
                case PanelType.SettingPanel:
                    OnSettingItemClick();
                    break;
            }
        }
    }
    private void OnPlayerItemClick()
    {
        PanelManager.instance.TogglePanel<PlayerPanel>();
        ToggleMenuItem<PlayerPanel>(playerItemTrans);
    }
    private void OnPropItemClick()
    {
        PanelManager.instance.TogglePanel<PropsPanel>();
        ToggleMenuItem<PropsPanel>(propItemTrans);
    }
    private void OnSkillItemClick()
    {
        PanelManager.instance.TogglePanel<SkillPanel>();
        ToggleMenuItem<SkillPanel>(skillItemTrans);
    }
    private void OnTaskItemClick()
    {

    }
    private void OnSettingItemClick()
    {
        PanelManager.instance.TogglePanel<SettingPanel>();
        ToggleMenuItem<SettingPanel>(settingItemTrans);
    }
    public void ToggleMenuItem<T>(RectTransform trans) where T : PanelBase
    {
        if (PanelManager.instance.IsShown<T>())
        {
            trans.localPosition = shownPos;
        }
        else
        {
            trans.localPosition = hidePos;
        }
    }
}