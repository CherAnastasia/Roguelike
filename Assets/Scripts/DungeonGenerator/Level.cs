using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class Level : MonoBehaviour
{
    public static int level = 1;
    public TextMeshProUGUI levelText;
   static bool trig = true;
    void Awake()
    {

    }
    private void Start()
    {
    }
    void Update()
    {
        trig = true;
    }
    static void UpdatedLevel()
    {
        ExperienceLevelSystem.UpdatedRecord(level); 
        //ExperienceLevelSystem.SaveRecord();
        level++;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (trig)
        {
            if (collision.tag == "Player" && level == 1)
            {
                SceneManager.LoadScene("BasementMain2");
            }
            if (collision.tag == "Player" && level == 2)
            {
                SceneManager.LoadScene("EndLevels");
            }
            UpdatedLevel();
        }
        trig = false;
    }
}
