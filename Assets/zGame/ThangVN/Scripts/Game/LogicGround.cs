using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class LogicGround : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Boom"))
        {
            if (collision.gameObject.GetComponent<BigBoom>() != null)
            {
                Debug.Log("BIG BOOM");
            }
            if (collision.gameObject.GetComponent<DoubleBoom>() != null)
            {
                Debug.Log("DoubleBoom BOOM");
            }
            StartCoroutine(PlayAnimBoom(collision.gameObject));
        }
    }

    IEnumerator PlayAnimBoom(GameObject obj)
    {
        yield return new WaitForSeconds(0.15f);
        obj.SetActive(false);
    }
}
