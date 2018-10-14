using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoleInfoPanel : PanelBase
{
    private Text roleName;
    private Text roleLevel;
    private Image healthBar;
    public override void Init(string panelName, params object[] args)
    {
        base.Init(panelName, args);
        prefabPath = "RoleInfoPanel";
        type = PanelType.RoleInfoPanel;
        initedObj.name = prefabPath;
        roleName = objTrans.Find("RoleName_Text").GetComponent<Text>();
        roleLevel = objTrans.Find("RoleLevel_Text").GetComponent<Text>();
        healthBar = objTrans.Find("HealthPanel/Health_Bar").GetComponent<Image>();
        MessageCenter.instance.AddMsgListener(type.ToString(), MsgHandle);
    }
    private void MsgHandle(KeyValuesUpdate kv)
    {
        if (!PanelManager.instance.IsShown<RoleInfoPanel>())
        {
            PanelManager.instance.TogglePanel<RoleInfoPanel>();
        }
        RoleInfo info=(RoleInfo)kv.Values;
        roleName.text = info.roleName;
        roleLevel.text = info.roleLevel.ToString();
        healthBar.fillAmount = info.health;
    }
    
}
public class RoleInfo
{
    public string roleName;
    public int roleLevel;
    public float health;
    public Sprite headPic;

    public RoleInfo(string roleName, int roleLevel, float health, Sprite headPic)
    {
        this.roleName = roleName;
        this.roleLevel = roleLevel;
        this.health = health;
        this.headPic = headPic;
    }
}
