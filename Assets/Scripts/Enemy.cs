using System.Collections;
using UnityEngine;

public enum EnemyState //состояния врага
{
    Idle,
    Wander,//бродит ходит
    Follow,//следует за нами
    Die,//умер
    Attack//атакуем
};
public enum EnemyType//виды врагов
{
    Melee,//ближний боец
    Ranged//стрелок
};

public class Enemy : MonoBehaviour
{
    GameObject playerr;//игрок
    public EnemyState currState = EnemyState.Idle;//начальнгое состояния врага ходит бродит
    public EnemyType enemyType;//тип врага
    public float range;//диапазон глаза врага
    public float speed;//скорость врага
    public float attackRange;//диапазон атаки
   // public float bulletSpeed;//скорость пули
    public float coolDown;//время востановления
    private bool chooseDir = false;//выбор направления
   // private bool dead = false;//враг мертв
    private bool coolDownAttack = false;//время востановления атаки
    public bool notInRoom = false;
    private Vector3 randomDir;//рандомное направление гуляния
    public GameObject bulletPrefab;
    // Start is called before the first frame update
    void Start()
    {
        playerr = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        switch(currState)//в зависимости от состояния
        {
            //case (EnemyState.Idle)://если гуляет
            //   /* Idle();*/ break;
            case (EnemyState.Wander)://если гуляет
            Wander(); break;
            case (EnemyState.Follow)://если приследует
            Follow(); break;
            case (EnemyState.Die)://если умер
            break;
            case (EnemyState.Attack)://если атакуем
            Attack(); break;
        }
        if(!notInRoom)
        {
            if (IsPlayerInRange(range) && currState != EnemyState.Die)//если находится в диапазоне глаза+враг жив
            {
                currState = EnemyState.Follow;
            }
            else if (!IsPlayerInRange(range) && currState != EnemyState.Die)//если находится за диапазоном глаза+враг жив
            {
                currState = EnemyState.Wander;
            }
            if (Vector3.Distance(transform.position, playerr.transform.position) <= attackRange) //позиция от нас до позиции игрока
            {
                currState = EnemyState.Attack;//атакуем
            }
        }
        else
        {
            currState = EnemyState.Idle;
        }
    }

    private bool IsPlayerInRange(float range)//игрок в радиусе глаза?
    {
        //позиция от нас до позиции игрока
        return Vector3.Distance(transform.position, playerr.transform.position) <= range;//возвращаем расстояние между нашей текущей позицией и позицией игрока
            //проверяем меньше ли оно диапазону
    }
    /*сопрограмма это метод, который может приостановить выполнение и вернуть управление Unity, 
     но затем продолжить с того места, на котором оно было остановлено, в следующем фрейме.*/
    private IEnumerator ChooseDirection()//сопрограмма с типом IEnumerator + yield
    {
        chooseDir = true;//направление выбранно
        yield return new WaitForSeconds(Random.Range(2f,8f)); //ждем рандомное количество времени между 2 секундами и 8 секундами и возвращаем новые значения (yield return: определяет возвращаемый элемент)
        //randomDir = new Vector3(0, 0, Random.Range(0, 360));//устанавливаем случайное направление
        //Quaternion nextRotation = Quaternion.Euler(randomDir);//для вращениея?
        //transform.rotation = Quaternion.Lerp(transform.rotation, nextRotation, Random.Range(0.5f, 2.5f));//обновляем вращение?
        chooseDir = false;//направление не выбрано
    }
    void Wander()//для гуляющего врага
    {
        if(!chooseDir)//направление не выбрано
        {
            StartCoroutine(ChooseDirection());
        }
        transform.position+=transform.right * speed * Time.deltaTime;
        if(IsPlayerInRange(range)) //в пределах досягаемости ли мы
        {
            currState = EnemyState.Follow;
        }
    }
    void Follow()//для приследующего врага
    {
        transform.position = Vector2.MoveTowards(transform.position, playerr.transform.position, speed * Time.deltaTime);
    }
    void Attack()//для атаки
    {
        if(!coolDownAttack)//если мы не востанавливаем атаку
        {
            switch(enemyType)//разные действия взависимости от врага
            {
                case (EnemyType.Melee):
                    play.DamagePlayer(1);//наносим урон
                    StartCoroutine(CoolDown());//после нанесения урона уходим в откат атаки
                    break;
                case (EnemyType.Ranged):
                    GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject;//создаем обьект пули
                    bullet.GetComponent<Bullet>().GetPlayer(playerr.transform);
                    bullet.AddComponent<Rigidbody2D>().gravityScale = 0;//устанавливаем графитацию
                    bullet.GetComponent<Bullet>().isEnemyBullet= true;//вражеская пуля
                    StartCoroutine(CoolDown());//перезарядка
                    break;
            }

        }
    }
    /*сопрограмма это метод, который может приостановить выполнение и вернуть управление Unity, 
    но затем продолжить с того места, на котором оно было остановлено, в следующем фрейме.*/
    private IEnumerator CoolDown()//сопрограмма с типом IEnumerator + yield
    {
        coolDownAttack = true;
        yield return new WaitForSeconds(coolDown);
        coolDownAttack = false;
    }
    public void Death()
    {
        //player.numberEnemiesKilled++;//увеличиваем счетчик
        // RoomController.instance.UpdateRooms();

        RoomController.instance.StartCoroutine(RoomController.instance.RoomCoroutune());
        Destroy(gameObject);//уничтожение врага
    }
}
