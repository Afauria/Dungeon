using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;
using UnityEngine.UI;
public class WebUtil : Singleton<WebUtil>
{

    string baseUrl = "http://zhangwy.xin:8081/GameServer/";
    public delegate void WebCallback<T>(RemoteResult<T> result);
    IEnumerator DB_Request<E>(string url, WWWForm form, WebCallback<E> webCallback)
    {
        WWW www = new WWW(baseUrl + url, form);
        if (!PanelManager.instance.IsShown<LoadingPanel>())
            PanelManager.instance.TogglePanel<LoadingPanel>();
        //KeyValuesUpdate kv = new KeyValuesUpdate("UpdateProcess", 0f);
        //while (!www.isDone)
        //{
        //    kv.Values = www.progress;
        //}
        //kv.Values = 1;
        //MessageCenter.instance.SendMessage("LoadingPanel", kv);
        KeyValuesUpdate kv = new KeyValuesUpdate("Loading", "StartLoading");
        MessageCenter.instance.SendMessage("LoadingPanel", kv);
        yield return www;
        kv.Values = "EndLoading";
        MessageCenter.instance.SendMessage("LoadingPanel", kv);
        if (PanelManager.instance.IsShown<LoadingPanel>())
            PanelManager.instance.TogglePanel<LoadingPanel>();

        Debug.Log(www.text);
        RemoteResult<E> result = JsonConvert.DeserializeObject<RemoteResult<E>>(www.text);
        if (result == null)
        {
            Debug.LogError("网络请求出错！");
            KeyValuesUpdate keyvalue = new KeyValuesUpdate("Error", "网络请求出错！");
            MessageCenter.instance.SendMessage("ErrorPanel", keyvalue);
        }
        else
        {
            webCallback(result);
        }
    }

    IEnumerator DB_RequestJson<E>(string url, string jsonStr, WebCallback<E> webCallback)
    {
        Dictionary<string, string> headers = new Dictionary<string, string>
        {
            { "Content-Type", "application/json" }
        };

        byte[] bs = System.Text.UTF8Encoding.UTF8.GetBytes(jsonStr);
        WWW www = new WWW(baseUrl + url, bs, headers);
        if (!PanelManager.instance.IsShown<LoadingPanel>())
            PanelManager.instance.TogglePanel<LoadingPanel>();
        KeyValuesUpdate kv = new KeyValuesUpdate("Loading", "StartLoading");
        MessageCenter.instance.SendMessage("LoadingPanel", kv);
        yield return www;
        kv.Values = "EndLoading";
        MessageCenter.instance.SendMessage("LoadingPanel", kv);
        if (PanelManager.instance.IsShown<LoadingPanel>())
            PanelManager.instance.TogglePanel<LoadingPanel>();

        Debug.Log(www.text);
        RemoteResult<E> result = JsonConvert.DeserializeObject<RemoteResult<E>>(www.text);
        if (result == null)
        {
            Debug.LogError("网络请求出错！");
            KeyValuesUpdate keyvalue = new KeyValuesUpdate("Error", "网络请求出错！");
            MessageCenter.instance.SendMessage("ErrorPanel", keyvalue);
        }
        webCallback(result);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <param name="webCallback"></param>
    public void AddUser(string username, string password, WebCallback<UserEntity> webCallback)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);
        StartCoroutine(DB_Request("addUser", form, webCallback));
    }
    public void CheckLogin(string username, string password, WebCallback<UserEntity> webCallback)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);
        StartCoroutine(DB_Request("/checkLogin", form, webCallback));
    }
    public void GetEmptyPlayer( WebCallback<List<PlayerEntity>> webCallback)
    {
        WWWForm form = new WWWForm();
        StartCoroutine(DB_Request("/getEmptyPlayer", form, webCallback));
    }
    public void AddPlayer(int userId, string playerName, Gender gender, ModelType modelType, WebCallback<PlayerEntity> webCallback)
    {
        WWWForm form = new WWWForm();
        form.AddField("userId", userId);
        form.AddField("playerName", playerName);
        form.AddField("gender", gender.ToString());
        form.AddField("modelType", modelType.ToString());
        StartCoroutine(DB_Request("addPlayer", form, webCallback));
    }
    public void GetUserPlayer(int userId, WebCallback<PlayerEntity[]> webCallback)
    {
        WWWForm form = new WWWForm();
        form.AddField("userId", userId);
        StartCoroutine(DB_Request("getUserPlayer", form, webCallback));
    }
    public void SaveGame(PlayerEntity playerEntity, WebCallback<PlayerEntity> webCallback)
    {
        Debug.Log(JsonConvert.SerializeObject(playerEntity));
        StartCoroutine(DB_RequestJson("saveGame", JsonConvert.SerializeObject(playerEntity), webCallback));
    }
    public void LoadGame(int playerId, WebCallback<PlayerEntity> webCallback)
    {
        WWWForm form = new WWWForm();
        form.AddField("playerId", playerId);
        StartCoroutine(DB_Request("loadGame", form, webCallback));

    }
}