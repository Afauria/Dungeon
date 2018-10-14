using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class UIDragger : MonoBehaviour
{

    public const string FAST_ICON_TAG = "FastIcon";
    public const string INVENTORY_ICON_TAG = "InventoryIcon";
    public const string SKILL_ICON_TAG = "SkillIcon";
    public const string DRAGGABLE_PANEL_TAG = "DraggablePanel";

    private Transform originalParent;
    private Transform tempParent;

    //private FastInventoryPanel FastInventoryPanel;
    //private InventoryPanel inventoryPanel;

    private bool isDragging = false;

    private GameObject dragObj;
    private Vector3 offset;

    private Vector2 originalPos;
    private Transform dragObjTrans;
    private Image dragObjImg;

    private List<RaycastResult> hitObjs = new List<RaycastResult>();

    private void Start()
    {
        tempParent = GameObject.Find("Canvas").transform.Find("Tips");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragObj = GetObjUnderMouse();
            if (dragObj == null)
                return;

            dragObjTrans = dragObj.transform;
            offset = dragObjTrans.transform.position - Input.mousePosition;

            if (dragObj.tag.Equals(FAST_ICON_TAG))
            {
                DragIconDown();
            }
            else if (dragObj.tag.Equals(DRAGGABLE_PANEL_TAG))
            {
                DragPanelDown();
            }
        }

        if (isDragging)
        {
            dragObjTrans.position = Input.mousePosition + offset;
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (dragObj == null)
                return;

            if (dragObj.tag.Equals(FAST_ICON_TAG))
            {
                DragIconUp();
            }
            else if (dragObj.tag.Equals(DRAGGABLE_PANEL_TAG))
            {
                DragPanelUp();
            }
        }
    }

    private GameObject GetObjUnderMouse()
    {
        var pointer = new PointerEventData(EventSystem.current);

        pointer.position = Input.mousePosition;
        EventSystem.current.RaycastAll(pointer, hitObjs);

        if (hitObjs.Count <= 0)
        {
            return null;
        }

        return hitObjs[0].gameObject;
    }

    private void DragPanelDown()
    {
        isDragging = true;
    }

    private void DragPanelUp()
    {
        dragObjTrans = null;

        isDragging = false;
    }

    private void DragIconDown()
    {
        if (dragObjTrans.GetComponent<Image>().sprite != null)
        {
            originalPos = dragObjTrans.position;
            originalParent = dragObjTrans.parent;

            dragObjTrans.SetParent(tempParent);
            dragObjImg = dragObjTrans.GetComponent<Image>();
            dragObjImg.raycastTarget = false;

            isDragging = true;
        }
    }

    private void DragIconUp()
    {
        if (dragObjImg != null)
        {
            Transform replaceObj = GetObjUnderMouse().transform;

            if (replaceObj != null)
            {
                dragObjTrans.position = replaceObj.position;
                dragObjTrans.SetParent(replaceObj.parent);
                replaceObj.position = originalPos;
                replaceObj.SetParent(originalParent);
            }
            else
            {
                dragObjTrans.SetParent(originalParent);
                dragObjTrans.position = originalPos;
            }

            dragObjImg.raycastTarget = true;
            dragObjImg = null;
            dragObjTrans = null;
        }

        isDragging = false;
    }
}
