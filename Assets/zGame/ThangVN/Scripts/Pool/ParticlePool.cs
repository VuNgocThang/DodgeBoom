using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePool : MonoBehaviour
{
    public enum TypeParticle
    {
        Click,
        Eat,
        Move,
        Unlock
    }

    public TypeParticle typeParticle;

    private ParticleSystem particle;
    // Start is called before the first frame update

    public void OnParticleSystemStopped()
    {
        if (typeParticle == TypeParticle.Click)
        {
            //LogicGame.Instance.clickParticlePool.Release(particle);
        }
        else if (typeParticle == TypeParticle.Eat)
        {
            //LogicGame.Instance.eatParticlePool.Release(particle);
        }
        else if (typeParticle == TypeParticle.Move)
        {
            //GameManager.Instance.comboParticlePool.Release(particle);
        }
        else if (typeParticle == TypeParticle.Unlock)
        {
            //GameManager.Instance.comboParticlePool.Release(particle);
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
