using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PropsPanel : PanelBase
{
    private GameObject[] propsItemsObj = new GameObject[30];
    private GameObject closeBtn;
    private List<MyKeyValuePair<int>> props;
    private Transform scrollContent;
    public override void Init(string panelName, params object[] args)
    {
        base.Init(panelName, args);
        prefabPath = "PropsPanel";
        type = PanelType.PropsPanel;
        initedObj.name = prefabPath;
        initedObj.AddComponent<IPanelDragable>();
        closeBtn = objTrans.Find("PropsPanelTop_Bg/CloseBtn_Frame").gameObject;
        scrollContent = objTrans.Find("PropsPanel_Bg/Props_Scroll View/Viewport/Content");
        

        EventTriggerListener.Get(closeBtn).onClick = (obj) =>
        {
            KeyValuesUpdate kv = new KeyValuesUpdate("MenuItemClick", PanelType.PropsPanel);
            MessageCenter.instance.SendMessage(PanelType.MenuPanel.ToString(), kv);
        };
        MessageCenter.instance.AddMsgListener(type.ToString(), MsgHandle);
        SetProps(PlayerData.instance.selectedPlayerEntity);
    }
    private void MsgHandle(KeyValuesUpdate kv)
    {
        if (kv.Key.Equals("ChangeSelectedRole"))
        {
            SetProps(PlayerData.instance.selectedPlayerEntity);
        }
    }
    private void SetProps(PlayerEntity player)
    {
        props = player.propsPack;
        
        for(int i = 0; i <propsItemsObj.Length;i++)
        {
            Destroy(propsItemsObj[i]);
        }
        int index = 0;
        foreach (MyKeyValuePair<int> kv in props)
        {
            GameObject prefab = Resources.Load<GameObject>("UI/PopupWindow/PropItem_Pic");
            propsItemsObj[index] = Instantiate(prefab, scrollContent.GetChild(index).Find("Prop_Frame"));
            propsItemsObj[index].tag = MyTag.PROP_ICON_TAG;
            propsItemsObj[index].AddComponent<IDragable>();
            propsItemsObj[index].GetComponent<IDragable>().itemName = kv.key;
            PropEntity propEntity = AssetsDB.instance.propsData.FindProp(kv.key);
            if (propEntity != null)
            {
                propsItemsObj[index].GetComponent<Image>().sprite = propEntity.propIcon;
                propsItemsObj[index].transform.Find("PropItem_Pic_Mask").GetComponent<Image>().sprite = propEntity.propIcon;
                propsItemsObj[index].AddComponent<IHoverable>();
                propsItemsObj[index].GetComponent<IHoverable>().strClass = typeof(PropIntroPanel).ToString();
                propsItemsObj[index].GetComponent<IHoverable>().content = propEntity;
                propsItemsObj[index].transform.Find("PropItem_Count_Text").GetComponent<Text>().text = kv.value.ToString();
                propsItemsObj[index].GetComponent<IDragable>().itemType=FastItemType.Prop;
            }
            else
            {
                EquipEntity equipEntity = AssetsDB.instance.equipsData.FindEquip(kv.key);
                propsItemsObj[index].GetComponent<Image>().sprite = equipEntity.equipIcon;
                propsItemsObj[index].transform.Find("PropItem_Pic_Mask").GetComponent<Image>().sprite = equipEntity.equipIcon;
                propsItemsObj[index].AddComponent<IHoverable>();
                propsItemsObj[index].GetComponent<IHoverable>().strClass=typeof(EquipIntroPanel).ToString();
                propsItemsObj[index].GetComponent<IHoverable>().content=equipEntity;
                propsItemsObj[index].transform.Find("PropItem_Count_Text").GetComponent<Text>().text = kv.value.ToString();
                propsItemsObj[index].GetComponent<IDragable>().itemType = FastItemType.Equip;
            }
            index++;
        }
    }
}
