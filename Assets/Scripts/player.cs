using TMPro;
using UnityEngine;

public class player : MonoBehaviour
{
    public float speed;//скорость игрока
    Rigidbody2D rigidbody2D;//игрок
    public TextMeshProUGUI collectedText;
    public static int collectedAmount=0;
    public GameObject bulletPrefab;//пуля
    public float bulletSpeed;//скорость пули
    private float lasttFire;//последний огонь
    public float fireDelay;//задержка огня
    void Awake()
    {
        collectedAmount = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D= GetComponent<Rigidbody2D>();//оюьект твердое тело
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");//горизонстальная ось для игрока (реагирует на a d)
        float vertical = Input.GetAxis("Vertical");//вертикальная ось для игрока (реагирует на w s)

        float shootHor = Input.GetAxis("ShootHorizontal");//горизонстальная ось для пули (ось которую мы сами сделали) (реагирует на право лево)
        float shootVert = Input.GetAxis("ShootVertical");//вертикальная ось для пули (ось которую мы сами сделали) (реагирует на низ вверх)
        if ((shootHor!=0 || shootVert!=0) && Time.time>lasttFire+fireDelay)//проверяем получем ли мы верт или гориз данные + больше ли это чем наша задержка+последний огонь 
        {
            Shoot(shootHor,shootVert);//стреляем
            lasttFire= Time.time;//устанавливаем значение посследнему огню
        }

        rigidbody2D.velocity = new Vector3(horizontal*speed, vertical*speed,0);//устанавливаем скорость тела (на z перемещаться нельзя)
        collectedText.text = "Item Collected: " + collectedAmount;//меняем значение текста (меняем количество сьеденных обьектов)
    }

    void Shoot(float x, float y)//метод стрельбы принимающий в себя верт и гор
    {
        GameObject bullet = Instantiate(bulletPrefab,transform.position, transform.rotation) as GameObject;//игровой обьект равнй экземпляру пули в нашей позиции при преобразовании или вращении игрока (?)
        bullet.AddComponent<Rigidbody2D>().gravityScale = 0;//делаем твердый обьект с гравитацией =0
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(//скорость равная вектору3
            (x < 0) ? Mathf.Floor(x) * bulletSpeed : Mathf.Ceil(x) * bulletSpeed,
            (y < 0) ? Mathf.Floor(y) * bulletSpeed : Mathf.Ceil(y) * bulletSpeed,
            0); //(на z перемещаться нельзя)
    }
}
