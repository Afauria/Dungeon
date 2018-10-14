using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChoosePlayerPanel : PanelBase
{
    //角色创建
    private GameObject toggleCreatePanelBtn;
    private GameObject createPlayerPanel;
    private bool isShowCreatePanel = false;
    private GameObject createBtn;
    private GameObject genderLeftBtn;
    private GameObject genderRightBtn;
    private GameObject modelLeftBtn;
    private GameObject modelRightBtn;
    private Text genderText;
    private Text modelText;
    private Text playerNameText;
    private readonly int modelCount = 2;
    private Gender currentGender = Gender.M;
    private ModelType currentModel = ModelType.Model1;
    private int currentModelIndex = 0;
    //角色显示
    private GameObject playerItemPanel;
    private int playerCount = 0;
    private int selectPlayerIndex = 0;

    //进入游戏
    private GameObject enterGameBtn;

    //角色预览
    private GameObject playerPreviewPanel;
    private GameObject content;
    private Image previewHeadPic;
    private Text previewIntroText;
    private Text[] previewAblitityCount = new Text[11];
    private Text[] previewBaseAblitityCount = new Text[9];
    private Transform previewModelParent;
    private RawImage previewModelRender;
    private GameObject model;
    public override void Init(string panelName, params object[] args)
    {
        base.Init(panelName, args);
        prefabPath = "ChoosePlayerPanel";
        type = PanelType.ChoosePlayerPanel;
        initedObj.name = prefabPath;
        createPlayerPanel = objTrans.Find("CreatePlayerPanel").gameObject;
        toggleCreatePanelBtn = objTrans.Find("CreatePlayerBtn").gameObject;
        createBtn = createPlayerPanel.transform.Find("CreateBtn").gameObject;
        genderLeftBtn = createPlayerPanel.transform.Find("GenderChoose_Bg/GenderLeftBtn_Bg").gameObject;
        genderRightBtn = createPlayerPanel.transform.Find("GenderChoose_Bg/GenderRightBtn_Bg").gameObject;
        modelLeftBtn = createPlayerPanel.transform.Find("ModelChoose_Bg/ModelLeftBtn_Bg").gameObject;
        modelRightBtn = createPlayerPanel.transform.Find("ModelChoose_Bg/ModelRightBtn_Bg").gameObject;
        genderText = createPlayerPanel.transform.Find("GenderChoose_Bg/GenderChoose_Text").GetComponent<Text>();
        modelText = createPlayerPanel.transform.Find("ModelChoose_Bg/ModelChoose_Text").GetComponent<Text>();
        playerNameText = createPlayerPanel.transform.Find("NameInput_Bg/NameInput_Text").GetComponent<Text>();
        playerItemPanel = objTrans.Find("PlayerItemPanel").gameObject;
        enterGameBtn = objTrans.Find("Bottom_Bg/EnterGameBtn_Bg").gameObject;

        playerPreviewPanel = objTrans.Find("PlayerPreviewPanel").gameObject;
        content = playerPreviewPanel.transform.Find("PlayerAbilityPanel_Bg1/PlayerAbilityPanel_Bg2/Ability_Scroll View/Viewport/Content").gameObject;
        previewHeadPic = content.transform.Find("PlayerInfo/PlayerHeadPic_Bg/PlayerHeadPic").GetComponent<Image>();
        previewIntroText = content.transform.Find("PlayerInfo/PlayerIntro_Text").GetComponent<Text>();
        previewModelParent = GameObject.Find("PlayerPreview_Camera").transform.Find("PreviewModelParent");
        previewModelRender = playerPreviewPanel.transform.Find("PlayerModelPreview").GetComponent<RawImage>();
        previewModelRender.gameObject.AddComponent<IRotatable>().model = previewModelParent.GetChild(0).gameObject;

        for (int i = 0; i < 9; i++)
        {
            previewBaseAblitityCount[i] = content.transform.GetChild(i + 2).Find("AbilityCount").GetComponent<Text>();
        }
        for (int i = 0; i < 11; i++)
        {
            previewAblitityCount[i] = content.transform.GetChild(i + 12).Find("AbilityCount").GetComponent<Text>();
        }

        MessageCenter.instance.AddMsgListener(type.ToString(), MsgHandle);
        EventTriggerListener.Get(toggleCreatePanelBtn).onClick = (obj) =>
        {
            ToggleCreatePanel();
        };
        EventTriggerListener.Get(genderLeftBtn).onClick = (obj) =>
        {
            LoopGender();
        };
        EventTriggerListener.Get(genderRightBtn).onClick = (obj) =>
        {
            LoopGender();
        };
        EventTriggerListener.Get(modelLeftBtn).onClick = (obj) =>
        {
            LoopModel(true);
        };
        EventTriggerListener.Get(modelRightBtn).onClick = (obj) =>
        {
            LoopModel(false);
        };
        EventTriggerListener.Get(createBtn).onClick = (obj) =>
        {
            WebUtil.instance.AddPlayer(PlayerData.instance.userId, playerNameText.text, currentGender, currentModel, AddPlayerCallback);
        };
        EventTriggerListener.Get(enterGameBtn).onClick = (obj) =>
        {
            SceneMgr.instance.EnterGame();
        };
    }
    private void MsgHandle(KeyValuesUpdate kv)
    {
        //显示该用户角色
        if (kv.Key.Equals("SetPlayer"))
        {
            PlayerEntity player = (PlayerEntity)kv.Values;
            GameObject prefab = Resources.Load<GameObject>("UI/PlayerItem_Bg");
            GameObject obj = Instantiate(prefab, playerItemPanel.transform.GetChild(playerCount));
            obj.name = "PlayerItem_Bg";
            Text playerName = obj.transform.Find("PlayerName_Text").GetComponent<Text>();
            Text playerLevel = obj.transform.Find("PlayerLevel_Text").GetComponent<Text>();
            Image headPic = obj.transform.Find("HeadPicRound_Mask/HeadPic").GetComponent<Image>();
            GameObject select = obj.transform.Find("HeadPic_Bg_Select").gameObject;
            playerCount++;
            playerName.text = player.playerName;
            playerLevel.text = "等级" + player.level.ToString();
            headPic.sprite = AssetsDB.instance.modelsData.FindModel(player.modelType).headPic;
            //headPic.sprite = AssetsDB.instance.modelsData.FindModel(ModelType.Model1).headPic;
            EventTriggerListener.Get(obj).onClick = (p) =>
            {
                //选中头像
                playerItemPanel.transform.GetChild(selectPlayerIndex).Find("PlayerItem_Bg/HeadPic_Bg_Select").gameObject.SetActive(false);
                selectPlayerIndex = obj.transform.parent.GetSiblingIndex();
                select.SetActive(true);
                PreviewPlayer(player);
                PreviewModel(player.modelType);
                playerPreviewPanel.SetActive(true);
                PlayerData.instance.playerEntity = player;
                PlayerData.instance.selectedPlayerEntity = player;

                //初始化数据
                List<MyKeyValuePair<int>> propPacks = new List<MyKeyValuePair<int>>();
                propPacks.Add(new MyKeyValuePair<int>("加速药水", 2));
                PlayerData.instance.playerEntity.propsPack = propPacks;

                List<MyKeyValuePair<EquipType>> equiped = new List<MyKeyValuePair<EquipType>>();
                equiped.Add(new MyKeyValuePair<EquipType>("学徒帽", EquipType.Header));
                equiped.Add(new MyKeyValuePair<EquipType>("钉锤", EquipType.Weapon));
                PlayerData.instance.playerEntity.equiped = equiped;

                List<PlayerEntity> partners = new List<PlayerEntity>();
                PlayerData.instance.emptyPlayers[1].playerName = "partner22";
                PlayerData.instance.emptyPlayers[2].playerName = "partner33";
                List<MyKeyValuePair<int>> propPacks2 = new List<MyKeyValuePair<int>>();
                propPacks2.Add(new MyKeyValuePair<int>("小血瓶", 1));
                propPacks2.Add(new MyKeyValuePair<int>("加速药水", 5));
                PlayerData.instance.emptyPlayers[1].propsPack = propPacks2;

                List<MyKeyValuePair<EquipType>> equiped2 = new List<MyKeyValuePair<EquipType>>();
                equiped2.Add(new MyKeyValuePair<EquipType>("学徒帽", EquipType.Header));
                equiped2.Add(new MyKeyValuePair<EquipType>("大砍刀", EquipType.Weapon));
                PlayerData.instance.emptyPlayers[2].equiped = equiped2;

                partners.Add(PlayerData.instance.emptyPlayers[1]);
                partners.Add(PlayerData.instance.emptyPlayers[2]);
                PlayerData.instance.playerEntity.partners = partners;

                for (int i = 0; i < 10; i++)
                {
                    PlayerData.instance.playerEntity.fastBar[i].icon = AssetsDB.instance.FindFastItem(
                        PlayerData.instance.playerEntity.fastBar[i].fastItemType.ToString(), 
                        PlayerData.instance.playerEntity.fastBar[i].fastItemName);
                }

            };
        }
    }
    private void AddPlayerCallback(RemoteResult<PlayerEntity> result)
    {
        if (result.code == 0)
        {
            KeyValuesUpdate kv = new KeyValuesUpdate("Error", result.message);
            MessageCenter.instance.SendMessage("ErrorPanel", kv);
        }
        else
        {
            KeyValuesUpdate kv = new KeyValuesUpdate("SetPlayer", result.data);
            MessageCenter.instance.SendMessage("ChoosePlayerPanel", kv);
            ToggleCreatePanel();
        }
    }
    private void LoopModel(bool isLeft)
    {
        string str1 = modelText.text.Substring(0, 2);
        int i;
        if (isLeft)
        {
            i = (int.Parse(modelText.text.Substring(2)) + modelCount - 1) % modelCount;
        }
        else
        {
            i = (int.Parse(modelText.text.Substring(2)) + 1) % modelCount;
        }
        modelText.text = str1 + i;
        currentModelIndex = i;
        PreviewModel();
    }
    private void LoopGender()
    {
        if (genderText.text.Equals("男"))
        {
            genderText.text = "女";
            currentGender = Gender.F;
        }
        else
        {
            genderText.text = "男";
            currentGender = Gender.M;
        }
        PreviewModel();


    }
    private void PreviewModel()
    {
        if (currentGender == Gender.F)
        {
            currentModel = (currentModelIndex == 0) ? ModelType.Model3 : ModelType.Model4;
        }
        else
        {
            currentModel = (currentModelIndex == 0) ? ModelType.Model1 : ModelType.Model2;
        }
        Destroy(previewModelParent.GetChild(0).gameObject);
        ModelEntity modelEntity = AssetsDB.instance.modelsData.FindModel(currentModel);
        model = Instantiate(modelEntity.model, previewModelParent.transform);
        previewModelRender.GetComponent<IRotatable>().model = model;
        previewHeadPic.sprite = modelEntity.headPic;
        previewIntroText.text = modelEntity.intro;
        foreach (PlayerEntity player in PlayerData.instance.emptyPlayers)
        {
            if (player.modelType == currentModel)
            {
                PreviewPlayer(player);
            }
        }
    }
    private void PreviewModel(ModelType modelType)
    {
        ModelEntity modelEntity = AssetsDB.instance.modelsData.FindModel(modelType);
        Destroy(previewModelParent.GetChild(0).gameObject);
        model = Instantiate(modelEntity.model, previewModelParent.transform);
        previewModelRender.GetComponent<IRotatable>().model = model;
        previewHeadPic.sprite = modelEntity.headPic;
        previewIntroText.text = modelEntity.intro;
    }
    private void PreviewPlayer(PlayerEntity playerEntity)
    {
        for (int i = 0; i < previewAblitityCount.Length; i++)
        {
            previewAblitityCount[i].text = playerEntity.ability[i].value.ToString();
        }
        for (int i = 0; i < previewBaseAblitityCount.Length; i++)
        {
            previewBaseAblitityCount[i].text = playerEntity.baseAbility[i].value.ToString();
        }
    }

    private void ToggleCreatePanel()
    {
        isShowCreatePanel = !isShowCreatePanel;

        createPlayerPanel.SetActive(isShowCreatePanel);
        playerPreviewPanel.SetActive(isShowCreatePanel);
        if (isShowCreatePanel)
        {
            PreviewModel();
            PreviewPlayer(PlayerData.instance.emptyPlayers[0]);
        }
    }
}
