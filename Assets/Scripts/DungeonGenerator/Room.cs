using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public int Width;//ширина
    public int Height;//высота
    public int X;
    public int Y;
    // Start is called before the first frame update
    void Start()
    {
        if(RoomController.instance==null)//убеждаемся что наша комната находится в правильной сцене
        {
            Debug.Log("You pressed play in the wrong scene!");
            return;
        }
        RoomController.instance.RegisterRoom(this);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnDrawGizmos()//рисует 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(Width, Height, 0));
    }
    public Vector3 GetRoomCentre()
    {
        return new Vector3(X * Width, Y * Height);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Player")
        {
            RoomController.instance.OnPlayerEnterRoom(this);
        }
    }
}
