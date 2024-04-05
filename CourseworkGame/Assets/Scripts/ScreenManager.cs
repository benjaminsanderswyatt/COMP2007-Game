using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager manager;

    public DataTransferToEndScreen dataTransfer;

    public float time;
    public TextMeshProUGUI timer;
    public bool timerOn = false;

    private void Start()
    {
        DontDestroyOnLoad(dataTransfer);
    }
    public void BeginTimer()
    {
        if (!timerOn)
        {

            time = 0;
            timerOn = true;

            StartCoroutine(UpdateTimer());
        }
    }

    public void EndTimer()
    {
        timerOn = false;
    }


    private IEnumerator UpdateTimer()
    {
        while (timerOn)
        {
            time += Time.deltaTime;
            var timePlaying = TimeSpan.FromSeconds(time);
            timer.text = timePlaying.ToString("mm':'ss'.'ff");

            yield return null;
        }
    }


    private void Awake()
    {
        manager = this;
    }

    public void WinGame()
    {
        dataTransfer.SetData(true, time);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }



    public void GameOver()
    {
        dataTransfer.SetData(false,time);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }



}
