using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ActionBarPanel : PanelBase
{
    public List<RoleInfo> actionRolesQueue;
    private Transform parent;
    private Transform[] actionRolePlaces = new Transform[9];
    private int actionCount = 9;
    private GameObject[] actionHeadFrames = new GameObject[9];
    private Image[] actionHeadPics = new Image[9];
    public override void Init(string panelName, params object[] args)
    {
        base.Init(panelName, args);
        prefabPath = "ActionBarPanel";
        type = PanelType.ActionBarPanel;
        MessageCenter.instance.AddMsgListener(type.ToString(), MsgHandle);
        parent = objTrans.Find("ActionBarCenter_Frame/ActionBarCenter_Bg");
        for (int i = 0; i < actionCount; i++)
        {
            actionRolePlaces[i] = parent.GetChild(i);
            actionHeadFrames[i] = objTrans.Find("ActionBarCenter_Frame/ActionBarCenter_Bg").GetChild(i).Find("ActionHead_Frame").gameObject;
            actionHeadPics[i] = actionHeadFrames[i].transform.Find("Head_Pic").GetComponent<Image>();
            EventTriggerListener.Get(actionHeadFrames[i]).onClick = (obj) =>
            {
                RoleInfo role = actionRolesQueue[obj.transform.parent.GetSiblingIndex()];
                KeyValuesUpdate keyvalue = new KeyValuesUpdate("RoleInfo", role);
                MessageCenter.instance.SendMessage(typeof(RoleInfoPanel).ToString(), keyvalue);
            };
        }
        //初始化行动条
        //List<RoleInfo> actionRoles = new List<RoleInfo>();
        //actionRoles.Add(new RoleInfo("name1", 12, 0.5f, AssetsDB.instance.modelsData.FindModel(ModelType.Model1).headPic));
        //actionRoles.Add(new RoleInfo("name2", 11, 0.2f, AssetsDB.instance.modelsData.FindModel(ModelType.Model2).headPic));
        //actionRoles.Add(new RoleInfo("name3", 3, 0.7f, AssetsDB.instance.modelsData.FindModel(ModelType.Model3).headPic));
        //KeyValuesUpdate kv = new KeyValuesUpdate("InitActionBar", actionRoles);
        //MessageCenter.instance.SendMessage(type.ToString(), kv);

        //移除第0个角色
        //KeyValuesUpdate kv2 = new KeyValuesUpdate("RemoveActionRole", 0);
        //MessageCenter.instance.SendMessage(type.ToString(), kv2);

        //添加第1个角色到列表末尾
        //KeyValuesUpdate kv4 = new KeyValuesUpdate("AddActionRole", new RoleInfo("name4", 53, 1f, AssetsDB.instance.modelsData.FindModel(ModelType.Model4).headPic));
        //MessageCenter.instance.SendMessage(type.ToString(), kv4);

        //交换角色位置，从2到1
        //KeyValuesUpdate kv3 = new KeyValuesUpdate("ChangeActionRole", new KeyValuePair<int, int>(2, 1));
        //MessageCenter.instance.SendMessage(type.ToString(), kv3);
    }
    private void MsgHandle(KeyValuesUpdate kv)
    {
        if (kv.Key.Equals("InitActionBar"))
        {
            actionRolesQueue = (List<RoleInfo>)kv.Values;
            for (int i = 0; i < actionRolesQueue.Count; i++)
            {
                actionHeadFrames[i].SetActive(true);
                actionHeadPics[i].sprite = actionRolesQueue[i].headPic;
            }
        }
        else if (kv.Key.Equals("AddActionRole"))
        {
            RoleInfo temp = (RoleInfo)kv.Values;
            parent.GetChild(actionRolesQueue.Count).Find("ActionHead_Frame").gameObject.SetActive(true);
            parent.GetChild(actionRolesQueue.Count).Find("ActionHead_Frame/Head_Pic").GetComponent<Image>().sprite = temp.headPic;
            actionRolesQueue.Add(temp);
        }
        else if (kv.Key.Equals("RemoveActionRole"))
        {
            int index = (int)kv.Values;
            actionRolesQueue.RemoveAt(index);
            parent.GetChild(index).Find("ActionHead_Frame").gameObject.SetActive(false);
            parent.GetChild(index).SetSiblingIndex(9);
        }
        else if (kv.Key.Equals("ChangeActionRole"))
        {
            KeyValuePair<int, int> pair = (KeyValuePair<int, int>)kv.Values;
            parent.GetChild(pair.Key).SetSiblingIndex(pair.Value);
            RoleInfo temp = actionRolesQueue[pair.Key];
            actionRolesQueue.RemoveAt(pair.Key);
            actionRolesQueue.Insert(pair.Value, temp);
            for (int i = 0; i < actionRolesQueue.Count; i++)
            {
                Debug.Log(actionRolesQueue[i].roleName);
            }
        }

    }
}