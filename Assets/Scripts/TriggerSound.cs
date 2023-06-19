using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TriggerSound : MonoBehaviour
{
    public AudioSource audioSource;
    public bool hasPlayed = false;
    public string subtitleText;
    public TextMeshProUGUI subtitleObj;

    void Update()
    {
        if (!audioSource.isPlaying && hasPlayed)
        {
            subtitleObj.text = "";
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            if(!hasPlayed)
            {
                audioSource.Play();
                subtitleObj.text = subtitleText;
                hasPlayed = true;
            }
        }
    }
}
