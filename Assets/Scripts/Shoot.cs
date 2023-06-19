using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Shoot : MonoBehaviour
{
    public float fireRate = 0.25f;                                        // Number in seconds which controls how often the player can fire
    public float weaponRange;                                        // Distance in Unity units over which the player can fire
    public Transform gunEnd;                                            // Holds a reference to the gun end object, marking the muzzle location of the gun
    public Camera fpsCam;                                                // Holds a reference to the first person camera
    private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);    // WaitForSeconds object used by our ShotEffect coroutine, determines time laser line will remain visible
    public AudioSource gunAudio;                                        // Reference to the audio source which will play our shooting sound effect
    private float nextFire;                                                // Float to store the time the player will be allowed to fire again, after firing
    public int bullets = 7;
    public AudioClip hasBulets;
    public AudioClip noBullets;
    public bool gunOut = false;
    public GameObject canShootMessage;
    public bool canShootMessageShow;
    public float shootMessageDistance;
    public GameObject resonator;
    public Animator anim;
    private bool canDrawGun = false;

    void Start()
    {
    }


    void Update()
    {
        anim.SetBool("gunOut", gunOut);

        if(Vector3.Distance(this.transform.position, resonator.transform.position) < weaponRange)
        {
            canDrawGun = true;
        }

        if(canShootMessageShow)
        {
            if (Vector3.Distance(resonator.transform.position, this.transform.position) < shootMessageDistance)
            {
                canShootMessage.SetActive(true);
            }
            else
            {
                canShootMessage.SetActive(false);
            }
        }


        /*if (bullets <= 0)
            bullets = 0;
        */

        if(Input.GetKeyDown(KeyCode.Q) && canDrawGun)
        {
            if (gunOut)
                gunOut = false;
            else
                gunOut = true;
        }

        // Check if the player has pressed the fire button and if enough time has elapsed since they last fired
        if (Input.GetButtonDown("Fire1") && Time.time > nextFire && gunOut)
        {
            ShootGun();
        }
    }


    private IEnumerator ShotEffect()
    {
        if(bullets <= 0)
            bullets = 0;

        if (bullets>0)
        {
            gunAudio.PlayOneShot(hasBulets);
            anim.Play("gun_shoot");
        }
        else
        {
            gunAudio.PlayOneShot(noBullets);
        }
        yield return shotDuration;
    }

    private void ShootGun()
    {
            bullets--;
            // Update the time when our player can fire next
            nextFire = Time.time + fireRate;

            // Start our ShotEffect coroutine to turn our laser line on and off
            StartCoroutine(ShotEffect());

            // Create a vector at the center of our camera's viewport
            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

            // Declare a raycast hit to store information about what our raycast has hit
            RaycastHit hit;

            // Check if our raycast has hit anything
            if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange))
            {
                Debug.Log(hit.collider.tag.ToString());
                if(hit.collider.tag == "Resonator")
                {
                    Invoke("FinishGame", hasBulets.length);
                }
            }

    }

    private void FinishGame()
    {
        SceneManager.LoadScene("Cutscene2");
    }
}
