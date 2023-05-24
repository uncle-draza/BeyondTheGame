using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public GameObject thisPrefab;
    public bool isInside = false;

    void Start()
    {
        
    }


    void Update()
    {
        if(isInside && Input.GetKeyDown(KeyCode.E))
        {
            thisPrefab.GetComponent<MeshRenderer>().enabled = false;
            
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        isInside = true;
    }

    public void OnTriggerExit(Collider other)
    {
        isInside = false;
    }
}
