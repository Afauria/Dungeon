using System.Collections.Generic;
using UnityEngine;
using System;

public enum PanelType
{
    MenuPanel,
    HealthPanel,
    PropsPanel,
    SettingPanel,
    SkillPanel,
    PlayerPanel,
    FastBarPanel,
    Tips,
    ActionBarPanel,
    HeadPanel,
    LoadingPanel,
    ErrorPanel,
    LoginPanel,
    TipsPanel,
    ChoosePlayerPanel,
    AbilityIntroPanel,
    SkillIntroPanel,
    RoleInfoPanel,
    ActionPointPanel,
    PickPanel,
    EquipIntroPanel,
    PropIntroPanel,
    BattleHandlePanel
}

public class PanelManager : Singleton<PanelManager>
{
    private GameObject canvas;
    //已经打开的面板
    public Dictionary<string, PanelBase> opendPanelDict;
    //面板的父物体
    private Dictionary<PanelType, Transform> panelParentDict;
    public Transform tipsParent;
    protected override void Awake()
    {
        base.Awake();
        InitLayer();
        opendPanelDict = new Dictionary<string, PanelBase>();
        tipsParent = GameObject.Find("TipsParent").transform;
    }

    private void InitLayer()
    {
        canvas = GameObject.Find("Canvas");
        if (canvas == null)
            Debug.LogError("panelMgr.InitLayerfail, canvas is null");
        panelParentDict = new Dictionary<PanelType, Transform>();

        foreach (PanelType panelType in Enum.GetValues(typeof(PanelType)))
        {
            string panelParentName = panelType.ToString() + "Parent";
            Transform transform = GameObject.Find(panelParentName).transform;
            panelParentDict.Add(panelType, transform);
        }
    }

    public void OpenPanel<T>(params object[] args) where T : PanelBase
    {
        string panelName = typeof(T).ToString();
        if (opendPanelDict.ContainsKey(panelName))
            return;

        PanelBase panel = canvas.AddComponent<T>();
        panel.Init(panelName, args);
        opendPanelDict.Add(panelName, panel);
        Transform objTrans = panel.objTrans;
        PanelType layer = panel.type;
        Transform parent = panelParentDict[layer];
        objTrans.SetParent(parent, false);

        panel.OnShowing();
        panel.OnShown();
    }

    public void TogglePanel<T>(params object[] args) where T : PanelBase
    {
        PanelBase panel;
        string panelName = typeof(T).ToString();
        opendPanelDict.TryGetValue(panelName, out panel);
        if (panel == null)
        {
            OpenPanel<T>(args);
        }
        else
        {
            panel.isShown = !panel.isShown;
            if (panel.isShown)
            {
                panel.OnShowing();
                panel.OnShown();
            }
            else
            {
                panel.OnClosing();
                panel.OnClosed();
            }
            
            panel.initedObj.SetActive(panel.isShown);
        }
    }

    public void ClosePanel(string panelName)
    {
        PanelBase panel = opendPanelDict[panelName];
        if (panel == null)
            return;

        panel.OnClosing();
        opendPanelDict.Remove(panelName);
        panel.OnClosed();
        panel.OnDestroyed();
        GameObject.Destroy(panel.initedObj);
        Component.Destroy(panel);
    }
    public bool IsShown<T>() where T : PanelBase
    {
        string panelName = typeof(T).ToString();
        PanelBase panel;
        opendPanelDict.TryGetValue(panelName, out panel);
        if (panel == null)
            return false;
        if (panel.isShown)
            return true;
        return false;
    }
    public void CloseAllPanel()
    {
        //迭代过程中不允许修改
        foreach (string key in opendPanelDict.Keys)
        {
            PanelBase panel = opendPanelDict[key];
            panel.OnClosing();
            panel.OnClosed();
            panel.OnDestroyed();
            GameObject.Destroy(panel.initedObj);
            Component.Destroy(panel);
        }
        opendPanelDict.Clear();
    }
    public PanelBase GetPanelBase<T>() where T : PanelBase
    {
        string panelName = typeof(T).ToString();
        PanelBase panel = opendPanelDict[panelName];
        return panel;
    }
}