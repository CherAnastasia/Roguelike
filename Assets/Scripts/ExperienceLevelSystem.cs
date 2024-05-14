using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExperienceLevelSystem : MonoBehaviour
{
    int record;
    void Awake()
    {
        record = 0;
    }
    public TextMeshProUGUI recordText;
    void Start()
    {
        record = PlayerPrefs.GetInt("RecordPlayer");
        recordText.text = "Record: " + record;
    }

    void Update()
    {
        if (player.numberEnemiesKilled > record)
        {
            record = player.numberEnemiesKilled;
            Debug.Log("New record!");
            recordText.text = "Record: " + record;
        }
    }
    public void SaveRecord()
    {
        PlayerPrefs.SetInt("RecordPlayer", record);
        PlayerPrefs.Save();
    }
}

