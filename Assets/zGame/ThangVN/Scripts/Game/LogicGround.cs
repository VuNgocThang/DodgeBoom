using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.EditorTools;
using UnityEngine;

public class LogicGround : MonoBehaviour
{
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.transform.parent.gameObject.CompareTag("Boom"))
    //    {
    //        Debug.Log(collision.transform.parent.gameObject.name);
    //        StartCoroutine(PlayAnimBoom(collision.transform.parent.gameObject));


    //        //if (collision.transform.parent.gameObject.GetComponent<BigBoom>() != null)
    //        //{
    //        //    Debug.Log("BIG BOOM");
    //        //}
    //        //if (collision.transform.parent.gameObject.GetComponent<DoubleBoom>() != null)
    //        //{
    //        //    Debug.Log("DoubleBoom BOOM");
    //        //}
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.gameObject.CompareTag("Boom"))
        {
            Debug.Log(other.transform.parent.gameObject.name);
            StartCoroutine(PlayAnimBoom(other.transform.parent.gameObject));
        }
    }

    IEnumerator PlayAnimBoom(GameObject obj)
    {
        yield return new WaitForSeconds(0.15f);
        obj.SetActive(false);
    }
}
