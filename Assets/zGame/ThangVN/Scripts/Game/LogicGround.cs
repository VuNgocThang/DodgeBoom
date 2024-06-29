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
            Vector3 newPos = other.transform.parent.transform.position;
            LogicGame.Instance.bigBoomPool.Spawn(newPos, true);
            SingleBoom boom = other.transform.parent.gameObject.GetComponent<SingleBoom>();
            if (boom != null)
            {
                if (boom.energy.gameObject.activeSelf)
                {
                    Debug.Log("Spawn energy");
                    Energy energy = CustomPoolController.Instance.GetEnergy();
                    energy.transform.position = newPos;
                }

                if (boom.coin.gameObject.activeSelf)
                {
                    Debug.Log("Spawn coin");
                    Coin coin = CustomPoolController.Instance.GetCoin();
                    coin.transform.position = newPos;
                }
            }
            StartCoroutine(PlayAnimBoom(other.transform.parent.gameObject));
        }
    }

    IEnumerator PlayAnimBoom(GameObject obj)
    {
        yield return new WaitForSeconds(0.15f);
        obj.SetActive(false);
    }
}
