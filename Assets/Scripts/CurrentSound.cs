using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentSound : MonoBehaviour
{
    public Vector3 soundPosition;
    public float soundDuration;
    //[HideInInspector]
    public float currentTime;
    private bool isTimerRunning;


    void Start()
    {
    }

    void Update()
    {
        if(isTimerRunning)
        {
            currentTime -= Time.deltaTime;

            if(currentTime<=0f)
            {
                currentTime = 0f;
                isTimerRunning = false;
                ResetSoundPosition();
            }
        }
    }

    public void AddNewSound(Vector3 position)
    {
        soundPosition.x = position.x;
        soundPosition.y = position.y;
        soundPosition.z = position.z;

        ResetTimer();
        StartTimer();
    }

    private void StartTimer()
    {
        isTimerRunning = true;
    }

    private void PauseTimer()
    {
        isTimerRunning = false;
    }
    private void ResetTimer()
    {
        currentTime = soundDuration;
        isTimerRunning = false;
    }

    private void ResetSoundPosition()
    {
        soundPosition.x = 0f;
        soundPosition.y = 0f;
        soundPosition.z = 0f;
    }
}
