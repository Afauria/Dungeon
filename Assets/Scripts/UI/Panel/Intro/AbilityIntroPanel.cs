using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityIntroPanel : PanelBase
{
    private Text intro;
    public override void Init(string panelName, params object[] args)
    {
        base.Init(panelName, args);
        prefabPath = "AbilityIntroPanel";
        type = PanelType.AbilityIntroPanel;
        initedObj.name = prefabPath;
        intro = objTrans.Find("AbilityIntro_Text").GetComponent<Text>();
        MessageCenter.instance.AddMsgListener(type.ToString(),MsgHandle);
    }
    public override void OnShowing()
    {
        base.OnShowing();
        initedObj.transform.position = Input.mousePosition;
    }
    private void MsgHandle(KeyValuesUpdate kv)
    {
        intro.text = kv.Values.ToString();
    }
}
