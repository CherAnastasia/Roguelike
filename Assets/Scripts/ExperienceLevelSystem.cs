using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExperienceLevelSystem : MonoBehaviour
{
    public TextMeshProUGUI levelText;
    static int record;
    void Awake()
    {
        //record = 0;
    }
    void Start()
    {
        record = PlayerPrefs.GetInt("RecordPlayer");
        levelText.text = Convert.ToString(record);
    }
    void Update()
    {
        levelText.text = Convert.ToString(record);
    }
    public static void UpdatedRecord(int level)
    {
            if (level > record)
            {
                record = (level);
                Debug.Log("New record!");
             }
        SaveRecord();
    }

    public static void SaveRecord()
    {
        PlayerPrefs.SetInt("RecordPlayer", record);
        PlayerPrefs.Save();
        Debug.Log("Save record!");
    }
}

