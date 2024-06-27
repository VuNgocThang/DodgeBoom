using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LogicPlayer : MonoBehaviour
{
    public float offSetSpeed = 1f;
    public Material matBG;
    private void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            if (transform.position.x < 10f)
            {
                transform.DOMoveX(transform.position.x + 0.2f * offSetSpeed, 0.1f * offSetSpeed);
                //matBG.mainTextureOffset -= new Vector2(0.01f, 0f);
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            if (transform.position.x > -10f)
            {
                transform.DOMoveX(transform.position.x - 0.2f * offSetSpeed, 0.1f * offSetSpeed);
                //matBG.mainTextureOffset += new Vector2(0.01f, 0f);
            }
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            offSetSpeed += 0.2f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Boom"))
        {
            collision.gameObject.SetActive(false);
            //Debug.Log(" - heart" + collision.gameObject.name);
        }
    }
}
