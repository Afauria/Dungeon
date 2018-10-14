
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleHandlePanel : PanelBase
{
    private GameObject attackObj;
    private GameObject defenseObj;
    private GameObject escapeObj;
    private GameObject passObj;
    public override void Init(string panelName, params object[] args)
    {
        base.Init(panelName, args);
        prefabPath = "BattleHandlePanel";
        type = PanelType.BattleHandlePanel;
        attackObj = objTrans.Find("Attack_Text").gameObject;
        defenseObj = objTrans.Find("Defense_Text").gameObject;
        escapeObj = objTrans.Find("Escape_Text").gameObject;
        passObj = objTrans.Find("Pass_Text").gameObject;
        EventTriggerListener.Get(attackObj).onClick = (obj) =>
        {

        };
        EventTriggerListener.Get(defenseObj).onClick = (obj) =>
        {

        };
        EventTriggerListener.Get(escapeObj).onClick = (obj) =>
        {

        };
        EventTriggerListener.Get(passObj).onClick = (obj) =>
        {

        };
    }
}
