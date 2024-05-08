using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class play : MonoBehaviour
{
    public static play instance;//экземпл€р этого же класса
    private static float health =8;//здоровье
    private static int maxHealth=8;//максимальное здоровье
    //private static float moveSpeed=5f;//скорость перемещени€?
    //private static float fireRate=0.5f;//скорость стрельбы?
    public static float Health { get => health; set => health = value; }
    public static int MaxHealth { get => maxHealth; set => maxHealth = value; }
    //public static float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    //public static float FireRate { get => fireRate; set => fireRate = value; }
    public TextMeshProUGUI healthText;//чтобы выводить здоровье

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;//экземпл€р равен этому сценарию
        }
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "Health: " + health;//выводим здоровье
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

    }
}
