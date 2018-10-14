using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitGame : MonoBehaviour {

	void Start () {
        InitLoginScenePanel();
    }
	public static void InitLoginScenePanel()
    {
        PanelManager.instance.OpenPanel<ErrorPanel>();
        PanelManager.instance.TogglePanel<ErrorPanel>();
        PanelManager.instance.OpenPanel<TipsPanel>();
        PanelManager.instance.TogglePanel<TipsPanel>();
        PanelManager.instance.OpenPanel<LoadingPanel>();
        PanelManager.instance.TogglePanel<LoadingPanel>();
        PanelManager.instance.OpenPanel<LoginPanel>();
    }
}
