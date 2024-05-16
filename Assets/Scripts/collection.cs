using UnityEngine;

[System.Serializable]
public class Item
{
    public string name;//���
    public string description;//��������
    public Sprite itemImage;
}
public class collection : MonoBehaviour
{
    public Item item;
    public float healtChange;//��������� ��������
    //public int coin;//������
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite=item.itemImage;
        Destroy(GetComponent<PolygonCollider2D>());
        gameObject.AddComponent<PolygonCollider2D>();
    }
    public void OnTriggerEnter2D(Collider2D collision)//����� ������� �������� ��� �������� (��������� � ���� ������������)
    {
        if (collision.tag == "Player")//���� ��� �����
        {
            player.collectedAmount++;//����������� �������
            play.HealPlayer(healtChange);//�����
            Destroy(gameObject);//���������� ������
        }
    }
}
