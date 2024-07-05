using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour
{
    [SerializeField] LayerMask layerPlayer;
    [SerializeField] TrailRenderer trail;
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
                SaveGame.Energy += 5;
            }

            if (trail != null) trail.enabled = true;
            Vector3 targetPosition = Camera.main.ScreenToWorldPoint(PopupInGame.Instance.energyUIPosition.position);
            transform.DOMove(targetPosition, 0.3f).OnComplete(() =>
            {
                if (trail != null) trail.enabled = false;
                gameObject.SetActive(false);
            });
            //transform.gameObject.SetActive(false);
        }
    }
    float speed = 3f;

    private void Update()
    {
        if (LogicGame.Instance.isUseMagnet)
        {
            Vector3 posPlayer = LogicGame.Instance.player.transform.position;
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
