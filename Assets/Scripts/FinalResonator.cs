using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalResonator : MonoBehaviour
{
    public GameObject player;
    public float uiActivationDistance;
    public GameObject indicatorText;


    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(this.transform.position, player.transform.position);

        if (distance <= uiActivationDistance)
        {
            indicatorText.SetActive(true);
            player.GetComponent<Shoot>().gunOut = true;
        }
        else
        {
            indicatorText.SetActive(false);
        }
    }
}