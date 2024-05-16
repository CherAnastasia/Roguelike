using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime; //время жизни пули
    public bool isEnemyBullet = false;//вражеская пуля
    private Vector2 lastPos;//последняя позиция
    private Vector2 curPos;//текущая позиция
    private Vector2 playerPos;//позиция игрока
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DeathDelay());//задрежка контроллера пули
    }

    // Update is called once per frame
    void Update()
    {
        if(isEnemyBullet)
        {
            curPos= transform.position;//текущая позиция равана нашей позиции?
            transform.position = Vector2.MoveTowards(transform.position, playerPos, 5f * Time.deltaTime);//переходим от нашей позиции к позиции игрока
            if(curPos== lastPos)//проверяем равна ли наша текущая позиция нашей последней позиции
            {
                Destroy(gameObject);//избавляемся от игрового обьектв
            }
            lastPos = curPos;
        }
    }
    public void GetPlayer(Transform player)
    {
        playerPos = player.position;//позиция игрока равна положению точки игрока
    }
    /*сопрограмма это метод, который может приостановить выполнение и вернуть управление Unity, 
     но затем продолжить с того места, на котором оно было остановлено, в следующем фрейме.*/
    IEnumerator DeathDelay() //сопрограмма с типом IEnumerator + yield
    {
        yield return new WaitForSeconds(lifeTime);//возврат с задержкой длинной в lifeTime (yield return: определяет возвращаемый элемент)
        Destroy(gameObject);//уничтожение пули
    }
    private void OnTriggerEnter2D(Collider2D collision)//метод который работает при триггере (принимает в себя столкновение)
    {
        if(collision.tag == "Enemy" && !isEnemyBullet)//если это враг и это не вражеская пуля
        {
            collision.gameObject.GetComponent<Enemy>().Death(); //убить врага   
            Destroy(gameObject);//уничтожение пули
        }
        if(collision.tag == "Player" && isEnemyBullet) //если это игрок и это вражеская пуля
        {
            play.DamagePlayer(1);//наносим урон
            Destroy(gameObject);//уничтожаем пулю
        }
        if (collision.tag == "Obstacle")
        {
            Destroy(gameObject);//уничтожаем пулю
        }
    }
}
