using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomInfo//информация о комнате
{
    public string name;
    public int X;
    public int Y;
}

public class RoomController : MonoBehaviour
{
    public static RoomController instance;
    string currentWorldName = "Basement";//текущее мировое имя (типа для перехода?)
    RoomInfo currentLoadRoomData;//текущие данные о комнате
    Room currRom;//текущая комната
    Queue<RoomInfo> loadRoomQueue = new Queue<RoomInfo>();
    public List<Room> loadRooms = new List<Room>();
    bool isLoadingRoom = false;//загрузочная ли это комната
    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        LoadRoom("Start",0,0);
        LoadRoom("Empty", 1, 0);
        LoadRoom("Empty", -1, 0);
        LoadRoom("Empty", 0, 1);
        LoadRoom("Empty", 0, -1);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRoomQueue();
    }
    void UpdateRoomQueue()
    {
        if (isLoadingRoom)//проверяем загружаем ли мы комнату
        {
            return;//не хотим ничего делать
        }
        if(loadRoomQueue.Count==0)//если количество точек очереди равны нулю
        {
            return; //возвращаемся ибо в очереди ничего не будет и нам вообще не нужно ничего делать
        }
        //если в очереди что то есть
        currentLoadRoomData= loadRoomQueue.Dequeue();//удаляем его из очереди
        isLoadingRoom= true;
        StartCoroutine(LoadRoomRoutine(currentLoadRoomData));
    }
    public void LoadRoom(string name, int x, int y)//загружает сцены
    {
        if(DoesRoomExist(x,y)==true)//нужно проверить существует ли вообще комната
        {
            return;//если она есть, то мы выходим, мы не хотим загружать эту комнату ибо комната уже существует
        }
        RoomInfo newRoomData=new RoomInfo();
        newRoomData.name = name;
        newRoomData.X= x;
        newRoomData.Y= y;

        loadRoomQueue.Enqueue(newRoomData);//хотим поставить комнату в очередь
    }
    /*сопрограмма это метод, который может приостановить выполнение и вернуть управление Unity, 
   но затем продолжить с того места, на котором оно было остановлено, в следующем фрейме.*/
    IEnumerator LoadRoomRoutine(RoomInfo info)//процедура загрузки (сопрограмма с типом IEnumerator + yield)
    {
        string roomName=currentWorldName+info.name;//имя комнаты
        AsyncOperation loadRoom = SceneManager.LoadSceneAsync(roomName, LoadSceneMode.Additive);//асинхронная загрузка сцен
         while(loadRoom.isDone==false)//пока комната не закончит загрузку
         {
            yield return null;
         }
    }
    public void RegisterRoom(Room room)//поестить комнату в сцену
    {
        room.transform.position = new Vector3(
           currentLoadRoomData.X * room.Width,
           currentLoadRoomData.Y * room.Height,
           0);
        room.X= currentLoadRoomData.X;
        room.Y= currentLoadRoomData.Y;
        room.name = currentWorldName + "-" + currentLoadRoomData.name + " " + room.X + ", " + room.Y;
        room.transform.parent = transform;
        isLoadingRoom= false;
        if(loadRooms.Count==null)
        {
            CameraController.instance.currRoom= room;
        }

        loadRooms.Add(room);//добавляем комнату в список выделенных комнат

    }
    public bool DoesRoomExist (int x, int y)//сущесвует ли комната
    {
        return loadRooms.Find(item => item.X == x && item.Y == y) != null;
    }
    public void OnPlayerEnterRoom(Room room)
    {
        CameraController.instance.currRoom = room;
        currRom = room;
    }
}
