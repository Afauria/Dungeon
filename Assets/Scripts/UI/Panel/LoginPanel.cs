using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LoginPanel : PanelBase
{
    private GameObject mainBtn;
    private GameObject changeBtn;
    private Text mainBtnText;
    private Text changeBtnText;
    private Text usernameText;
    private Text passwordText;
    private GameObject exitGameBtn;
    public override void Init(string panelName, params object[] args)
    {
        base.Init(panelName, args);
        prefabPath = "LoginPanel";
        type = PanelType.LoginPanel;
        initedObj.name = prefabPath;
        mainBtn = objTrans.Find("LoginPanel_Bg/MainBtn_Bg").gameObject;
        changeBtn= objTrans.Find("LoginPanel_Bg/ChangeBtn_Text").gameObject;
        mainBtnText = mainBtn.transform.Find("MainBtn_Text").GetComponent<Text>();
        usernameText = objTrans.Find("LoginPanel_Bg/LoginPanel_Bg1/Username_Bg/Username_Text").GetComponent<Text>();
        passwordText = objTrans.Find("LoginPanel_Bg/LoginPanel_Bg1/Password_Bg/Password_Text").GetComponent<Text>();
        changeBtnText = changeBtn.GetComponent<Text>();
        exitGameBtn = objTrans.Find("ExitBtn").gameObject;
        EventTriggerListener.Get(mainBtn).onClick=(obj)=>
        {
            if (mainBtnText.text.Equals("登录"))
            {
                Login();
            }
            else if (mainBtnText.text.Equals("注册"))
            {
                Regist();
            }
        };
        EventTriggerListener.Get(changeBtn).onClick = (obj) =>
        {
            if (changeBtnText.text.Equals("注册"))
            {
                changeBtnText.text = "登录";
                mainBtnText.text = "注册";
            }
            else if (changeBtnText.text.Equals("登录"))
            {
                changeBtnText.text = "注册";
                mainBtnText.text = "登录";
            }
        };
        EventTriggerListener.Get(exitGameBtn).onClick = (obj) =>
        {
            Application.Quit();
        };
    }
    private void Login()
    {
        WebUtil.instance.CheckLogin(usernameText.text,passwordText.text,LoginCallback);
    }
    private void Regist()
    {
        WebUtil.instance.AddUser(usernameText.text, passwordText.text, RegistCallback);
    }
    private void LoginCallback(RemoteResult<UserEntity> result)
    {
        if (result.code == 0)
        {
            KeyValuesUpdate kv = new KeyValuesUpdate("Error", result.message);
            MessageCenter.instance.SendMessage("ErrorPanel", kv);
        }
        else
        {
            KeyValuesUpdate kv = new KeyValuesUpdate("Tips", result.message);
            MessageCenter.instance.SendMessage("TipsPanel", kv);
            PanelManager.instance.TogglePanel<LoginPanel>();
            PanelManager.instance.TogglePanel<ChoosePlayerPanel>();
            PlayerData.instance.userId =result.data.userId;
            WebUtil.instance.GetUserPlayer(PlayerData.instance.userId, GetUserPlayerCallback);
            WebUtil.instance.GetEmptyPlayer(delegate (RemoteResult<List<PlayerEntity>> result2) {
                PlayerData.instance.emptyPlayers = result2.data;
            });
        }
    }
    private void RegistCallback(RemoteResult<UserEntity> result)
    {
        if(result.code==0)
        {
            KeyValuesUpdate kv = new KeyValuesUpdate("Error", result.message);
            MessageCenter.instance.SendMessage("ErrorPanel", kv);
        }
        else
        {
            KeyValuesUpdate kv = new KeyValuesUpdate("Tips", result.message);
            MessageCenter.instance.SendMessage("TipsPanel", kv);
            EventTriggerListener.Get(changeBtn).onClick.Invoke(changeBtn);
        }
    }

    private void GetUserPlayerCallback(RemoteResult<PlayerEntity[]> result)
    {
        if (result.code ==1)
        {
            PlayerEntity[] players = result.data;
            foreach (PlayerEntity player in players)
            {
                //显示用户所有角色头像
                KeyValuesUpdate kv2 = new KeyValuesUpdate("SetPlayer", player);
                MessageCenter.instance.SendMessage("ChoosePlayerPanel", kv2); 
            }
        }
        else
        {
            //提示该用户还没有创建角色
            KeyValuesUpdate kv = new KeyValuesUpdate("Tips", result.message);
            MessageCenter.instance.SendMessage("TipsPanel", kv);
        }
        
    }
}
