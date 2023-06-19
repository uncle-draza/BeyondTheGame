using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public AudioSource walkingsound, runningsound;
    public GameObject player;

    void Update()
    {
        if((Input.GetKey(KeyCode.W)|| Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) && !Input.GetKey(KeyCode.LeftControl) && player.GetComponent<PlayerController>().characterController.isGrounded)
        {
            if(Input.GetKey(KeyCode.LeftShift))
            {
                runningsound.enabled = true;
                walkingsound.enabled = false;
            }
            else
            {
                walkingsound.enabled = true;
                runningsound.enabled = false;
            }
        }
        else
        {
            walkingsound.enabled = false;
            runningsound.enabled = false;
        }
    }
}
