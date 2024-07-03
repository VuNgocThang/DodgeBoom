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
    float speed = 3f;

    private void Update()
    {
        if (LogicGame.Instance.isUseMagnet)
        {
            Vector3 posPlayer = LogicGame.Instance.player.transform.position;
            //float distance = Vector3.Distance(transform.position, posPlayer);
            Vector3 direction = (posPlayer - transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime, Space.World);
        }

        if (!LogicGame.Instance.isUseMagnet) StartCoroutine(ReturnFalse());
        else StopCoroutine(ReturnFalse());
    }

    IEnumerator ReturnFalse()
    {
        yield return new WaitForSeconds(5f);
        if (!LogicGame.Instance.isUseMagnet)
            gameObject.SetActive(false);
    }
}
