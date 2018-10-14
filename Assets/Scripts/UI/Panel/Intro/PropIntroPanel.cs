using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PropIntroPanel : PanelBase
{
    private Text propName;
    private Text propType;
    private Text propIntro;
    public override void Init(string panelName, params object[] args)
    {
        base.Init(panelName, args);
        prefabPath = "PropIntroPanel";
        type = PanelType.PropIntroPanel;
        initedObj.name = prefabPath;
        propName = objTrans.Find("PropName_Text").GetComponent<Text>();
        propType = objTrans.Find("PropType_Text").GetComponent<Text>();
        propIntro = objTrans.Find("PropIntro_Text").GetComponent<Text>();
        MessageCenter.instance.AddMsgListener(type.ToString(), MsgHandle);
    }
    public override void OnShowing()
    {
        base.OnShowing();
        objTrans.position = Input.mousePosition;
    }
    private void MsgHandle(KeyValuesUpdate kv)
    {
        PropEntity propEntity = (PropEntity)kv.Values;
        propName.text = propEntity.propName;
        propType.text = propEntity.typeName;
        propIntro.text = propEntity.description;
    }
}
