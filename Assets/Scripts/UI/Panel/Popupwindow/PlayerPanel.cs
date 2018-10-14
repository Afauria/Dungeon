using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerPanel : PanelBase
{
    private GameObject closeBtn;
    private GameObject leftRotateBtn;
    private GameObject rightRotateBtn;
    private Vector3 leftRotation = new Vector3(0, 3, 0);
    private Vector3 rightRotation = new Vector3(0, -3, 0);
    //装备设置
    private Transform equipPanel;
    private Transform playerModelParent;
    private GameObject playerModel;
    private Image equipHeader;
    private Image equipUpper;
    private Image equipLower;
    private Image equipWeapon;
    //属性设置
    private Transform playerRightPanel;
    private GameObject content;
    private Image headPic;
    private Text introText;
    private Text[] ablitityCount = new Text[11];
    private Text[] baseAblitityCount = new Text[9];
    private Transform[] abilityObjs = new Transform[11];
    private Transform[] baseAbilityObjs = new Transform[9];
    private Coroutine coroutine;
    public override void Init(string panelName, params object[] args)
    {
        base.Init(panelName, args);
        prefabPath = "PlayerPanel";
        type = PanelType.PlayerPanel;
        initedObj.name = prefabPath;
        initedObj.AddComponent<IPanelDragable>();
        closeBtn = objTrans.Find("PlayerPanelLeft_Bg1/PlayerPanelLeft_Bg3/CloseBtn_Frame").gameObject;
        EventTriggerListener.Get(closeBtn).onClick = (obj) =>
        {
            KeyValuesUpdate kv = new KeyValuesUpdate("MenuItemClick", PanelType.PlayerPanel);
            MessageCenter.instance.SendMessage(PanelType.MenuPanel.ToString(), kv);
        };
        playerRightPanel = objTrans.Find("PlayerPanelRight_Bg1");
        content = playerRightPanel.Find("PlayerPanelRight_Bg2/Ability_Scroll View/Viewport/Content").gameObject;
        headPic = content.transform.Find("PlayerInfo/PlayerHeadPic_Bg/PlayerHeadPic").GetComponent<Image>();
        introText = content.transform.Find("PlayerInfo/PlayerIntro_Text").GetComponent<Text>();
        equipPanel = objTrans.Find("PlayerPanelLeft_Bg1/PlayerPanelLeft_Bg2/EquipPanel");
        equipHeader = equipPanel.Find("EquipHead_frame/EquipHead_bg/EquipHead_pic").GetComponent<Image>();
        equipUpper = equipPanel.Find("EquipUpper_frame/EquipUpper_bg/EquipUpper_pic").GetComponent<Image>();
        equipLower = equipPanel.Find("EquipLower_frame/EquipLower_bg/EquipLower_pic").GetComponent<Image>();
        equipWeapon = equipPanel.Find("EquipWeapon_frame/EquipWeapon_bg/EquipWeapon_pic").GetComponent<Image>();
        playerModelParent = GameObject.Find("PlayerPreview_Camera2").transform.Find("PreviewModelParent");

        leftRotateBtn = objTrans.Find("PlayerPanelLeft_Bg1/PlayerPanelLeft_Bg2/LeftRotateBtn_Frame").gameObject;
        rightRotateBtn = objTrans.Find("PlayerPanelLeft_Bg1/PlayerPanelLeft_Bg2/RightRotateBtn_Frame").gameObject;
        EventTriggerListener.Get(leftRotateBtn).onDown = (obj) =>
        {
            coroutine=StartCoroutine(RotateModel(leftRotation));
        };
        EventTriggerListener.Get(rightRotateBtn).onDown = (obj) =>
         {
             coroutine=StartCoroutine(RotateModel(rightRotation));
         };
        EventTriggerListener.Get(leftRotateBtn).onUp = (obj) =>
        {
            StopCoroutine(coroutine);
        };
        EventTriggerListener.Get(rightRotateBtn).onUp = (obj) =>
        {
            StopCoroutine(coroutine);
        };
        for (int i = 0; i < 9; i++)
        {
            baseAbilityObjs[i] = content.transform.GetChild(i + 2);
            baseAbilityObjs[i].gameObject.AddComponent<IHoverable>();
            baseAbilityObjs[i].GetComponent<IHoverable>().content = AssetsDB.instance.baseAbilityDes[i].Value;
            baseAbilityObjs[i].GetComponent<IHoverable>().strClass = typeof(AbilityIntroPanel).ToString();
            baseAblitityCount[i] = baseAbilityObjs[i].Find("AbilityCount").GetComponent<Text>();
        }
        for (int i = 0; i < 11; i++)
        {
            abilityObjs[i] = content.transform.GetChild(i + 12);
            abilityObjs[i].gameObject.AddComponent<IHoverable>();
         //   abilityObjs[i].GetComponent<IHoverable>().content = AssetsDB.instance.descriptionData.ability[i].description;
            abilityObjs[i].GetComponent<IHoverable>().strClass = typeof(AbilityIntroPanel).ToString();
            ablitityCount[i] = abilityObjs[i].Find("AbilityCount").GetComponent<Text>();
        }
        SetAbilityPanel(PlayerData.instance.selectedPlayerEntity);
        SetPlayerEquip(PlayerData.instance.selectedPlayerEntity);
        MessageCenter.instance.AddMsgListener(type.ToString(), MsgHandle);
    }
    private void MsgHandle(KeyValuesUpdate kv)
    {
        if (kv.Key.Equals("ChangeSelectedRole"))
        {
            SetAbilityPanel(PlayerData.instance.selectedPlayerEntity);
            SetPlayerEquip(PlayerData.instance.selectedPlayerEntity);
        }
    }
    private void SetAbilityPanel(PlayerEntity playerEntity)
    {
        ModelEntity modelEntity = AssetsDB.instance.modelsData.FindModel(playerEntity.modelType);
        headPic.sprite = modelEntity.headPic;
        introText.text = modelEntity.intro;
        for (int i = 0; i < ablitityCount.Length; i++)
        {
            ablitityCount[i].text = playerEntity.ability[i].value.ToString();
        }
        for (int i = 0; i < baseAblitityCount.Length; i++)
        {
            baseAblitityCount[i].text = playerEntity.baseAbility[i].value.ToString();
        }
    }
    private void SetPlayerEquip(PlayerEntity player)
    {
        ModelEntity modelEntity = AssetsDB.instance.modelsData.FindModel(player.modelType);
        equipHeader.gameObject.SetActive(false);
        equipUpper.gameObject.SetActive(false);
        equipLower.gameObject.SetActive(false);
        equipWeapon.gameObject.SetActive(false);
        foreach (MyKeyValuePair<EquipType> equip in player.equiped)
        {
            if (equip.value == EquipType.Header)
            {
                equipHeader.gameObject.SetActive(true);
                equipHeader.sprite = AssetsDB.instance.equipsData.FindEquip(equip.key, equip.value).equipIcon;
            }
            else if (equip.value == EquipType.Upper)
            {
                equipUpper.gameObject.SetActive(true);
                equipUpper.sprite = AssetsDB.instance.equipsData.FindEquip(equip.key, equip.value).equipIcon;
            }
            else if (equip.value == EquipType.Lower)
            {
                equipLower.gameObject.SetActive(true);
                equipLower.sprite = AssetsDB.instance.equipsData.FindEquip(equip.key, equip.value).equipIcon;
            }
            else if (equip.value == EquipType.Weapon)
            {
                equipWeapon.gameObject.SetActive(true);
                equipWeapon.sprite = AssetsDB.instance.equipsData.FindEquip(equip.key, equip.value).equipIcon;
            }

        }
        Destroy(playerModelParent.GetChild(0).gameObject);
        playerModel = Instantiate(modelEntity.model, playerModelParent.transform);
    }
    private IEnumerator RotateModel(Vector3 rotation)
    {
        while (true)
        {
            playerModel.transform.Rotate(rotation);
            yield return new WaitForEndOfFrame();
        }
    }
}

