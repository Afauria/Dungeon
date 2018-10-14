using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LoadingPanel : PanelBase
{
    //private Image loadingBar;
    private Text loadingText;
    private Animator anim;
    public override void Init(string panelName, params object[] args)
    {
        base.Init(panelName, args);
        prefabPath = "LoadingPanel";
        type = PanelType.LoadingPanel;
        initedObj.name = prefabPath;
        //loadingBar = initedObj.transform.Find("Loading_Bg/Loading_Mask").gameObject.GetComponent<Image>();
        loadingText = objTrans.Find("Loading_Bg/Loading_Text").gameObject.GetComponent<Text>();
        anim = loadingText.GetComponent<Animator>();
        MessageCenter.instance.AddMsgListener(type.ToString(), MsgHandle);
    }
    void MsgHandle(KeyValuesUpdate kv)
    {
        if (kv.Key.Equals("Loading"))
        { 
            if (kv.Values.ToString().Equals("StartLoading"))
            {
                anim.SetBool("IsLoading",true);
            }
            else if (kv.Values.ToString().Equals("EndLoading"))
            {
                anim.SetBool("IsLoading", false);
            }
        }
        //if (kv.Key.Equals("UpdateProcess"))
        //{
        //    loadingBar.fillAmount = float.Parse(kv.Values.ToString());
        //}
    }

}
