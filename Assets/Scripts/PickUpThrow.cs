using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpThrow : MonoBehaviour
{
    public float throwForce; //600
    private Vector3 objectPos;
    private float distance;

    public bool canHold = true;
    public GameObject item;
    public GameObject tempParent;
    public bool isHolding;
    public float pickupDistance;

    void Update()
    {
        distance = Vector3.Distance(item.transform.position, tempParent.transform.position);
        if(distance >= 10f)
        {
            isHolding = false;
        }
        if(isHolding==true)
        {
            item.GetComponent<Rigidbody>().velocity = Vector3.zero;
            item.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            item.transform.SetParent(tempParent.transform);
            
            if(Input.GetMouseButtonDown(1))
            {
                item.GetComponent<Rigidbody>().AddForce(tempParent.transform.forward * throwForce);
                isHolding = false;
            }
        }
        else
        {
            objectPos = item.transform.position;
            item.transform.SetParent(null);
            item.GetComponent<Rigidbody>().useGravity = true;
            item.transform.position = objectPos;
        }
    }

    private void OnMouseDown()
    {
        if(distance<=pickupDistance)
        {
            isHolding = true;
            item.GetComponent<Rigidbody>().useGravity = false;
            item.GetComponent<Rigidbody>().detectCollisions = true;
        }
    }

    private void OnMouseUp()
    {
        isHolding = false;
    }
}
