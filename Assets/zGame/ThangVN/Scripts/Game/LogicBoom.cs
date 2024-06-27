using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BoomType
{
    Single,
    Double,
    BigBoom,
    FireBoom,
    EnergyBoom,
    CoinBoom  
}
public class LogicBoom : MonoBehaviour
{
    float posY = 10f;
    Rigidbody rb;
    //public float gravityScale;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void Init(Vector3 spawnPos)
    {
        transform.position = spawnPos;

        //float randomGravity = Random.Range(1f, 3f);
        //gravityScale = randomGravity;
    }

    //void FixedUpdate()
    //{
    //    Vector3 customGravity = gravityScale * Physics.gravity;
    //    rb.AddForce(customGravity - Physics.gravity, ForceMode.Acceleration);
    //}
}
