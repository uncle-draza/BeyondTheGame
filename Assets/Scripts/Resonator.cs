using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;


public class Resonator : MonoBehaviour
{
    public bool switchingLevel;
    public GameObject player;
    public float uiActivationDistance;
    public GameObject ui;
    public string finalScene;
    public TextMeshProUGUI quoteTextUI;
    public string quoteText;
    public Image backgroundImage;
    public Sprite spriteToShow;
    public string levelToLoad;
    public GameObject pressToContinue;
    public GameObject indicatorText;

    private void Start()
    {
        pressToContinue.SetActive(false);
        indicatorText.SetActive(false);
    }

    void Update()
    {
        float distance = Vector3.Distance(this.transform.position, player.transform.position);

        if(distance<=uiActivationDistance)
        {
            indicatorText.SetActive(true);
            if(Input.GetKeyDown(KeyCode.E))
            {
                if(switchingLevel)
                {
                    ui.SetActive(true);
                    quoteTextUI.text = quoteText;
                    backgroundImage.sprite = spriteToShow;
                    LoadLevel();
                }
                else
                {
                    FinishGame();
                }
            }
        }
        else
        {
            indicatorText.SetActive(false);
        }
    }

    private void FinishGame()
    {
        SceneManager.LoadScene(finalScene);
    }
    void LoadLevel()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        yield return null;

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(levelToLoad);
        asyncOperation.allowSceneActivation = false;
        while (!asyncOperation.isDone)
        {
            if (asyncOperation.progress >= 0.9f)
            {
                pressToContinue.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    asyncOperation.allowSceneActivation = true;
                }    
            }
            yield return null;
        }
    }
}
