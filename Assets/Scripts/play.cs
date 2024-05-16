using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class play : MonoBehaviour
{
    public static play instance;//экземпл€р этого же класса

    private static float health =8;//здоровье
    private static int maxHealth=8;//максимальное здоровье
    public static float Health { get => health; set => health = value; }
    public static int MaxHealth { get => maxHealth; set => maxHealth = value; }

    public TextMeshProUGUI healthText;//чтобы выводить здоровье
    public TextMeshProUGUI levelText;

    // Start is called before the first frame update
    void Awake()
    {
        if(Level.level==1)
        health = maxHealth;
        if (instance == null)
        {
            instance = this;//экземпл€р равен этому сценарию
        }
    }
    // Update is called once per frame
    void Update()
    {
        healthText.text = "Health: " + health;//выводим здоровье
        levelText.text = "Level - " + (Level.level);
    }
    public static void DamagePlayer(int damage)//урок игроку
    {
        health -= damage;//уздоровь€ минусуем дамаг
        if (Health <= 0)//если здоровье отрицательно или равно 0
        {
            KillPlayer();//убиваем игрока
        }
    }
    public static void HealPlayer(float healAmount)//лечение игрока
    {                                        //средство исцелени€
        health = Mathf.Min(maxHealth, health+healAmount);//отхиливаем 
    }
    private static void KillPlayer()
    {
        Level.level = 1;
        SceneManager.LoadScene("End");
    }
}
