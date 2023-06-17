using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NtpActivation : MonoBehaviour
{
    public GameObject ntp;

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            ntp.SetActive(true);
        }
    }
}
