using TMPro;
using UnityEngine;

public class player : MonoBehaviour
{
    public float speed;//�������� ������
    Rigidbody2D rigidbody2D;//�����
    public TextMeshProUGUI collectedText;
    public static int collectedAmount=0;
    public GameObject bulletPrefab;//����
    public float bulletSpeed;//�������� ����
    private float lasttFire;//��������� �����
    public float fireDelay;//�������� ����
    void Awake()
    {
        collectedAmount = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D= GetComponent<Rigidbody2D>();//������ ������� ����
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");//��������������� ��� ��� ������ (��������� �� a d)
        float vertical = Input.GetAxis("Vertical");//������������ ��� ��� ������ (��������� �� w s)

        float shootHor = Input.GetAxis("ShootHorizontal");//��������������� ��� ��� ���� (��� ������� �� ���� �������) (��������� �� ����� ����)
        float shootVert = Input.GetAxis("ShootVertical");//������������ ��� ��� ���� (��� ������� �� ���� �������) (��������� �� ��� �����)
        if ((shootHor!=0 || shootVert!=0) && Time.time>lasttFire+fireDelay)//��������� ������� �� �� ���� ��� ����� ������ + ������ �� ��� ��� ���� ��������+��������� ����� 
        {
            Shoot(shootHor,shootVert);//��������
            lasttFire= Time.time;//������������� �������� ����������� ����
        }

        rigidbody2D.velocity = new Vector3(horizontal*speed, vertical*speed,0);//������������� �������� ���� (�� z ������������ ������)
        collectedText.text = "Item Collected: " + collectedAmount;//������ �������� ������ (������ ���������� ��������� ��������)
    }

    void Shoot(float x, float y)//����� �������� ����������� � ���� ���� � ���
    {
        GameObject bullet = Instantiate(bulletPrefab,transform.position, transform.rotation) as GameObject;//������� ������ ����� ���������� ���� � ����� ������� ��� �������������� ��� �������� ������ (?)
        bullet.AddComponent<Rigidbody2D>().gravityScale = 0;//������ ������� ������ � ����������� =0
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(//�������� ������ �������3
            (x < 0) ? Mathf.Floor(x) * bulletSpeed : Mathf.Ceil(x) * bulletSpeed,
            (y < 0) ? Mathf.Floor(y) * bulletSpeed : Mathf.Ceil(y) * bulletSpeed,
            0); //(�� z ������������ ������)
    }
}
