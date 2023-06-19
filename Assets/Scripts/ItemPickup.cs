using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public GameObject thisInstance;
    public GameObject itemPrefab;
    public bool isInside = false;
    public GameObject itemPoint;
    public GameObject player;
    private bool isShowing = false;
    public GameObject pitem;
    private bool pickedUp = false;
    public GameObject pickupIndicator;
    public GameObject hideIndicator;

    void Update()
    {
        if(isInside == true && Input.GetKeyDown(KeyCode.E) && pickedUp == false)
        {
            pickupIndicator.SetActive(false);
            thisInstance.GetComponent<MeshRenderer>().enabled = false;
            player.GetComponent<PlayerController>().canMove = false;
            Instantiate(itemPrefab, itemPoint.transform.position, itemPoint.transform.rotation);
            pitem.GetComponent<PickableItem>().isPickedUp = true;
            isShowing = true;
            hideIndicator.SetActive(true);
            pickedUp = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if(isShowing && Input.GetKeyDown(KeyCode.Q))
        {
            hideIndicator.SetActive(false);
            GameObject item = GameObject.FindGameObjectWithTag("ShowingItem");
            item.SetActive(false);
            isShowing = false;
            player.GetComponent<PlayerController>().canMove = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            isInside = true;
            if(!isShowing)
            {
                pickupIndicator.SetActive(true);
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            isInside = false;
            pickupIndicator.SetActive(false);
        }
    }
}
