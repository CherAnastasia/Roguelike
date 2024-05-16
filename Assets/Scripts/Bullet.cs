using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime; //����� ����� ����
    public bool isEnemyBullet = false;//��������� ����
    private Vector2 lastPos;//��������� �������
    private Vector2 curPos;//������� �������
    private Vector2 playerPos;//������� ������
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DeathDelay());//�������� ����������� ����
    }

    // Update is called once per frame
    void Update()
    {
        if(isEnemyBullet)
        {
            curPos= transform.position;//������� ������� ������ ����� �������?
            transform.position = Vector2.MoveTowards(transform.position, playerPos, 5f * Time.deltaTime);//��������� �� ����� ������� � ������� ������
            if(curPos== lastPos)//��������� ����� �� ���� ������� ������� ����� ��������� �������
            {
                Destroy(gameObject);//����������� �� �������� �������
            }
            lastPos = curPos;
        }
    }
    public void GetPlayer(Transform player)
    {
        playerPos = player.position;//������� ������ ����� ��������� ����� ������
    }
    /*����������� ��� �����, ������� ����� ������������� ���������� � ������� ���������� Unity, 
     �� ����� ���������� � ���� �����, �� ������� ��� ���� �����������, � ��������� ������.*/
    IEnumerator DeathDelay() //����������� � ����� IEnumerator + yield
    {
        yield return new WaitForSeconds(lifeTime);//������� � ��������� ������� � lifeTime (yield return: ���������� ������������ �������)
        Destroy(gameObject);//����������� ����
    }
    private void OnTriggerEnter2D(Collider2D collision)//����� ������� �������� ��� �������� (��������� � ���� ������������)
    {
        if(collision.tag == "Enemy" && !isEnemyBullet)//���� ��� ���� � ��� �� ��������� ����
        {
            collision.gameObject.GetComponent<Enemy>().Death(); //����� �����   
            Destroy(gameObject);//����������� ����
        }
        if(collision.tag == "Player" && isEnemyBullet) //���� ��� ����� � ��� ��������� ����
        {
            play.DamagePlayer(1);//������� ����
            Destroy(gameObject);//���������� ����
        }
        if (collision.tag == "Obstacle")
        {
            Destroy(gameObject);//���������� ����
        }
    }
}
