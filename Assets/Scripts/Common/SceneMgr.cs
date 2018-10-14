using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneMgr : Singleton<SceneMgr>
{
    void Start()
    {
        InitLoginScene();
    }

    public void EnterGame()
    {
        StartCoroutine(EnterGameCoroutine());
    }
    
    public void BackToLogin()
    {
        StartCoroutine(BackToLoginCoroutine());
    }
    public void BackToHall()
    {
        StartCoroutine(BackToHallCoroutine());
    }
    IEnumerator EnterGameCoroutine()
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync("GameScene");
        ao.allowSceneActivation = false;

        KeyValuesUpdate kv = new KeyValuesUpdate("Loading", "StartLoading");
        if (!PanelManager.instance.IsShown<LoadingPanel>())
            PanelManager.instance.TogglePanel<LoadingPanel>();
        while (ao.progress < 0.9f)
        {
            MessageCenter.instance.SendMessage("LoadingPanel", kv);
            yield return new WaitForEndOfFrame();
        }
        kv.Values = "EndLoading";
        MessageCenter.instance.SendMessage("LoadingPanel", kv);
        ao.allowSceneActivation = true;
        if (PanelManager.instance.IsShown<LoadingPanel>())
            PanelManager.instance.TogglePanel<LoadingPanel>();
        if (PanelManager.instance.IsShown<ChoosePlayerPanel>())
            PanelManager.instance.TogglePanel<ChoosePlayerPanel>();
        InitGameScene();
    }
    IEnumerator BackToLoginCoroutine()
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync("StartScene");
        ao.allowSceneActivation = false;

        KeyValuesUpdate kv = new KeyValuesUpdate("Loading", "StartLoading");
        if (!PanelManager.instance.IsShown<LoadingPanel>())
            PanelManager.instance.TogglePanel<LoadingPanel>();
        while (ao.progress < 0.9f)
        {
            MessageCenter.instance.SendMessage("LoadingPanel", kv);
            yield return new WaitForEndOfFrame();
        }
        kv.Values = "EndLoading";
        MessageCenter.instance.SendMessage("LoadingPanel", kv);
        ao.allowSceneActivation = true;
        if (PanelManager.instance.IsShown<LoadingPanel>())
            PanelManager.instance.TogglePanel<LoadingPanel>();
        PanelManager.instance.CloseAllPanel();
        PlayerData.instance.ClearInfo();
        InitLoginScene();
    }
    IEnumerator BackToHallCoroutine()
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync("StartScene");
        ao.allowSceneActivation = false;

        KeyValuesUpdate kv = new KeyValuesUpdate("Loading", "StartLoading");
        if (!PanelManager.instance.IsShown<LoadingPanel>())
            PanelManager.instance.TogglePanel<LoadingPanel>();
        while (ao.progress < 0.9f)
        {
            MessageCenter.instance.SendMessage("LoadingPanel", kv);
            yield return new WaitForEndOfFrame();
        }
        kv.Values = "EndLoading";
        MessageCenter.instance.SendMessage("LoadingPanel", kv);
        ao.allowSceneActivation = true;
        if (PanelManager.instance.IsShown<LoadingPanel>())
            PanelManager.instance.TogglePanel<LoadingPanel>();
        PanelManager.instance.CloseAllPanel();
        PlayerData.instance.ClearInfo();
        InitLoginScene();

        PanelManager.instance.TogglePanel<LoginPanel>();
        PanelManager.instance.TogglePanel<ChoosePlayerPanel>();
        WebUtil.instance.GetUserPlayer(PlayerData.instance.userId, GetUserPlayerCallback);
    }
    private void GetUserPlayerCallback(RemoteResult<PlayerEntity[]> result)
    {
        PlayerEntity[] players = result.data;
        foreach (PlayerEntity player in players)
        {
            KeyValuesUpdate kv= new KeyValuesUpdate("SetPlayer", player);
            MessageCenter.instance.SendMessage("ChoosePlayerPanel", kv);
        }
    }

    public void InitLoginScene()
    {
        PanelManager.instance.OpenPanel<ErrorPanel>();
        PanelManager.instance.TogglePanel<ErrorPanel>();
        PanelManager.instance.OpenPanel<TipsPanel>();
        PanelManager.instance.TogglePanel<TipsPanel>();
        PanelManager.instance.OpenPanel<LoadingPanel>();
        PanelManager.instance.TogglePanel<LoadingPanel>();
        PanelManager.instance.OpenPanel<LoginPanel>();
        
        //PanelManager.instance.OpenPanel<PickPanel>();
        //List<object> values = new List<object>();
        //values.Add(AssetsDB.instance.propsData.medicines[0]);
        //values.Add(AssetsDB.instance.equipsData.weapons[0]);
        //KeyValuesUpdate kv = new KeyValuesUpdate("",values);
        //MessageCenter.instance.SendMessage(typeof(PickPanel).ToString(), kv);
    }
    public void InitGameScene()
    {
        //固定界面
        PanelManager.instance.OpenPanel<MenuPanel>();
        PanelManager.instance.OpenPanel<HealthPanel>();
        PanelManager.instance.OpenPanel<FastBarPanel>();
        PanelManager.instance.OpenPanel<ActionBarPanel>();
        PanelManager.instance.OpenPanel<HeadPanel>();
        PanelManager.instance.OpenPanel<RoleInfoPanel>();
        //隐藏界面
        PanelManager.instance.TogglePanel<ActionBarPanel>();
        PanelManager.instance.TogglePanel<RoleInfoPanel>();
        //弹窗初始化
        PanelManager.instance.OpenPanel<PropsPanel>();
        PanelManager.instance.OpenPanel<SkillPanel>();
        PanelManager.instance.OpenPanel<PlayerPanel>();
        PanelManager.instance.OpenPanel<SettingPanel>();
        //隐藏弹窗
        PanelManager.instance.TogglePanel<PropsPanel>();
        PanelManager.instance.TogglePanel<SkillPanel>();
        PanelManager.instance.TogglePanel<PlayerPanel>();
        PanelManager.instance.TogglePanel<SettingPanel>();
    }
    //IEnumerator LoadSceneProgress()
    //{
    //    int displayProgress = 0;
    //    int targetProgress = 0;
    //    AsyncOperation ao = SceneManager.LoadSceneAsync("GameScene");
    //    ao.allowSceneActivation = false;

    //    KeyValuesUpdate kv = new KeyValuesUpdate("UpdateProcess",0);
    //    if (!PanelManager.instance.IsShown<LoadingPanel>())
    //        PanelManager.instance.TogglePanel<LoadingPanel>();
    //    while (ao.progress < 0.9f)
    //    {
    //        Debug.Log(ao.progress);
    //        targetProgress = (int)(ao.progress * 100);

    //        while (displayProgress < targetProgress)
    //        {
    //            displayProgress++;
    //            //percentText.text = displayProgress + "%";
    //            //loadBar.fillAmount = (float)displayProgress / 100;
    //            kv.Values= (float)displayProgress / 100;
    //            MessageCenter.instance.SendMessage("LoadingPanel", kv);
    //            yield return new WaitForEndOfFrame();
    //        }
    //    }
    //    targetProgress = 100;
    //    while (displayProgress < targetProgress)
    //    {
    //        displayProgress++;
    //        //percentText.text = displayProgress + "%";
    //        //loadBar.fillAmount = (float)displayProgress / 100;
    //        kv.Values = (float)displayProgress / 100;
    //        MessageCenter.instance.SendMessage("LoadingPanel", kv);
    //        yield return new WaitForEndOfFrame();
    //    }

    //    ao.allowSceneActivation = true;
    //    if (PanelManager.instance.IsShown<LoadingPanel>())
    //        PanelManager.instance.TogglePanel<LoadingPanel>();
    //}
}
