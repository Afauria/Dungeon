using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EquipIntroPanel : PanelBase {
    private Text equipName;
    private Text equipType;
    private Text equipIntro;
    private Text equipAbility;
    private Text equipBaseAbility1;
    private Text equipBaseAbility2;
    private Text equipBaseAbility3;
    private Text valueText;
    public override void Init(string panelName, params object[] args)
    {
        base.Init(panelName, args);
        prefabPath = "EquipIntroPanel";
        type = PanelType.EquipIntroPanel;
        initedObj.name = prefabPath;
        
        equipName = objTrans.Find("EquipName_Text").GetComponent<Text>();
        equipType = objTrans.Find("EquipType_Text").GetComponent<Text>();
        equipIntro = objTrans.Find("EquipIntro_Text").GetComponent<Text>();
        equipAbility = objTrans.Find("EquipAbility_Text").GetComponent<Text>();
        equipBaseAbility1 = objTrans.Find("EquipBaseAbility_Text1").GetComponent<Text>();
        equipBaseAbility2 = objTrans.Find("EquipBaseAbility_Text2").GetComponent<Text>();
        equipBaseAbility3 = objTrans.Find("EquipBaseAbility_Text3").GetComponent<Text>();
        valueText = objTrans.Find("Value_Text").GetComponent<Text>();
        MessageCenter.instance.AddMsgListener(type.ToString(), MsgHandle);
    }
    public override void OnShowing()
    {
        base.OnShowing();
        objTrans.position = Input.mousePosition;
    }
    private void MsgHandle(KeyValuesUpdate kv)
    {
        EquipEntity equipEntity = (EquipEntity)kv.Values;
        equipName.text = equipEntity.equipName;
        equipType.text = equipEntity.typeName;
        equipIntro.text = equipEntity.description;
        equipAbility.text = equipEntity.ability;
        equipBaseAbility1.text = equipEntity.baseAbility1;
        equipBaseAbility2.text = equipEntity.baseAbility2;
        equipBaseAbility3.text = equipEntity.baseAbility3;
        valueText.text = equipEntity.lowerValue.ToString();
    }
}
