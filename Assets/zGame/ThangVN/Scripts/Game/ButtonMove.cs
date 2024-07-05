using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonMove : MonoBehaviour, IPointerDownHandler, IPointerExitHandler, IPointerUpHandler, IPointerClickHandler
{
    public bool isPressing;

    public virtual void OnPointerClick(PointerEventData eventData)
    {

    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        isPressing = true;
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        isPressing = false;
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
    }


}
