using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePool : MonoBehaviour
{
    public enum TypeParticle
    {
        SingleBoom,
        BigBoom,
        FireBoom,
    }

    public TypeParticle typeParticle;

    private ParticleSystem particle;
    // Start is called before the first frame update

    public void OnParticleSystemStopped()
    {
        if (typeParticle == TypeParticle.SingleBoom)
        {
            LogicGame.Instance.singleBoomPool.Release(particle);
        }
        else if (typeParticle == TypeParticle.BigBoom)
        {
            LogicGame.Instance.bigBoomPool.Release(particle);
        }
        else if (typeParticle == TypeParticle.FireBoom)
        {
            LogicGame.Instance.fireBoomPool.Release(particle);
        }
    }
    void Start()
    {
        particle = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
