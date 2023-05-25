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


    void Update()
    {
        if(isInside && Input.GetKeyDown(KeyCode.E))
        {
            thisInstance.GetComponent<MeshRenderer>().enabled = false;
            player.GetComponent<PlayerController>().canMove = false;
            Instantiate(itemPrefab, itemPoint.transform.position, itemPoint.transform.rotation);
            isShowing = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if(isShowing && Input.GetKeyDown(KeyCode.Escape))
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
        isInside = true;
    }

    public void OnTriggerExit(Collider other)
    {
        isInside = false;
    }
}
