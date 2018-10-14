using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageCenter : Singleton<MessageCenter>
{
    //委托：消息传递
    public delegate void MessageEventHanlder(KeyValuesUpdate kv);

    public Dictionary<string, MessageEventHanlder> messageDict = new Dictionary<string, MessageEventHanlder>();

    public void AddMsgListener(string messageType, MessageEventHanlder handler)
    {
        if (!messageDict.ContainsKey(messageType))
        {
            messageDict.Add(messageType, null);
        }
        messageDict[messageType] += handler;
    }

    public void RemoveMsgListener(string messageType, MessageEventHanlder handler)
    {
        if (messageDict.ContainsKey(messageType))
        {
            messageDict[messageType] -= handler;
        }
    }
    public void RemoveMsgListener(string messageType)
    {
        if (messageDict.ContainsKey(messageType))
        {
            messageDict.Remove(messageType);
        }
    }
    public void ClearALLMsgListener()
    {
        if (messageDict != null)
        {
            messageDict.Clear();
        }
    }

    public void SendMessage(string messageType, KeyValuesUpdate kv)
    {
        MessageEventHanlder del;
        if (messageDict.TryGetValue(messageType, out del))
        {
            if (del != null)
            {
                del(kv);
            }
        }
    }
}

/// <summary>
/// 键值更新对
/// 功能： 配合委托，实现委托数据传递
/// </summary>
public class KeyValuesUpdate
{
    //消息名称
    private string _Key;
    //消息内容
    private object _Values;

    public string Key
    {
        get { return _Key; }
    }
    public object Values
    {
        get { return _Values; }
        set { _Values = value; }
    }

    public KeyValuesUpdate(string key, object valueObj)
    {
        _Key = key;
        _Values = valueObj;
    }
}
