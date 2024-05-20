using UnityEngine;

public class Door : MonoBehaviour
{
   public enum DoorType
    {
        left,
        right, 
        top, 
        bottom
    };
   public DoorType doorType;
    public GameObject doorCollider;
    private GameObject playerr;
    private float widthbottom =3.5f;
    private float widthtop = 4.5f;
    private float widthleft = 4.5f;
    private float widthright = 4.5f;
    // private float widthOffset = 5f;
    private void Start()
    {
         playerr = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            switch (doorType)
            {
                case DoorType.bottom:
                    playerr.transform.position = new Vector2(transform.position.x, transform.position.y - widthbottom);
                    break;
                case DoorType.left:
                    playerr.transform.position = new Vector2(transform.position.x - widthleft, transform.position.y);
                    break;
                case DoorType.right:
                    playerr.transform.position = new Vector2(transform.position.x + widthright, transform.position.y);
                    break;
                case DoorType.top:
                    playerr.transform.position = new Vector2(transform.position.x, transform.position.y + widthtop);
                    break;
            }
        }
    }
}
