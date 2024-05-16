using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class play : MonoBehaviour
{
    public static play instance;//��������� ����� �� ������

    private static float health =8;//��������
    private static int maxHealth=8;//������������ ��������
    public static float Health { get => health; set => health = value; }
    public static int MaxHealth { get => maxHealth; set => maxHealth = value; }

    public TextMeshProUGUI healthText;//����� �������� ��������
    public TextMeshProUGUI levelText;

    // Start is called before the first frame update
    void Awake()
    {
        if(Level.level==1)
        health = maxHealth;
        if (instance == null)
        {
            instance = this;//��������� ����� ����� ��������
        }
    }
    // Update is called once per frame
    void Update()
    {
        healthText.text = "Health: " + health;//������� ��������
        levelText.text = "Level - " + (Level.level);
    }
    public static void DamagePlayer(int damage)//���� ������
    {
        health -= damage;//��������� �������� �����
        if (Health <= 0)//���� �������� ������������ ��� ����� 0
        {
            KillPlayer();//������� ������
        }
    }
    public static void HealPlayer(float healAmount)//������� ������
    {                                        //�������� ���������
        health = Mathf.Min(maxHealth, health+healAmount);//���������� 
    }
    private static void KillPlayer()
    {
        Level.level = 1;
        SceneManager.LoadScene("End");
    }
}
