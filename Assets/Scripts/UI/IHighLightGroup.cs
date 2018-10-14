using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class IHighLightGroup : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{
    private Image bg;
    private Color alphaColor = new Color(1, 0.8f, 0.4f, 0f);
    private Color unAlphaColor = new Color(1, 0.8f, 0.4f, 0.2f);
    void Start()
    {
        bg = GetComponent<Image>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        bg.color = unAlphaColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        bg.color = alphaColor;
    }
}
