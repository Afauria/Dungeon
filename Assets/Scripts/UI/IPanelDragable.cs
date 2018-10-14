using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class IPanelDragable : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private Vector2 offset;
    public void OnBeginDrag(PointerEventData eventData)
    {
        offset = (Vector2)gameObject.transform.position - eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        gameObject.transform.position = eventData.position + offset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

    }
}
