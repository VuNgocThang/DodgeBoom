using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LogicPlayer : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            if (transform.position.x < 7f)
            {
                transform.DOMoveX(transform.position.x + 0.2f, 0.1f);
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            if (transform.position.x > -7f)
            {
                transform.DOMoveX(transform.position.x - 0.2f, 0.1f);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Boom"))
        {
            Debug.Log(" - heart" + collision.gameObject.name);
        }
    }
}
