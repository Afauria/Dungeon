using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
public class SkillPanel : PanelBase
{

    Button a;
    private readonly GameObject[] sortItems = new GameObject[5];
    private int selectTabIndex = 0;
    private Transform sortPanel;
    private Transform col1;
    private Transform col2;
    private GameObject closeBtn;
    private Image[] col1Images = new Image[4];
    private Image[] col2Images = new Image[4];
    private Image[] col1ImagesMask = new Image[4];
    private Image[] col2ImagesMask = new Image[4];
    public override void Init(string panelName, params object[] args)
    { 
        base.Init(panelName, args);
        prefabPath = "SkillPanel";
        type = PanelType.SkillPanel;
        initedObj.name = prefabPath;
        initedObj.AddComponent<IPanelDragable>();
        closeBtn = objTrans.Find("SkillPanelTop_Bg/CloseBtn_Frame").gameObject;
        //关闭按钮点击事件注册，通知菜单项关闭菜单
        EventTriggerListener.Get(closeBtn).onClick = (obj) =>
        {
            KeyValuesUpdate kv = new KeyValuesUpdate("MenuItemClick", PanelType.SkillPanel);
            MessageCenter.instance.SendMessage(PanelType.MenuPanel.ToString(), kv);
        };
        col1 = objTrans.Find("SkillPanel_Bg/SkillCol1");
        col2 = objTrans.Find("SkillPanel_Bg/SkillCol2");

        sortPanel = objTrans.Find("SkillSortPanel");
        for (int i = 0; i < sortPanel.childCount; i++)
        {
            sortItems[i] = sortPanel.GetChild(i).gameObject;
            EventTriggerListener.Get(sortItems[i]).onClick = (obj) =>
            {
                ChangeSortTab(obj);
            };
        }
        for (int i = 0; i < 4; i++)
        {
            col1Images[i] = col1.GetChild(i).Find("SkillItem_Frame2/SkillItem_Pic").GetComponent<Image>();
            col1ImagesMask[i] = col1Images[i].transform.Find("SkillItem_Pic_Mask").GetComponent<Image>();
            col1ImagesMask[i].gameObject.AddComponent<IHoverable>();
            col1ImagesMask[i].gameObject.GetComponent<IHoverable>().strClass = typeof(SkillIntroPanel).ToString();
        }
        for (int i = 0; i < 4; i++)
        {
            col2Images[i] = col2.GetChild(i).Find("SkillItem_Frame2/SkillItem_Pic").GetComponent<Image>();
            col2ImagesMask[i] = col2Images[i].transform.Find("SkillItem_Pic_Mask").GetComponent<Image>();
            col2ImagesMask[i].gameObject.AddComponent<IHoverable>();
            col2ImagesMask[i].gameObject.GetComponent<IHoverable>().strClass = typeof(SkillIntroPanel).ToString();
        }
        SetSkillData(0);
    }
    private void ChangeSortTab(GameObject obj)
    {
        sortPanel.GetChild(selectTabIndex).Find("SkillSortItem_Select").gameObject.SetActive(false);
        obj.transform.Find("SkillSortItem_Select").gameObject.SetActive(true);
        selectTabIndex = obj.transform.GetSiblingIndex();
        SetSkillData(selectTabIndex);
    }
    private void SetSkillData(int selectTab)
    {
        SkillEntity[] skillEntities = AssetsDB.instance.skillsData.skillType1;
        switch (selectTab)
        {
            case 0:
                skillEntities = AssetsDB.instance.skillsData.skillType1;
                break;
            case 1:
                skillEntities = AssetsDB.instance.skillsData.skillType2;
                break;
            case 2:
                skillEntities = AssetsDB.instance.skillsData.skillType3;
                break;
            case 3:
                skillEntities = AssetsDB.instance.skillsData.skillType4;
                break;
            case 4:
                skillEntities = AssetsDB.instance.skillsData.skillType5;
                break;
        }
        for (int i = 0; i < 4; i++)
        {
            col1Images[i].sprite = skillEntities[i].skillIcon;
            col1ImagesMask[i].sprite = skillEntities[i].skillIcon;
            col1ImagesMask[i].gameObject.GetComponent<IHoverable>().content = skillEntities[i];
            col1Images[i].GetComponent<IDragable>().itemName = skillEntities[i].skillName;
            col1Images[i].GetComponent<IDragable>().itemType = FastItemType.Skill;
        }
        for (int i = 0; i < 4; i++)
        {
            col2Images[i].sprite = skillEntities[i + 4].skillIcon;
            col2ImagesMask[i].sprite = skillEntities[i + 4].skillIcon;
            col2ImagesMask[i].gameObject.GetComponent<IHoverable>().content = skillEntities[i + 4];

            col2Images[i].GetComponent<IDragable>().itemName = skillEntities[i+4].skillName;
            col2Images[i].GetComponent<IDragable>().itemType = FastItemType.Skill;
        }

    }
}
