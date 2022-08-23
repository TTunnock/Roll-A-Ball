using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Timer : MonoBehaviour
{
    float currentTime = 0;
    float bestTime;
    bool timing = false;


    SceneController sceneController;

    [Header("UI Countdown Panel")]
    public GameObject countdownPanel;
    public TMP_Text countdownText;

    [Header("UI In Game Panel")]
    public TMP_Text timerText;

    [Header("UI Win Panel")]
    public GameObject timesPanel;
    public TMP_Text yourTimeResult;
    public TMP_Text bestTimeResult;



    void Start()
    {
        timesPanel.SetActive(false);
        countdownPanel.SetActive(false);
        timerText.text = "";
        sceneController = FindObjectOfType<SceneController>();
    }

    
    void Update()
    {
        
   
            if (timing)
            {
                currentTime -= Time.deltaTime;
                timerText.text = currentTime.ToString("F3");
            }
        
    }

    public IEnumerator StartCountdown()
    {
        bestTime = PlayerPrefs.GetFloat("BestTime" + sceneController.GetSceneName());
        if (bestTime == 0f) bestTime = 600f;

        countdownPanel.SetActive(true);
        countdownText.text = "3";
        yield return new WaitForSeconds(1);
        countdownText.text = "2";
        yield return new WaitForSeconds(1);
        countdownText.text = "1";
        yield return new WaitForSeconds(1);
        countdownText.text = "GO!";
        yield return new WaitForSeconds(1);
        StartTimer();
        countdownPanel.SetActive(false);
    }

    public void StartTimer()
    {
        currentTime = 0;
        timing = true;
    }

    public void StopTimer()
    {
        timing = false;
        timesPanel.SetActive(true);
        yourTimeResult.text = currentTime.ToString("F3");
        bestTimeResult.text = bestTime.ToString("F3");

       // if (currentTime == 0)
       // {
       //     //Turn on off in game panel
       //     PlayerController.inGamePanel.SetActive(false);
       //     PlayerController.winPanel.SetActive(true);
       //     //remove controls from player
       //     PlayerController.wonGame = true;
       //     //Set the velocity of the rigidbody to zero
       //     PlayerController.rb.velocity = Vector3.zero;
       //     PlayerController.rb.angularVelocity = Vector3.zero;

       //}

        if (currentTime <= bestTime)
        {
          bestTime = currentTime;
          PlayerPrefs.SetFloat("BestTime" + sceneController.GetSceneName(), bestTime);
          bestTimeResult.text = bestTime.ToString("F3") + " !! NEW BEST TIMEs !!";
        }
    }

    public bool IsTiming()
    {
        return timing;
    }
}
