using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillArea : MonoBehaviour
{
    public GameObject player;

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            player.GetComponent<PlayerController>().Die("You were lost in the void...");
        }
    }
}
