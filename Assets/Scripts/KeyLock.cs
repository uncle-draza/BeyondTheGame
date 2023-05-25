using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyLock : MonoBehaviour
{
    public GameObject key;
    public bool isInside;
    public bool isLocked;
    public GameObject level2Manager;
    public Animator anim;
    public Animator leftDoorAnim;
    public Animator rightDoorAnim;
    public bool completedUnlocking = false;
    void Start()
    {
        key.SetActive(false);
        isLocked = true;
    }


    void Update()
    {
        anim.SetBool("IsKeyIn", !isLocked);
        leftDoorAnim.SetBool("isUnlocked", completedUnlocking);
        rightDoorAnim.SetBool("isUnlocked", completedUnlocking);


        if (isInside && Input.GetKeyDown(KeyCode.E) && level2Manager.GetComponent<Level2Manager>().hasKey == true)
        {
            key.SetActive(true);
            isLocked = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
            isInside = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
            isInside = false;
    }

    public void OpenDoor()
    {
        completedUnlocking = true;
    }
}
