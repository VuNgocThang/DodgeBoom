using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using ntDev;
using Spine.Unity;

public class LogicPlayer : MonoBehaviour
{

    public float offSetSpeed = 1f;
    public Material matBG;
    public GameObject cover;
    public GameObject shield;
    public GameObject magnet;
    public Transform spine;
    public GameObject vfx;
    [SerializeField] bool isMoving;
    [SerializeField] bool isDie;
    [SerializeField] Animator anim;
    [SerializeField] LayerMask layerBoom;
    [SerializeField] CharacterData characterData;
    [SerializeField] SkeletonMecanim spineMecanim;
    private void Awake()
    {
        ManagerEvent.RegEvent(EventCMD.EVENT_USE_EFFECT, UseEffect);
    }

    private void Start()
    {
        LoadSkinSpine();

    }

    void LoadSkinSpine()
    {
        spineMecanim.Skeleton.SetSkin(characterData.nameToPlay);
        spineMecanim.Skeleton.SetSlotsToSetupPose();
        spineMecanim.LateUpdate();
    }

    private void FixedUpdate()
    {
        if (isDie)
        {
            isMoving = false;
            vfx.gameObject.SetActive(false);
            anim.Play("die");
            return;
        }

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
    private void OnTriggerEnter(Collider other)
    {
        if (LogicGame.Instance.IsInLayerMask(other.gameObject, layerBoom))
        {
            Debug.Log("Play Anim Die");
            Vector3 posParticle = new Vector3(other.transform.position.x, transform.position.y, 0);
            LogicGame.Instance.singleBoomPool.Spawn(posParticle, true);
            //isDie = true;
            //LogicGame.Instance.isPauseGame = true;
            //StartCoroutine(RaiseEventLose());
        }
    }

    void UseEffect(object e)
    {
        cover.gameObject.SetActive(true);
        SaveGame.Energy = 0;
        StartCoroutine(StopUseEffect());
    }

    IEnumerator StopUseEffect()
    {
        yield return new WaitForSeconds(10f);
        cover.gameObject.SetActive(false);
    }

    IEnumerator RaiseEventLose()
    {
        yield return new WaitForSeconds(1f);
        ManagerEvent.RaiseEvent(EventCMD.EVENT_LOSE);
    }

}
