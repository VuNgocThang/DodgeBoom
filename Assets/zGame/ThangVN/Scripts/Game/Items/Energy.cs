using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour
{
    [SerializeField] LayerMask layerPlayer;
    //public Rigidbody rb;
    //private void Awake()
    //{
    //    rb = GetComponent<Rigidbody>();
    //}

    public void Init()
    {
        transform.DOLocalJump(new Vector3(transform.localPosition.x + Random.Range(-2f, 2f), -7f, 0f), 2, 2, 0.3f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (LogicGame.Instance.IsInLayerMask(other.gameObject, layerPlayer))
        {
            if (!LogicGame.Instance.isUseEnergy)
            {
                SaveGame.Energy += 50;
            }

            transform.gameObject.SetActive(false);
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (LogicGame.Instance.IsInLayerMask(collision.gameObject, layerPlayer))
    //    {
    //        if (!LogicGame.Instance.isUseEnergy)
    //        {
    //            SaveGame.Energy += 50;
    //        }

    //        transform.gameObject.SetActive(false);
    //    }
    //}
}
