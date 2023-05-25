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

    void Update()
    {
        if(isInside == true && Input.GetKeyDown(KeyCode.E) && pickedUp == false)
        {
            thisInstance.GetComponent<MeshRenderer>().enabled = false;
            player.GetComponent<PlayerController>().canMove = false;
            Instantiate(itemPrefab, itemPoint.transform.position, itemPoint.transform.rotation);
            pitem.GetComponent<PickableItem>().isPickedUp = true;
            isShowing = true;
            pickedUp = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if(isShowing && Input.GetKeyDown(KeyCode.Q))
        {
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
            isInside = true;
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.tag.Equals("Player"))
            isInside = false;
    }
}
