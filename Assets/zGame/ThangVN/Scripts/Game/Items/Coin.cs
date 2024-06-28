using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Add Coin += 1");
            SaveGame.Coin += 1;
            transform.gameObject.SetActive(false);
        }
    }
}
