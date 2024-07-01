using DG.Tweening;
using System.Collections;
using UnityEngine;

public class LogicGround : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.transform.parent.gameObject.CompareTag("Boom")) return;

        if (other.transform.parent.gameObject.CompareTag("Boom"))
        {
            Debug.Log(other.transform.parent.gameObject.name);
            Vector3 posParticle = new Vector3(other.transform.parent.transform.position.x, transform.position.y, 0);
            Vector3 posItem = other.transform.parent.transform.position;


            SingleBoom singleBoom = other.transform.parent.gameObject.GetComponent<SingleBoom>();
            if (singleBoom != null)
            {
                LogicGame.Instance.singleBoomPool.Spawn(posParticle, true);
                if (singleBoom.energy.gameObject.activeSelf)
                {
                    Debug.Log("Spawn energy");
                    Energy energy = CustomPoolController.Instance.GetEnergy();
                    energy.transform.position = posItem;
                }

                if (singleBoom.coin.gameObject.activeSelf)
                {
                    Debug.Log("Spawn coin");
                    Coin coin = CustomPoolController.Instance.GetCoin();
                    coin.transform.position = posItem;
                }
            }
            else
            {
                LogicGame.Instance.bigBoomPool.Spawn(posParticle, true);
                CameraShake.Instance.OnShake(0.2f, 1);
            }
            LogicGame.Instance.listBoom.Remove(other.transform.parent.gameObject);
            other.transform.parent.gameObject.SetActive(false);
        }
    }
}
