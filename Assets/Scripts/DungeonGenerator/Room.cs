using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public int Width;//ширина
    public int Height;//высота
    public int X;
    public int Y;
    private bool updateDoors=false;
    public Room(int x, int y)
    {
        X = x;
        Y = y;
    }

    public Door leftDoor;
    public Door rightDoor;
    public Door topDoor;
    public Door bottomDoor;
    public List<Door> doors = new List<Door>();
    // Start is called before the first frame update
    void Start()
    {
        if(RoomController.instance==null)//убеждаемся что наша комната находится в правильной сцене
        {
            Debug.Log("You pressed play in the wrong scene!");
            return;
        }
        Door[] ds = GetComponentsInChildren<Door>();
        foreach(Door d in ds)
        {
           doors.Add(d);
            switch(d.doorType)
            {
                case Door.DoorType.right:
                    rightDoor= d;
                    break;
                case Door.DoorType.left:
                    leftDoor= d;
                    break;
                case Door.DoorType.top:
                    topDoor= d;
                    break;
                case Door.DoorType.bottom:
                    bottomDoor= d;
                    break;
            }
        }
        RoomController.instance.RegisterRoom(this);
    }
    void Update()
    {
        if (Level.level == 1)
        {
            if (name.Contains("End") && !updateDoors)
            {
                RemoveUnconectedDoors();
                updateDoors = true;
            }
        }
       else
       {
            if (name.Contains("Endd") && !updateDoors)
            {
                RemoveUnconectedDoors();
                updateDoors = true;
            }
       }    
    }

    public void RemoveUnconectedDoors()
    {
        Debug.Log("removing doors");
        foreach (Door door in doors)
        {
            switch (door.doorType)
            {
                case Door.DoorType.right:
                    if(GetRight()==null)//если нет комнаты
                        door.gameObject.SetActive(false);
                    break;
                case Door.DoorType.left:
                    if (GetLeft() == null)//если нет комнаты
                        door.gameObject.SetActive(false);
                    break;
                case Door.DoorType.top:
                    if (GetTop() == null)//если нет комнаты
                        door.gameObject.SetActive(false);
                    break;
                case Door.DoorType.bottom:
                    if (GetButtom() == null)//если нет комнаты
                        door.gameObject.SetActive(false);
                    break;
            }
        }
    }
    public Room GetRight()
    {
        if(RoomController.instance.DoesRoomExist(X+1,Y))//есть ли комната справа
        {
            return RoomController.instance.FindRoom(X + 1, Y);
        }
        return null;
    }
    public Room GetLeft()
    {
        if (RoomController.instance.DoesRoomExist(X - 1, Y)) //есть ли комната слева
        {
            return RoomController.instance.FindRoom(X - 1, Y);
        }
        return null;
    }
    public Room GetTop()
    {
        if (RoomController.instance.DoesRoomExist(X, Y+1)) //есть ли комната сверху
        {
            return RoomController.instance.FindRoom(X, Y+1);
        }
        return null;
    }
    public Room GetButtom()
    {
        if (RoomController.instance.DoesRoomExist(X, Y-1))//есть ли комната снизу
        {
            return RoomController.instance.FindRoom(X, Y-1);
        }
        return null;
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
