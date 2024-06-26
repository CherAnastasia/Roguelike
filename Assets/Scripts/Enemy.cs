using System.Collections;
using UnityEngine;

public enum EnemyState //��������� �����
{
    Idle,
    Wander,//������ �����
    Follow,//������� �� ����
    Die,//����
    Attack//�������
};
public enum EnemyType//���� ������
{
    Melee,//������� ����
    Ranged,//�������
    Boss
};

public class Enemy : MonoBehaviour
{
    GameObject playerr;//�����
    public EnemyState currState = EnemyState.Idle;//���������� ��������� ����� ����� ������
    public EnemyType enemyType;//��� �����
    public float range;//�������� ����� �����
    public float speed;//�������� �����
    public float attackRange;//�������� �����
   //public float bulletSpeed;//�������� ����
    public float coolDown;//����� �������������
    private bool chooseDir = false;//����� �����������
   // private bool dead = false;//���� �����
    private bool coolDownAttack = false;//����� ������������� �����
    public bool notInRoom = false;
   //private Vector3 randomDir;//��������� ����������� �������
    public GameObject bulletPrefab;
    public bool isBoss;
    public static float health = 8;//��������
    public static int maxHealth = 8;//������������ ��������
    public static float Health { get => health; set => health = value; }
    public static int MaxHealth { get => maxHealth; set => maxHealth = value; }
    // Start is called before the first frame update
    void Awake()
    {
        health = maxHealth;
    }
    void Start()
    {
        playerr = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        switch(currState)//� ����������� �� ���������
        {
            case (EnemyState.Wander)://���� ������
            Wander(); break;
            case (EnemyState.Follow)://���� ����������
            Follow(); break;
            case (EnemyState.Die)://���� ����
            break;
            case (EnemyState.Attack)://���� �������
            Attack(); break;
        }
        if(!notInRoom)
        {
            if (IsPlayerInRange(range) && currState != EnemyState.Die)//���� ��������� � ��������� �����+���� ���
            {
                currState = EnemyState.Follow;
               // if (enemyType == EnemyType.Boss) play.instance.bossHeaith.SetActive(true);
            }
            else if (!IsPlayerInRange(range) && currState != EnemyState.Die)//���� ��������� �� ���������� �����+���� ���
            {
                currState = EnemyState.Wander;
                //if (enemyType == EnemyType.Boss) play.instance.bossHeaith.SetActive(true);
                //else play.instance.bossHeaith.SetActive(false);
            }
            if (Vector3.Distance(transform.position, playerr.transform.position) <= attackRange) //������� �� ��� �� ������� ������
            {
                currState = EnemyState.Attack;//�������
               // if (enemyType == EnemyType.Boss) play.instance.bossHeaith.SetActive(true);
            }
        }
        else
        {
           // play.instance.bossHeaith.SetActive(false);
            currState = EnemyState.Idle;
        }
    }

    private bool IsPlayerInRange(float range)//����� � ������� �����?
    {
        //������� �� ��� �� ������� ������
        return Vector3.Distance(transform.position, playerr.transform.position) <= range;//���������� ���������� ����� ����� ������� �������� � �������� ������
            //��������� ������ �� ��� ���������
    }
    /*����������� ��� �����, ������� ����� ������������� ���������� � ������� ���������� Unity, 
     �� ����� ���������� � ���� �����, �� ������� ��� ���� �����������, � ��������� ������.*/
    private IEnumerator ChooseDirection()//����������� � ����� IEnumerator + yield
    {
        chooseDir = true;//����������� ��������
        yield return new WaitForSeconds(Random.Range(2f,8f)); //���� ��������� ���������� ������� ����� 2 ��������� � 8 ��������� � ���������� ����� �������� (yield return: ���������� ������������ �������)
        //randomDir = new Vector3(0, 0, Random.Range(0, 360));//������������� ��������� �����������
        //Quaternion nextRotation = Quaternion.Euler(randomDir);//��� ���������?
        //transform.rotation = Quaternion.Lerp(transform.rotation, nextRotation, Random.Range(0.5f, 2.5f));//��������� ��������?
        chooseDir = false;//����������� �� �������
    }
    void Wander()//��� ��������� �����
    {
        if(!chooseDir)//����������� �� �������
        {
            StartCoroutine(ChooseDirection());
        }
        transform.position+=transform.right * speed * Time.deltaTime;
        if(IsPlayerInRange(range)) //� �������� ������������ �� ��
        {
            currState = EnemyState.Follow;
        }
    }
    void Follow()//��� ������������� �����
    {
        transform.position = Vector2.MoveTowards(transform.position, playerr.transform.position, speed * Time.deltaTime);
    }
    void Attack()//��� �����
    {
        if (!coolDownAttack)//���� �� �� �������������� �����
        {
            switch(enemyType)//������ �������� ������������ �� �����
            {
                case (EnemyType.Melee):
                    play.DamagePlayer(1);//������� ����
                    StartCoroutine(CoolDown());//����� ��������� ����� ������ � ����� �����
                    break;
                case (EnemyType.Ranged):
                    GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject;//������� ������ ����
                    bullet.GetComponent<Bullet>().GetPlayer(playerr.transform);
                    bullet.AddComponent<Rigidbody2D>().gravityScale = 0;//������������� ����������
                    bullet.GetComponent<Bullet>().isEnemyBullet= true;//��������� ����
                    StartCoroutine(CoolDown());//�����������
                    break;
                case (EnemyType.Boss):
                    GameObject bullet1 = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject;//������� ������ ����
                    bullet1.GetComponent<Bullet>().GetPlayer(playerr.transform);
                    bullet1.AddComponent<Rigidbody2D>().gravityScale = 0;//������������� ����������
                    bullet1.GetComponent<Bullet>().isEnemyBullet = true;//��������� ����
                    StartCoroutine(CoolDown());//�����������
                    break;
            }

        }
    }
    /*����������� ��� �����, ������� ����� ������������� ���������� � ������� ���������� Unity, 
    �� ����� ���������� � ���� �����, �� ������� ��� ���� �����������, � ��������� ������.*/
    private IEnumerator CoolDown()//����������� � ����� IEnumerator + yield
    {
        coolDownAttack = true;
        yield return new WaitForSeconds(coolDown);
        coolDownAttack = false;
    }
    public void Death()
    {
        //player.numberEnemiesKilled++;//����������� �������
        // RoomController.instance.UpdateRooms();

        RoomController.instance.StartCoroutine(RoomController.instance.RoomCoroutune());
        Destroy(gameObject);//����������� �����
        if(enemyType==EnemyType.Boss)
        {
            play.instance.port.SetActive(true);
          play.instance.bossHeaith.SetActive(false);
           play.instance.port.transform.position =transform.position;
        }
    }
    public void DamageBoos(int damage)//����
    {
        play.instance.bossHeaith.SetActive(true);
        health -= damage;//��������� �������� �����
        if (Health <= 0)//���� �������� ������������ ��� ����� 0
        {
            Death();//������� 
        }
    }
}
