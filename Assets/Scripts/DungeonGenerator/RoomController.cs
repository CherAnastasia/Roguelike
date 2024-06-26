using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using Unity.VisualScripting;

public class RoomInfo//���������� � �������
{
    public string name;
    public int X;
    public int Y;
}

public class RoomController : MonoBehaviour
{
    public static RoomController instance;
    string currentWorldName = "Basement";//������� ������� ��� (���� ��� ��������?)
    RoomInfo currentLoadRoomData;//������� ������ � �������
    Room currRom;//������� �������
    Queue<RoomInfo> loadRoomQueue = new Queue<RoomInfo>();
    public List<Room> loadRooms = new List<Room>();
    bool isLoadingRoom = false;//����������� �� ��� �������
    bool spawnedBossRoom = false;
    bool updateRooms = false;
    void Awake()
    {
        instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        UpdateRoomQueue();
    }
    void UpdateRoomQueue()
    {
        if (isLoadingRoom)//��������� ��������� �� �� �������
        {
            return;//�� ����� ������ ������
        }
        if(loadRoomQueue.Count==0)//���� ���������� ����� ������� ����� ����
        {
            if(!spawnedBossRoom)
            {
                StartCoroutine(SpawnBossRoom());
            }
            else if (spawnedBossRoom && !updateRooms)
            {
                foreach(Room room in loadRooms)
                {
                    room.RemoveUnconectedDoors();
                }
                UpdateRooms();
                updateRooms = true;
            }
            return; //������������ ��� � ������� ������ �� ����� � ��� ������ �� ����� ������ ������
        }
        //���� � ������� ��� �� ����
        currentLoadRoomData= loadRoomQueue.Dequeue();//������� ��� �� �������
        isLoadingRoom= true;
        StartCoroutine(LoadRoomRoutine(currentLoadRoomData));
    }
    IEnumerator SpawnBossRoom()
    {
        spawnedBossRoom=true;
        yield return new WaitForSeconds(0.5f);
        if(loadRoomQueue.Count==0)
        {
            Room bossRoom = loadRooms[loadRooms.Count - 1];
            Room tempRoom = new Room(bossRoom.X, bossRoom.Y);
            Destroy(bossRoom.gameObject);
            var roomToRemove = loadRooms.Single(r => r.X == tempRoom.X && r.Y == tempRoom.Y);
            loadRooms.Remove(roomToRemove);
            if(Level.level==1)
            LoadRoom("End", tempRoom.X, tempRoom.Y);
            else
            LoadRoom("Endd", tempRoom.X, tempRoom.Y);
        }
    }
    public void LoadRoom(string name, int x, int y)//��������� �����
    {
        if(DoesRoomExist(x,y)==true)//����� ��������� ���������� �� ������ �������
        {
            return;//���� ��� ����, �� �� �������, �� �� ����� ��������� ��� ������� ��� ������� ��� ����������
        }
        RoomInfo newRoomData=new RoomInfo();
        newRoomData.name = name;
        newRoomData.X= x;
        newRoomData.Y= y;

        loadRoomQueue.Enqueue(newRoomData);//����� ��������� ������� � �������
    }
    /*����������� ��� �����, ������� ����� ������������� ���������� � ������� ���������� Unity, 
   �� ����� ���������� � ���� �����, �� ������� ��� ���� �����������, � ��������� ������.*/
    IEnumerator LoadRoomRoutine(RoomInfo info)//��������� �������� (����������� � ����� IEnumerator + yield)
    {
        string roomName=currentWorldName+info.name;//��� �������
        AsyncOperation loadRoom = SceneManager.LoadSceneAsync(roomName, LoadSceneMode.Additive);//����������� �������� ����
         while(loadRoom.isDone==false)//���� ������� �� �������� ��������
         {
            yield return null;
         }
    }
    public void RegisterRoom(Room room)//�������� ������� � �����
    {
        if(!DoesRoomExist(currentLoadRoomData.X,currentLoadRoomData.Y))
        {
            room.transform.position = new Vector3(
          currentLoadRoomData.X * room.Width,
          currentLoadRoomData.Y * room.Height,
          0);
            room.X = currentLoadRoomData.X;
            room.Y = currentLoadRoomData.Y;
            room.name = currentWorldName + "-" + currentLoadRoomData.name + " " + room.X + ", " + room.Y;
            room.transform.parent = transform;
            isLoadingRoom = false;
            if (loadRooms.Count == 0)
            {
                CameraController.instance.currRoom = room;
            }
            loadRooms.Add(room);//��������� ������� � ������ ���������� ������
            //room.RemoveUnconectedDoors();
        } 
        else
        {
            Destroy(room.gameObject);
            isLoadingRoom = false;
        }

    }
    public bool DoesRoomExist (int x, int y)//��������� �� �������
    {
        return loadRooms.Find(item => item.X == x && item.Y == y) != null;
    }
    public Room FindRoom(int x, int y)
    {
        return loadRooms.Find(item => item.X == x && item.Y == y);
    }
    public string GetRandomRoomName()
    {
            string[] possibleRooms = new string[]
            {
            "Empty",
            "Basic1"
            };
        if (Level.level!=1)
        {
             possibleRooms = new string[]
            {
            "Empty2",
            "Basic2"
            };
        }

        return possibleRooms[Random.Range(0, possibleRooms.Length)];
    }
    public void OnPlayerEnterRoom(Room room)
    {
        CameraController.instance.currRoom = room;
        currRom = room;
        //UpdateRooms();
        StartCoroutine(RoomCoroutune());
    }
    public IEnumerator RoomCoroutune()
    {
        yield return new WaitForSeconds(0.2f);
        UpdateRooms();
    }
    public void UpdateRooms()
    {
        foreach(Room room in loadRooms)
        {
            if (currRom!=room)
            {
                Enemy[] enemies = room.GetComponentsInChildren<Enemy>();
                if(enemies!=null)
                {
                    foreach(Enemy enemy in enemies)
                    {
                        enemy.notInRoom = true;
                        Debug.Log("Not in room");
                    }
                    foreach (Door door in room.GetComponentsInChildren<Door>())
                    {
                        door.doorCollider.SetActive(false);
                    }
                }
                else
                {
                    foreach (Door door in room.GetComponentsInChildren<Door>())
                    {
                        door.doorCollider.SetActive(false);
                    }
                }
            }
            else
            {
                Enemy[] enemies = room.GetComponentsInChildren<Enemy>();
                if (enemies /*!= null*/.Length > 0)
                {
                    foreach (Enemy enemy in enemies)
                    {
                        enemy.notInRoom = false;
                        Debug.Log("In in room");
                    }
                    foreach (Door door in room.GetComponentsInChildren<Door>())
                    {
                        door.doorCollider.SetActive(true);
                    }
                }
                else
                {
                    foreach (Door door in room.GetComponentsInChildren<Door>())
                    {
                        door.doorCollider.SetActive(false);
                    }
                }
            }
        }
    }
}
