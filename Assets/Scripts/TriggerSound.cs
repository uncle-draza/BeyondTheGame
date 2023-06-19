using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSound : MonoBehaviour
{
    public AudioSource audioSource;
    private bool hasPlayed = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            if(!hasPlayed)
            {
                audioSource.Play();
                hasPlayed = true;
            }
        }
    }
}
/*private var hasPlayed = false;

function OnTriggerEnter(){
    if(!hasPlayed){
        audio.PlayOneShot(Sound);
        hasPlayed = true;
    }
}*/