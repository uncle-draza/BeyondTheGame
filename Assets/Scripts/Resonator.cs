using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Resonator : MonoBehaviour
{
    public bool switchingLevel;
    public GameObject player;
    public float uiActivationDistance;
    public GameObject ui;
    public string finalScene;

    void Start()
    {
        
    }

    
    void Update()
    {
        float distance = Vector3.Distance(this.transform.position, player.transform.position);

        if(distance<=uiActivationDistance)
        {
            ui.SetActive(true);

            if(Input.GetKeyDown(KeyCode.E))
            {
                if(switchingLevel)
                {
                    NextLevel();
                }
                else
                {
                    FinishGame();
                }
            }
        }
        else
        {
            ui.SetActive(false);
        }
    }

    private void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void FinishGame()
    {
        SceneManager.LoadScene(finalScene);
    }
}
