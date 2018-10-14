using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class IGroupDragable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private readonly List<RaycastResult> hitObjs = new List<RaycastResult>();
    public bool isSelected;
    private Vector3 originPos;
    private Vector2 offset;
    private Transform dragGroupParent;
    private Transform originParent;
    public RoleInfo roleInfo;
    void Awake()
    {
        dragGroupParent = GameObject.Find("DragGroupParent").transform;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originPos = transform.position;
        originParent = transform.parent;
        transform.SetParent(dragGroupParent);
        offset = (Vector2)transform.position - eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position + offset;
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(originParent);
        GameObject targetPlace = GetObjUnderMouse(eventData);
        if (targetPlace == null)
        {
            KeyValuesUpdate kv = new KeyValuesUpdate("CreateGroup",
                new ChangeGroupInfo(transform.parent.GetSiblingIndex(), 0,0, roleInfo));
            MessageCenter.instance.SendMessage(typeof(HeadPanel).ToString(), kv);
        }
        else
        {
            if (targetPlace.transform.GetSiblingIndex() == transform.parent.GetSiblingIndex())
            {
                transform.position = originPos;
            }
            KeyValuesUpdate kv = new KeyValuesUpdate("ChangeGroup",
                new ChangeGroupInfo(transform.parent.GetSiblingIndex(), targetPlace.transform.GetSiblingIndex(), transform.GetSiblingIndex(), roleInfo));
            MessageCenter.instance.SendMessage(typeof(HeadPanel).ToString(), kv);
        }
    }
    private GameObject GetObjUnderMouse(PointerEventData eventData)
    {
        var pointer = new PointerEventData(EventSystem.current)
        {
            position = eventData.position
        };
        EventSystem.current.RaycastAll(pointer, hitObjs);

        foreach (RaycastResult result in hitObjs)
        {
            if (result.gameObject.tag.Equals(MyTag.GROUP_HEAD_PLACE))
                return result.gameObject;
        }
        return null;

    }
}
public class ChangeGroupInfo
{
    public int originGroup;
    public int targetGroup;
    public int originIndex;
    public RoleInfo roleInfo;

    public ChangeGroupInfo(int originGroup, int targetGroup, int originIndex, RoleInfo roleInfo)
    {
        this.originGroup = originGroup;
        this.targetGroup = targetGroup;
        this.originIndex = originIndex;
        this.roleInfo = roleInfo;
    }
}
