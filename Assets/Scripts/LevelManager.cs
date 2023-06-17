using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    void Start()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("LevelIndex", index);
        Debug.Log("Sacuvan pokrenut level sa indeksom " + index);
    }
}
