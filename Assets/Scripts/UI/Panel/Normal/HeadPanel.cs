using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HeadPanel : PanelBase
{
    public List<GameObject> groupsObj = new List<GameObject>();
    public List<List<RoleInfo>> groupsRoles = new List<List<RoleInfo>>();
    private GameObject roleItemPrefab;
    private GameObject connectionPrefab;
    private GameObject groupPrefab;
    private GameObject preSelected;
    public override void Init(string panelName, params object[] args)
    {
        base.Init(panelName, args);
        prefabPath = "HeadPanel";
        type = PanelType.HeadPanel;
        initedObj.name = prefabPath;
        roleItemPrefab = Resources.Load<GameObject>("UI/Normal/HeadPanel_Frame");
        connectionPrefab = Resources.Load<GameObject>("UI/Normal/HeadPanel_Connection");
        groupPrefab = Resources.Load<GameObject>("UI/Normal/HeadGroup");

        //测试用
        //List<List<RoleInfo>> roles = new List<List<RoleInfo>>();
        //List<RoleInfo> roleGroup1 = new List<RoleInfo>();
        //roleGroup1.Add(new RoleInfo("name1", 1, 0.3f, AssetsDB.instance.modelsData.FindModel(ModelType.Model1).headPic));
        //roleGroup1.Add(new RoleInfo("name2", 2, 0.3f, AssetsDB.instance.modelsData.FindModel(ModelType.Model2).headPic));
        //List<RoleInfo> roleGroup2 = new List<RoleInfo>();
        //roleGroup2.Add(new RoleInfo("name3", 3, 0.3f, AssetsDB.instance.modelsData.FindModel(ModelType.Model3).headPic));
        //List<RoleInfo> roleGroup3 = new List<RoleInfo>();
        //roleGroup3.Add(new RoleInfo("name4", 3, 0.3f, AssetsDB.instance.modelsData.FindModel(ModelType.Model4).headPic));
        //roles.Add(roleGroup1);
        //roles.Add(roleGroup2);
        //roles.Add(roleGroup3);

        List<List<RoleInfo>> roles = new List<List<RoleInfo>>();
        List<RoleInfo> roleGroup1 = new List<RoleInfo>();
        List<PlayerEntity> partners = PlayerData.instance.playerEntity.partners;
        roleGroup1.Add(new RoleInfo(PlayerData.instance.playerEntity.playerName, 1, 0.3f, AssetsDB.instance.modelsData.FindModel(PlayerData.instance.playerEntity.modelType).headPic));
        for (int i = 0; i < partners.Count; i++)
        {
            roleGroup1.Add(new RoleInfo(partners[i].playerName, partners[i].level, partners[i].curHp, AssetsDB.instance.modelsData.FindModel(partners[i].modelType).headPic));
        }
       
        roles.Add(roleGroup1);

        MessageCenter.instance.AddMsgListener(type.ToString(), MsgHandle);
        KeyValuesUpdate kv = new KeyValuesUpdate("InitGroup", roles);
        MessageCenter.instance.SendMessage(type.ToString(), kv);
    }
    private void MsgHandle(KeyValuesUpdate kv)
    {
        if (kv.Key.Equals("InitGroup"))
        {
            for(int i = 0; i < groupsObj.Count; i++)
            {
                Destroy(groupsObj[i]);
            }
            groupsObj.Clear();
            groupsRoles = (List<List<RoleInfo>>)kv.Values;
            for (int i = 0; i < groupsRoles.Count; i++)
            {
                groupsObj.Add(Instantiate(groupPrefab,objTrans));
                for (int j = 0; j < groupsRoles[i].Count; j++)
                {
                    GameObject roleItem = Instantiate(roleItemPrefab, groupsObj[i].transform);
                    if (j < groupsRoles[i].Count - 1)
                        Instantiate(connectionPrefab, groupsObj[i].transform);
                    roleItem.tag = MyTag.GROUP_HEAD_TAG;
                    roleItem.AddComponent<IGroupDragable>().roleInfo = groupsRoles[i][j];
                    Image headPic = roleItem.transform.Find("Head_Pic").GetComponent<Image>();
                    Text levelText = roleItem.transform.Find("Level_Text").GetComponent<Text>();
                    GameObject selectMask = roleItem.transform.Find("Head_Frame_Select").gameObject;
                    headPic.sprite = groupsRoles[i][j].headPic;
                    levelText.text = groupsRoles[i][j].roleLevel.ToString();
                    EventTriggerListener.Get(roleItem,groupsRoles[i][j].roleName).onClick = (obj) =>
                    {
                        if(preSelected!=null)
                            preSelected.SetActive(false);
                        selectMask.SetActive(true);
                        preSelected = selectMask;
                        if (obj.GetComponent<EventTriggerListener>().param[0].ToString().Equals(PlayerData.instance.playerEntity.playerName))
                        {
                            PlayerData.instance.selectedPlayerEntity = PlayerData.instance.playerEntity;
                            KeyValuesUpdate kv2 = new KeyValuesUpdate("ChangeSelectedRole", null);
                            MessageCenter.instance.SendMessage(typeof(PlayerPanel).ToString(), kv2);
                            MessageCenter.instance.SendMessage(typeof(PropsPanel).ToString(), kv2);
                        }
                        foreach (PlayerEntity partner in PlayerData.instance.playerEntity.partners) {
                            if (obj.GetComponent<EventTriggerListener>().param[0].ToString().Equals(partner.playerName))
                            {
                                PlayerData.instance.selectedPlayerEntity = partner;
                                KeyValuesUpdate kv2 = new KeyValuesUpdate("ChangeSelectedRole",null);
                                MessageCenter.instance.SendMessage(typeof(PlayerPanel).ToString(),kv2);
                                MessageCenter.instance.SendMessage(typeof(PropsPanel).ToString(), kv2);
                            }
                        }
                    };
                }
            }
        }
        else if (kv.Key.Equals("ChangeGroup"))
        {
            ChangeGroupInfo changeGroupInfo = (ChangeGroupInfo)kv.Values;
            groupsRoles[changeGroupInfo.targetGroup].Add(changeGroupInfo.roleInfo);
            groupsRoles[changeGroupInfo.originGroup].Remove(changeGroupInfo.roleInfo);
            if (groupsRoles[changeGroupInfo.originGroup].Count == 0)
            {
                groupsRoles.RemoveAt(changeGroupInfo.originGroup);
            }
            KeyValuesUpdate kv2 = new KeyValuesUpdate("InitGroup", groupsRoles);
            MessageCenter.instance.SendMessage(type.ToString(), kv2);
        }
        else if (kv.Key.Equals("CreateGroup"))
        {
            ChangeGroupInfo changeGroupInfo = (ChangeGroupInfo)kv.Values;
            List<RoleInfo> newGroup=new List<RoleInfo>();
            newGroup.Add(changeGroupInfo.roleInfo);
            groupsRoles.Add(newGroup);
            groupsRoles[changeGroupInfo.originGroup].Remove(changeGroupInfo.roleInfo);
            if (groupsRoles[changeGroupInfo.originGroup].Count==0)
            {
                groupsRoles.RemoveAt(changeGroupInfo.originGroup);
            }
            KeyValuesUpdate kv2 = new KeyValuesUpdate("InitGroup", groupsRoles);
            MessageCenter.instance.SendMessage(type.ToString(), kv2);
        }
    }
}
