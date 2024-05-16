using UnityEngine;

[System.Serializable]
public class Item
{
    public string name;//имя
    public string description;//описание
    public Sprite itemImage;
}
public class collection : MonoBehaviour
{
    public Item item;
    public float healtChange;//изменение здоровья
    //public int coin;//монета
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite=item.itemImage;
        Destroy(GetComponent<PolygonCollider2D>());
        gameObject.AddComponent<PolygonCollider2D>();
    }
    public void OnTriggerEnter2D(Collider2D collision)//метод который работает при триггере (принимает в себя столкновение)
    {
        if (collision.tag == "Player")//если это игрок
        {
            player.collectedAmount++;//увеличиваем счетчик
            play.HealPlayer(healtChange);//хилим
            Destroy(gameObject);//уничтожаем обьект
        }
    }
}
