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
            Vector3 newPos = new Vector3(other.transform.parent.transform.position.x, transform.position.y, 0);
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
            other.transform.parent.gameObject.SetActive(false);
        }
    }
}
