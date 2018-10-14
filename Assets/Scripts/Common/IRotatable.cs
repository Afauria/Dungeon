using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class IRotatable : MonoBehaviour,IDragHandler
{
    public GameObject model;
    private Vector3 preMousePos;
    private Vector3 preRot;

    public void OnDrag(PointerEventData eventData)
    {
        model.transform.Rotate(Vector3.down * eventData.delta.x*0.5f);
    }

}
