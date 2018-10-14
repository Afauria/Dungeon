using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PickPanel : PanelBase
{
    private GameObject closeBtn;
    private GameObject pickAll;
    private GameObject content;
    private Image[] pickItemPics=new Image[6];
    public override void Init(string panelName, params object[] args)
    {
        base.Init(panelName, args);
        prefabPath = "PickPanel";
        type = PanelType.PickPanel;
        initedObj.name = prefabPath;
        closeBtn = objTrans.Find("ClosePanelBtn_Bg").gameObject;
        pickAll = objTrans.Find("PickAllBtn_Bg").gameObject;
        content = objTrans.Find("PickPanel_Bg1").gameObject;
        for(int i=0;i< content.transform.childCount;i++)
        {
            pickItemPics[i] = content.transform.GetChild(i).Find("PickItem_Frame/PickItem_Pic").GetComponent<Image>();
            pickItemPics[i].gameObject.AddComponent<IHoverable>();
              
        }
        MessageCenter.instance.AddMsgListener(type.ToString(), MsgHandle);
        EventTriggerListener.Get(closeBtn).onClick = (obj) =>
        {
            PanelManager.instance.TogglePanel<PickPanel>();
        };
        EventTriggerListener.Get(pickAll).onClick = (obj) =>
        {
            PanelManager.instance.TogglePanel<PickPanel>();
        };
    }
    private void MsgHandle(KeyValuesUpdate kv)
    {
        List<object> entities=(List<object>)kv.Values;
        for(int i=0;i<entities.Count;i++)
        {
            object o = entities[i];
            pickItemPics[i].gameObject.SetActive(true);
            if(o is PropEntity)
            {
                PropEntity entity = (PropEntity)o;
                pickItemPics[i].sprite = entity.propIcon;
                pickItemPics[i].GetComponent<IHoverable>().content = entity;
                pickItemPics[i].GetComponent<IHoverable>().strClass = typeof(PropIntroPanel).ToString();
            }
            else if(o is EquipEntity)
            {
                EquipEntity entity = (EquipEntity)o;
                pickItemPics[i].sprite = entity.equipIcon;
                pickItemPics[i].GetComponent<IHoverable>().content = entity;
                pickItemPics[i].GetComponent<IHoverable>().strClass =typeof(EquipIntroPanel).ToString();
            }
        }
        for(int i= entities.Count; i<6; i++)
        {
            pickItemPics[i].gameObject.SetActive(false);
        }
    }
}
