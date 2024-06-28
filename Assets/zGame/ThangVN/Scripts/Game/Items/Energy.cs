using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Add Energy += 10");
            SaveGame.Energy += 10;
            transform.gameObject.SetActive(false);
        }
    }
}
