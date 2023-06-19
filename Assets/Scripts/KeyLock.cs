using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    public TextMeshProUGUI indicatorText;
    public GameObject indicatorTextObj;

    void Start()
    {
        key.SetActive(false);
        isLocked = true;
        indicatorTextObj.SetActive(false);
    }


    void Update()
    {
        anim.SetBool("IsKeyIn", !isLocked);
        leftDoorAnim.SetBool("isUnlocked", completedUnlocking);
        rightDoorAnim.SetBool("isUnlocked", completedUnlocking);

        if(level2Manager.GetComponent<Level2Manager>().hasKey == true)
        {
            indicatorText.text = "Press [E] to unlock";
        }
        else
        {
            indicatorText.text = "Locked";
        }


        if (isInside && Input.GetKeyDown(KeyCode.E) && level2Manager.GetComponent<Level2Manager>().hasKey == true)
        {
            key.SetActive(true);
            indicatorTextObj.SetActive(false);
            isLocked = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            isInside = true;
            indicatorTextObj.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            isInside = false;
            indicatorTextObj.SetActive(false);
        }
    }

    public void OpenDoor()
    {
        completedUnlocking = true;
    }
}
