using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PanelBase : MonoBehaviour
{
    [HideInInspector]
    public object[] args;
    [HideInInspector]
    public PanelType type;
    public bool isShown;
    public string prefabPath;
    public GameObject initedObj;
    public Transform objTrans;

    #region 生命周期封装
    public virtual void Init(string panelName, params object[] args)
    {
        this.args = args;
        GameObject prefab = Resources.Load<GameObject>("UI/" + panelName);
        if (prefab == null)
            prefab = Resources.Load<GameObject>("UI/Common/" + panelName);
        if (prefab == null)
            prefab = Resources.Load<GameObject>("UI/PopupWindow/" + panelName);
        if (prefab == null)
            prefab = Resources.Load<GameObject>("UI/Normal/" + panelName);
        if (prefab == null)
            prefab = Resources.Load<GameObject>("UI/Intro/" + panelName);



        if (prefab == null)
        {
            Debug.LogError("PanelManager.OpenPanel fail, prefab is null, prefabPath = " + panelName);
        }
        initedObj = Instantiate(prefab);
        objTrans = initedObj.transform;
    }
    public virtual void OnShowing() { }
    public virtual void OnShown() { isShown = true; }
    public virtual void OnClosing() { }
    public virtual void OnClosed()
    {
        isShown = false;
    }

    protected virtual void Close()
    {
        string name = this.GetType().ToString();
        PanelManager.instance.ClosePanel(name);
    }
    public virtual void OnDestroyed()
    {
        MessageCenter.instance.RemoveMsgListener(type.ToString());
    }
    #endregion

    #region 常用方法封装
    /// <summary>
    /// 通过EventTriigerListener添加事件触发
    /// </summary>
    /// <param name="delHandle">事件触发处理</param>
    protected void RegistClickEvent(EventTriggerListener.VoidDelegate delHandle)
    {
        EventTriggerListener.Get(initedObj).onClick = delHandle;
    }
    /// <summary>
    /// 通过消息中心发送消息，也可以直接调用消息中心
    /// </summary>
    /// <param name="msgType">消息对象</param>
    /// <param name="msgName">消息名称</param>
    /// <param name="msgContent">消息内容</param>
    protected void SendMsg(string msgType, string msgName, object msgContent)
    {
        KeyValuesUpdate kvs = new KeyValuesUpdate(msgName, msgContent);
        MessageCenter.instance.SendMessage(msgType, kvs);
    }
    #endregion
}
