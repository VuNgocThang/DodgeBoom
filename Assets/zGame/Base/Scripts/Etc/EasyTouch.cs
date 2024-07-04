using System;
using UnityEngine;
using UnityEngine.EventSystems;

// by nt.Dev93
namespace ntDev
{
    public class EasyTouch : MonoBehaviour, IPointerDownHandler, IPointerExitHandler, IPointerUpHandler, IDragHandler
    {
        [SerializeField] bool IsController;
        [SerializeField] RectTransform rectBG;
        [SerializeField] Transform transControl;

        bool IsTouch = false;

        public Action<Vector3> OnEventTouch;
        public Action OnEventDrag;
        public Action<Vector3> OnEventDragV;
        public Action<Vector3> OnEventDragD;
        public Action<Vector3> OnEventRelease;

        Vector3 defaultPos;
        void Start()
        {
            if (IsController && rectBG != null)
                defaultPos = rectBG.localPosition;
        }

        public Vector3 StartPos;
        Vector3 bgPos;
        public virtual void OnPointerDown(PointerEventData eventData)
        {
            IsTouch = true;
            StartPos = eventData.position;
            currentPos = StartPos;
            lastPos = currentPos;
            OnEventTouch?.Invoke(StartPos);
            Time = 0;

            if (IsController)
            {
                bgPos = StartPos;
                bgPos.x /= transform.lossyScale.x;
                bgPos.y /= transform.lossyScale.y;
                if (rectBG != null) rectBG.localPosition = bgPos;
                if (transControl != null) transControl.localPosition = Vector3.zero;
            }
            // ManagerSound.PlaySound(ManagerSound.Instance.aClick);
        }
        Vector3 currentPos;
        Vector3 lastPos;
        Vector3 V;
        Vector3 D;
        public virtual void OnDrag(PointerEventData eventData)
        {
            if (IsTouch)
            {
                currentPos = eventData.position;
                V = currentPos - StartPos;
                D = currentPos - lastPos;
                OnEventDragV?.Invoke(V);
                OnEventDragD?.Invoke(D);
                OnEventDrag?.Invoke();
                lastPos = currentPos;

                if (IsController && transControl != null)
                {
                    transControl.localPosition = V.normalized * rectBG.sizeDelta.x / 2;
                    transControl.eulerAngles = new Vector3(0, 0, Mathf.Atan2(transControl.localPosition.y, transControl.localPosition.x) * Mathf.Rad2Deg);
                }
            }
        }
        public virtual void OnPointerUp(PointerEventData eventData)
        {
            OnEventRelease?.Invoke(eventData.position);
            Time = -1;
            if (rectBG != null) rectBG.localPosition = defaultPos;
            if (transControl != null) transControl.localPosition = Vector3.zero;
            IsTouch = false;
        }
        public virtual void OnPointerExit(PointerEventData eventData)
        {
            if (IsTouch)
            {
                OnEventRelease?.Invoke(eventData.position);
                Time = -1;
            }
            // if (rectBG != null) rectBG.localPosition = defaultPos;
            // if (transControl != null) transControl.localPosition = Vector3.zero;
            // IsTouch = false;
        }

        public float Time = -1;
        void Update()
        {
            if (Time >= 0) Time += Ez.TimeMod;
        }
    }
}
