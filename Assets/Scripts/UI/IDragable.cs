using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class IDragable : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private readonly List<RaycastResult> hitObjs = new List<RaycastResult>();
    private Transform tempParent;
    private Vector2 offset;

    private Image originImage;
    private Image originImageMask;

    private GameObject dragObj;
    private Image dragImage;
    private Transform dragObjTrans;

    public string itemName;
    public FastItemType itemType;

    private Color alphaColor = new Color(1, 1, 1, 0);
    private Color unAlphaColor = new Color(1, 1, 1, 1);
    void Start()
    {
        originImage = GetComponent<Image>();
        originImageMask = transform.GetChild(0).GetComponent<Image>();
        tempParent = PanelManager.instance.tipsParent;
        if (gameObject.tag.Equals(MyTag.FAST_ICON_TAG))
        {
            dragObj = tempParent.Find("FastBarTempParent/FastItem/FastItem_Pic").gameObject;
        }
        else if (gameObject.tag.Equals(MyTag.PROP_ICON_TAG))
        {
            dragObj = tempParent.Find("PropsPanelTempParent/PropsPanel_Bg/Props_Scroll View/Viewport/Content/PropItemPlace/Prop_Frame/PropItem_Pic").gameObject;
        }
        else if (gameObject.tag.Equals(MyTag.SKILL_ICON_TAG))
        {
            dragObj = tempParent.Find("SkillPanelTempParent/SkillPanel_Bg/SkillCol1/SkillItem_Frame/SkillItem_Frame2/SkillItem_Pic").gameObject;
        }
        dragImage = dragObj.GetComponent<Image>();
        dragObjTrans = dragObj.transform;

        dragObj.SetActive(false);
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (gameObject.tag.Equals(MyTag.FAST_ICON_TAG))
        {
            originImage.color = alphaColor;
            originImageMask.color = alphaColor;
        }
        dragImage.sprite = GetComponent<Image>().sprite;
        dragObj.SetActive(true);
        dragObj.transform.position = transform.position;

        offset = (Vector2)dragObjTrans.position - eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        dragObjTrans.position = eventData.position + offset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        dragObj.SetActive(false);
        originImage.color = unAlphaColor;
        originImageMask.color = unAlphaColor;
        GameObject targetPlace = GetObjUnderMouse(eventData);
        if (gameObject.tag.Equals(MyTag.FAST_ICON_TAG))
        {
            originImage.gameObject.SetActive(false);
        }
        if (targetPlace == null)
        {
            if (gameObject.tag.Equals(MyTag.FAST_ICON_TAG))
            {
                originImage.gameObject.SetActive(true);
            }
            return;
        }

        if (targetPlace.tag.Equals(MyTag.FAST_ITEM_PLACE_TAG))
        {
            Transform targetObj = targetPlace.transform.Find("FastItem_Pic");
            targetObj.gameObject.SetActive(true);
            targetObj.GetComponent<Image>().sprite = dragImage.sprite;
            targetObj.Find("FastItem_Pic_Mask").GetComponent<Image>().sprite = dragImage.sprite;

            //更改快捷键数据
            int targetIndex = targetPlace.transform.GetSiblingIndex();
            int originIndex = transform.parent.GetSiblingIndex();
            if (gameObject.tag.Equals(MyTag.FAST_ICON_TAG))
            {
                PlayerData.instance.playerEntity.fastBar[targetIndex] = PlayerData.instance.playerEntity.fastBar[originIndex];
                PlayerData.instance.playerEntity.fastBar[targetIndex].index = targetIndex;
                PlayerData.instance.playerEntity.fastBar[originIndex] = new FastItemEntity(originIndex);
                
            }
            else if (gameObject.tag.Equals(MyTag.PROP_ICON_TAG) || gameObject.tag.Equals(MyTag.SKILL_ICON_TAG))
            {
                FastItemEntity temp = new FastItemEntity
                {
                    index = targetIndex,
                    fastItemName = itemName,
                    fastItemType = itemType,
                    icon = originImage.sprite
                };
                PlayerData.instance.playerEntity.fastBar[targetIndex]=temp;
            }
        }
        foreach (FastItemEntity item in PlayerData.instance.playerEntity.fastBar)
        {
            Debug.Log(item.index+":"+item.fastItemName);
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
            if (result.gameObject.tag.Equals(MyTag.FAST_ITEM_PLACE_TAG))
                return result.gameObject;
        }
        return null;

    }
}
