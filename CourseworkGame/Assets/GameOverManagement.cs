using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManagement : MonoBehaviour
{
    private DataTransferToEndScreen data;

    public TextMeshProUGUI finalTime;

    public TextMeshProUGUI bestTime;

    public GameObject deathScreen;
    public GameObject winScreen;

    public SaveData saveData;



    private void Awake()
    {
        SaveSystem.Initialize();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        data = FindObjectOfType<DataTransferToEndScreen>();

        

        winScreen.SetActive(false);
        deathScreen.SetActive(false);

        if (data.state)
        {
            //win
            winScreen.SetActive(true);
            var timePlaying = TimeSpan.FromSeconds(data.time);
            finalTime.text = timePlaying.ToString("mm':'ss'.'ff");



            string loadedData = SaveSystem.Load("save");

            saveData = new SaveData(0);

            if (loadedData != null)
            {
                saveData = JsonUtility.FromJson<SaveData>(loadedData);
            }

            if (saveData.bestTime > data.time || saveData.bestTime == 0)
            {
                //new best time
                saveData.bestTime = data.time;
            }

            var bestTimeSpan = TimeSpan.FromSeconds(saveData.bestTime);
            bestTime.text = bestTimeSpan.ToString("mm':'ss'.'ff");

            string saveingData = JsonUtility.ToJson(saveData);
            SaveSystem.Save("save", saveingData);

        }
        else
        {
            //loss
            deathScreen.SetActive(true);
        }
    }

    public void TryAgain()
    {
        SceneManager.LoadScene(2);
    }
}

[Serializable]
public class SaveData
{
    public float bestTime;

    public SaveData (float bT)
    {
        bestTime = bT;
    }
}