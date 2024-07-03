using System;
using UnityEngine;
using TMPro;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;

// by nt.Dev93
namespace ntDev
{
    public class Popup : MonoBehaviour
    {
        [SerializeField] public EasyButton[] btnClose;
        [SerializeField] Animator animator;


        [SerializeField] bool init = false;
        public virtual void Init()
        {
            if (!init)
            {
                init = true;
                foreach (EasyButton btn in btnClose)
                    btn.OnClick(Hide);
            }

            gameObject.SetActive(true);
            if (animator != null) animator.Play("Show");
        }

        public virtual void Hide()
        {
            if (animator != null) animator.Play("Hide");
            StartCoroutine(ClosePopup());
        }

        IEnumerator ClosePopup()
        {
            yield return new WaitForSeconds(1f);
            gameObject.SetActive(false);
            ManagerEvent.RaiseEvent(EventCMD.EVENT_POPUP_CLOSE, this);
        }

    }
}