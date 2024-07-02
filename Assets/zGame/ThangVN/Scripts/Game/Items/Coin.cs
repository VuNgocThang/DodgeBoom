using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] LayerMask layerPlayer;
    //public Rigidbody rb;
    //private void Awake()
    //{
    //    rb = GetComponent<Rigidbody>();
    //}
    public void Init()
    {
        //rb.velocity = Vector2.zero;
        //rb.AddForce(new Vector2(Random.Range(-1f, 1f), 5f), ForceMode.Impulse);

        transform.DOLocalJump(new Vector3(transform.localPosition.x + Random.Range(-2f, 2f), -7f, 0f), 2, 2, 0.3f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (LogicGame.Instance.IsInLayerMask(other.gameObject, layerPlayer))
        {
            SaveGame.Coin += 1;
            transform.gameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        //if (LogicGame.Instance.isUseMagnet)
        //{
        //    Vector3 posPlayer = LogicGame.Instance.player.transform.position;
        //    float distance = Vector3.Distance(transform.position, posPlayer);
        //    transform.position = Vector3.Lerp(transform.position, new Vector3(posPlayer.x, transform.position.y, 0), 10f);
        //}
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
