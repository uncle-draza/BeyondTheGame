using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public int scenePlayedIndex;
    private bool wasPlayed;
    public Button continueButton;
    public GameObject confirmationPanel;
    public GameObject pressToContinue;
    public GameObject ui;
    public TextMeshProUGUI quoteTextUI;
    public string quoteText;
    public Sprite spriteToShow;
    public Image backgroundImage;
    public string firstCutsceneSceneName;


    void Start()
    {
        //proveri da li je igra igrana
        scenePlayedIndex = PlayerPrefs.GetInt("LevelIndex", 999);
        if(scenePlayedIndex == 999)
        {
            wasPlayed = false;
        }
        else
        {
            wasPlayed = true;
        }
        //ako nije, onemoguci dugme continue, ako jeste, omoguci ga
        if(wasPlayed == true)
        {
            continueButton.interactable = true;
        }
        else
        {
            continueButton.interactable = false;
        }


        //iskljuci confirmation panel
        confirmationPanel.SetActive(false);
    }


    void Update()
    {
        
    }

    public void NewGame()
    {
        confirmationPanel.SetActive(true);
    }

    public void Play()
    {
        SceneManager.LoadScene(firstCutsceneSceneName);
    }

    public void Cancel()
    {
        confirmationPanel.SetActive(false);
    }

    public void Continue()
    {
        ui.SetActive(true);
        quoteTextUI.text = quoteText;
        backgroundImage.sprite = spriteToShow;
        LoadLevel();
    }
    void LoadLevel()
    {
        StartCoroutine(LoadScene());
    }
    IEnumerator LoadScene()
    {
        yield return null;

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(scenePlayedIndex);
        asyncOperation.allowSceneActivation = false;
        while (!asyncOperation.isDone)
        {
            if (asyncOperation.progress >= 0.9f)
            {
                pressToContinue.SetActive(true);
                if (Input.GetKeyDown(KeyCode.L))
                {
                    asyncOperation.allowSceneActivation = true;
                }
            }
            yield return null;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
