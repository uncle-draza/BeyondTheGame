using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaWarning : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            //pusti zvuk upozorenja
            Debug.Log("Pazi!");
        }
    }
}
