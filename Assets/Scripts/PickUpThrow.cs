using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpThrow : MonoBehaviour
{
    public float throwForce;
    private Vector3 objectPos;
    private float distance;
    public GameObject soundManager;
    public bool canHold = true;
    public GameObject item;
    public GameObject tempParent;
    public bool isHolding;
    public float pickupDistance;
    private bool pickedUp = false;
    private Collider collider;
    public AudioSource audioSource;

    private void Start()
    {
        collider = this.GetComponent<BoxCollider>();
    }

    void Update()
    {
        distance = Vector3.Distance(item.transform.position, tempParent.transform.position);


        if(Input.GetKeyDown(KeyCode.E) && distance <= pickupDistance)
        {
            isHolding = true;
            pickedUp = true;
            item.GetComponent<Rigidbody>().useGravity = false;
            item.GetComponent<Rigidbody>().detectCollisions = true;
            item.transform.position = tempParent.transform.position;
        }

        if(distance >= 10f)
        {
            isHolding = false;
        }


        if(isHolding==true)
        {
            item.GetComponent<Rigidbody>().velocity = Vector3.zero;
            item.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            item.transform.SetParent(tempParent.transform);
            collider.isTrigger = true;
            
            if(Input.GetMouseButtonDown(0))
            {
                item.GetComponent<Rigidbody>().AddForce(tempParent.transform.forward * throwForce);
                collider.isTrigger = false;
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

    private void OnCollisionEnter(Collision collision)
    {
        if(pickedUp == true)
        {
            soundManager.GetComponent<CurrentSound>().AddNewSound(this.transform.position);
            audioSource.Play();
            pickedUp = false;
        }
    }
}
