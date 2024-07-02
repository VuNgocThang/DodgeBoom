using DG.Tweening;
using System.Collections;
using UnityEngine;

public class LogicGround : MonoBehaviour
{
    [SerializeField] LayerMask layerBoom;

    private void OnTriggerEnter(Collider other)
    {
        if (LogicGame.Instance.IsInLayerMask(other.transform.parent.gameObject, layerBoom))
        {
            Vector3 posParticle = new Vector3(other.transform.parent.transform.position.x, transform.position.y, 0);
            Vector3 posItem = other.transform.parent.transform.position;

            IBoom boom = other.transform.parent.gameObject.GetComponent<IBoom>();
            if (boom != null)
            {
                boom.SpawnParticle(posParticle, posItem);
            }

            LogicGame.Instance.listBoom.Remove(other.transform.parent.gameObject);
            foreach (LogicShadow s in LogicGame.Instance.listShadow)
            {
                float distanceThreshold = 0.1f;

                if (Vector3.Distance(s.transform.position, new Vector3(other.transform.parent.transform.position.x, -7, 0)) < distanceThreshold)
                {
                    s.gameObject.SetActive(false);
                    break;
                }
            }
            other.transform.parent.gameObject.SetActive(false);
        }
    }
}
