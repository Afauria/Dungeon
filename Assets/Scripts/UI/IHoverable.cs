using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
public class IHoverable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public object content;
    public string strClass;
    private readonly List<RaycastResult> hitObjs = new List<RaycastResult>();
    public void OnPointerEnter(PointerEventData eventData)
    {
        KeyValuesUpdate kv = new KeyValuesUpdate("ShowIntro", content);
        //利用反射调用泛型方法,因为字符串无法作为类传进泛型
        try
        {
            System.Reflection.MethodInfo method = typeof(PanelManager).GetMethod("TogglePanel");
            method.MakeGenericMethod(new Type[] { Type.GetType(strClass) }).Invoke(PanelManager.instance, new object[1]);
            MessageCenter.instance.SendMessage(strClass, kv);
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        try
        {
            System.Reflection.MethodInfo method = typeof(PanelManager).GetMethod("TogglePanel");
            method.MakeGenericMethod(new Type[] { Type.GetType(strClass) }).Invoke(PanelManager.instance, new object[1]);
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }

}
