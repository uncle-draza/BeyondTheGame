using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nyarlathotep : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject subttles;

    private void OnEnable()
    {
        subttles.SetActive(true);
    }
    void Update()
    {
        if(!audioSource.isPlaying)
        {
            subttles.SetActive(false);
            Destroy(gameObject, 1f);
        }
    }
}
