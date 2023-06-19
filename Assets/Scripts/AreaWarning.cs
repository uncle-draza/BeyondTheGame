using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaWarning : MonoBehaviour
{
    public AudioSource audioSource;
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            audioSource.Play();
            Debug.Log("Pazi!");
        }
    }
}
