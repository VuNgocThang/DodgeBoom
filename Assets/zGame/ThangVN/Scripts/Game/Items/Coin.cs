using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        SaveGame.Coin += 1;
        transform.gameObject.SetActive(false);
    }
}
