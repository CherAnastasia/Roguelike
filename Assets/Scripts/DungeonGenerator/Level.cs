using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Level : MonoBehaviour
{
    public static int level = 1;
    static bool trig = true;
    void Update()
    {
        trig = true;
    }
    void OnTriggerEnter2D(Collider2D collision)
   {
        if (trig)
        {
            if (collision.tag == "Player" && level == 1)
            {
                SceneManager.LoadScene("BasementMain2");
                ExperienceLevelSystem.UpdatedRecord(level);  
                level++;
            }
            else if (collision.tag == "Player" && level == 2)
            {
                SceneManager.LoadScene("EndLevels");
                ExperienceLevelSystem.UpdatedRecord(level);
            }

        }
        trig = false;
    }
}
