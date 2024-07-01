using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using ntDev;

public class LogicPlayer : MonoBehaviour
{

    public float offSetSpeed = 1f;
    public Material matBG;
    public GameObject cover;
    public Transform spine;
    public GameObject vfx;
    [SerializeField] bool isMoving;
    [SerializeField] Animator anim;
    private void Awake()
    {
        ManagerEvent.RegEvent(EventCMD.EVENT_USE_EFFECT, UseEffect);
    }

    private void FixedUpdate()
    {
        isMoving = false;

        if (Input.GetKey(KeyCode.D))
        {
            spine.localScale = new Vector3(0.4f, 0.4f, 0.4f);

            isMoving = true;
            vfx.gameObject.SetActive(true);
            anim.Play("run_slow");
            if (transform.position.x < 10f)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x + 0.2f * offSetSpeed, transform.position.y, 0), 0.1f * offSetSpeed);
                //transform.DOMoveX(transform.position.x + 0.2f * offSetSpeed, 0.1f * offSetSpeed);
                //matBG.mainTextureOffset -= new Vector2(0.005f, 0f);
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            spine.localScale = new Vector3(-0.4f, 0.4f, 0.4f);

            isMoving = true;
            vfx.gameObject.SetActive(true);
            anim.Play("run_slow");
            if (transform.position.x > -10f)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x - 0.2f * offSetSpeed, transform.position.y, 0), 0.1f * offSetSpeed);
                //transform.DOMoveX(transform.position.x - 0.2f * offSetSpeed, 0.1f * offSetSpeed);
                //matBG.mainTextureOffset += new Vector2(0.005f, 0f);
            }
        }

        if (!isMoving)
        {
            vfx.gameObject.SetActive(false);
            anim.Play("idle");
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            offSetSpeed += 0.2f;
        }
    }

    //private void FixedUpdate()
    //{
    //    if (Input.GetKey(KeyCode.D))
    //    {
    //        spine.localScale = new Vector3(0.4f, 0.4f, 0.4f);

    //        isMoving = true;
    //        vfx.gameObject.SetActive(true);
    //        anim.Play("run_slow");
    //        if (transform.position.x < 10f)
    //        {
    //            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x + 0.2f * offSetSpeed, transform.position.y, 0), 0.1f * offSetSpeed);
    //            //transform.DOMoveX(transform.position.x + 0.2f * offSetSpeed, 0.1f * offSetSpeed);
    //            //matBG.mainTextureOffset -= new Vector2(0.005f, 0f);
    //        }
    //    }

    //    if (Input.GetKey(KeyCode.A))
    //    {
    //        spine.localScale = new Vector3(-0.4f, 0.4f, 0.4f);

    //        isMoving = true;
    //        vfx.gameObject.SetActive(true);
    //        anim.Play("run_slow");
    //        if (transform.position.x > -10f)
    //        {
    //            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x - 0.2f * offSetSpeed, transform.position.y, 0), 0.1f * offSetSpeed);
    //            //transform.DOMoveX(transform.position.x - 0.2f * offSetSpeed, 0.1f * offSetSpeed);
    //            //matBG.mainTextureOffset += new Vector2(0.005f, 0f);
    //        }
    //    }
    //}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Boom"))
        {
            collision.gameObject.SetActive(false);
            //Debug.Log(" - heart" + collision.gameObject.name);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    void UseEffect(object e)
    {
        cover.gameObject.SetActive(true);
        SaveGame.Energy = 0;
        StartCoroutine(StopUseEffect());
    }

    IEnumerator StopUseEffect()
    {
        yield return new WaitForSeconds(3f);
        cover.gameObject.SetActive(false);
    }
}
