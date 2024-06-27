using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicGround : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Boom"))
        {
            Debug.Log(collision.gameObject.name + " collision");
            StartCoroutine(PlayAnimBoom(collision.gameObject));
        }
    }

    IEnumerator PlayAnimBoom(GameObject obj)
    {
        yield return new WaitForSeconds(0.15f);
        obj.SetActive(false);
    }
}
