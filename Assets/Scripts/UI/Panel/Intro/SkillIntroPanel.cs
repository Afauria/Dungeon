using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillIntroPanel : PanelBase {
    
    private Text skillName;
    private Text intro;
    private Text pointCount;
    private Text coldRound;
    private Text area;
    private Text damage;
    private Text otherAffect;
    public override void Init(string panelName, params object[] args)
    {
        base.Init(panelName, args);
        prefabPath = "SkillIntroPanel";
        type = PanelType.SkillIntroPanel;
        initedObj.name = prefabPath;
        skillName= objTrans.Find("SkillName_Text").GetComponent<Text>();
        intro = objTrans.Find("SkillIntro_Text").GetComponent<Text>();
        pointCount = objTrans.Find("PointCount/PointCount_Text").GetComponent<Text>();
        coldRound= objTrans.Find("ColdRound/ColdRound_Text").GetComponent<Text>();
        area = objTrans.Find("Area/Area_Text").GetComponent<Text>(); 
        damage= objTrans.Find("Damage/Damage_Text").GetComponent<Text>();
        otherAffect= objTrans.Find("OtherAffect/OtherAffect_Text").GetComponent<Text>();
        MessageCenter.instance.AddMsgListener(type.ToString(), MsgHandle);
    }
    public override void OnShowing()
    {
        base.OnShowing();
        initedObj.transform.position = Input.mousePosition;
    }
    private void MsgHandle(KeyValuesUpdate kv)
    {
        SkillEntity skillEntity = (SkillEntity)kv.Values;
        skillName.text = skillEntity.skillName;
        intro.text = skillEntity.description;
        coldRound.text = skillEntity.coldTime.ToString();
        damage.text = skillEntity.damageRate.ToString();
        
    }

}
