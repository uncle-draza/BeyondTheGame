using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilverKey : PickableItem
{
    
    public GameObject level2Manager;
    void Update()
    {
        if(isPickedUp)
        {
            level2Manager.GetComponent<Level2Manager>().hasKey = true;
        }
    }
}
